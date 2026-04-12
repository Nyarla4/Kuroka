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

public class NanikaSpecialHeartOfIron:NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<PlatingPower>(5M)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PlatingPower>(),
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialHeartOfIron heartOfIron = this;
        PotionModel.AssertValidForTargetedPotion(target);
        PlatingPower platingPower = await PowerCmd.Apply<PlatingPower>(target, heartOfIron.DynamicVars.Power<PlatingPower>().BaseValue, heartOfIron.Owner.Creature, (CardModel) null);
    }
}