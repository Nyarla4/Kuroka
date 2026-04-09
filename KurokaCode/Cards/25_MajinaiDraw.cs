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

public class MajinaiDraw() : KurokaCard(1, CardType.Skill, CardRarity.Common, TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new("DrawCount", 2M),
        new PowerVar<MajinaiPower>(4M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, DynamicVars["DrawCount"].BaseValue, this.Owner);

        var alive = this.CombatState!
            .GetCreaturesOnSide(CombatSide.Enemy)
            .Where(c => c.IsAlive)
            .ToList();

        if (alive.Count == 0) return;

        var rng = this.Owner.RunState.Rng.Niche;

        // Java: for (int i = 0; i < 2; i++)
        const int applyCount = 2;

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
        this.DynamicVars["DrawCount"].UpgradeValueBy(1M);
        this.DynamicVars.Power<MajinaiPower>().UpgradeValueBy(2M);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<MajinaiPower>()
    ];
}

