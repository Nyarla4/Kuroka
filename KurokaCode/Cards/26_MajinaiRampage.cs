using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards;

public class MajinaiRampage() : KurokaCard(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new("ApplyCount", 4M), // Java: baseSecondMagic; upgrade does not change this
        new PowerVar<MajinaiPower>(4M) // Java: magicNumber (amount applied)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var alive = this.CombatState!
            .GetCreaturesOnSide(CombatSide.Enemy)
            .Where(c => c.IsAlive)
            .ToList();

        if (alive.Count == 0) return;

        var rng = this.Owner.RunState.Rng.Niche;
        int applyCount = (int)DynamicVars["ApplyCount"].BaseValue;

        for (int i = 0; i < applyCount; i++)
        {
            Creature target = alive[rng.NextInt(0, alive.Count)];
            await PowerCmd.Apply<MajinaiPower>(
                target,
                DynamicVars.Power<MajinaiPower>().BaseValue,
                this.Owner.Creature,
                this
            );
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Power<MajinaiPower>().UpgradeValueBy(2M);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<MajinaiPower>()
    ];
}

