using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialVulnerablePotion : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyEnemy;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<VulnerablePower>(2M)];

    public override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<VulnerablePower>()];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialVulnerablePotion vulnerablePotion = this;
        PotionModel.AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color("fd2155"));
        VulnerablePower vulnerablePower = await PowerCmd.Apply<VulnerablePower>(target, vulnerablePotion.DynamicVars.Vulnerable.BaseValue, vulnerablePotion.Owner.Creature, (CardModel) null);
    }
}