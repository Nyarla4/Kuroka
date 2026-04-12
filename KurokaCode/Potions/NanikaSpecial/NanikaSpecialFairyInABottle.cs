using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialFairyInABottle : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.Automatic;

    public override TargetType TargetType => TargetType.Self;

    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        PotionModel.AssertValidForTargetedPotion(target);
        await CreatureCmd.Heal(target, Math.Max((Decimal) target.MaxHp * 0.1M, 1M));
    }

    public override bool ShouldDie(Creature creature) => creature != this.Owner.Creature;

    public override async Task AfterPreventingDeath(Creature creature)
    {
        await this.OnUseWrapper((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), creature);
    }
}