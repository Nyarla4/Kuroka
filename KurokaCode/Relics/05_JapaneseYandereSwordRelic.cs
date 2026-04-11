// 05_JapaneseYandereSwordRelic.cs
// Java: onExhaust → 랜덤 적에게 Majinai(7)
using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Relics;

public class JapaneseYandereSwordRelic : KurokaRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<MajinaiPower>(7M)];

    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool causedByEthereal)
    {
        // 카드 소유자가 이 유물 소유 플레이어여야 함
        if (card.Owner != Owner) return;

        var alive = Owner.Creature.CombatState!
            .GetCreaturesOnSide(CombatSide.Enemy)
            .Where(c => c.IsAlive)
            .ToList();

        if (alive.Count == 0) return;

        this.Flash();
        var rng = Owner.RunState.Rng.CombatTargets;
        Creature target = rng.NextItem(alive)!;

        await PowerCmd.Apply<MajinaiPower>(
            target,
            DynamicVars.Power<MajinaiPower>().BaseValue,
            Owner.Creature,
            null);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<MajinaiPower>()];
}