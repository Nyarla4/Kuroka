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

public class NanikaSpecialGhostInAJar : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<IntangiblePower>(1M)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<IntangiblePower>()
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialGhostInAJar ghostInAjar = this;
        PotionModel.AssertValidForTargetedPotion(target);
        IntangiblePower intangiblePower = await PowerCmd.Apply<IntangiblePower>(target, ghostInAjar.DynamicVars.Power<IntangiblePower>().BaseValue, ghostInAjar.Owner.Creature, (CardModel) null);
    }
}