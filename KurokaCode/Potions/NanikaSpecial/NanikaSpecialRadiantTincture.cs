using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialRadiantTincture : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.ForEnergy((PotionModel)this)
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(1),
        new PowerVar<RadiancePower>(2M)
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialRadiantTincture radiantTincture = this;
        PotionModel.AssertValidForTargetedPotion(target);
        await PlayerCmd.GainEnergy((Decimal) radiantTincture.DynamicVars.Energy.IntValue, target.Player);
        RadiancePower radiancePower = await PowerCmd.Apply<RadiancePower>(target, radiantTincture.DynamicVars.Power<RadiancePower>().BaseValue, radiantTincture.Owner.Creature, (CardModel) null);
    }
}