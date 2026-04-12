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

public class NanikaSpecialFyshOil : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<StrengthPower>(1M),
        new PowerVar<DexterityPower>(1M)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.FromPower<DexterityPower>()
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialFyshOil fyshOil = this;
        PotionModel.AssertValidForTargetedPotion(target);
        StrengthPower strengthPower = await PowerCmd.Apply<StrengthPower>(target, fyshOil.DynamicVars.Strength.BaseValue, fyshOil.Owner.Creature, (CardModel) null);
        DexterityPower dexterityPower = await PowerCmd.Apply<DexterityPower>(target, fyshOil.DynamicVars.Dexterity.BaseValue, fyshOil.Owner.Creature, (CardModel) null);
    }
}