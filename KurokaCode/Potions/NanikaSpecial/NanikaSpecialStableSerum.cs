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

public class NanikaSpecialStableSerum : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(CardKeyword.Retain)
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new RepeatVar(1)
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialStableSerum stableSerum = this;
        PotionModel.AssertValidForTargetedPotion(target);
        RetainHandPower retainHandPower = await PowerCmd.Apply<RetainHandPower>(target, stableSerum.DynamicVars.Repeat.BaseValue, stableSerum.Owner.Creature, (CardModel) null);
    }
}