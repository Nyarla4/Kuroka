using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public sealed class NanikaSpecialEntropicBrew : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.AnyTime;

    public override TargetType TargetType => TargetType.Self;
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new RepeatVar(2)
    ];
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialEntropicBrew entropicBrew = this;
        for (int i = 0; i < DynamicVars.Repeat.BaseValue; i++)
        {
            if (!(await PotionCmd.TryToProcure(PotionFactory.CreateRandomPotionOutOfCombat(entropicBrew.Owner, entropicBrew.Owner.RunState.Rng.CombatPotionGeneration).ToMutable(), entropicBrew.Owner)).success)
                break;
        }
    }
}