// 06_MajitonomiconRelic.cs
// Java: atTurnStart → 방어도 > 0이면 랜덤 적에게 Majinai(방어도 수치)
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Kuroka.KurokaCode.Relics;

public class MajitonomiconRelic : KurokaRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner) return;

        int block = Owner.Creature.Block;
        if (block <= 0) return;

        var alive = Owner.Creature.CombatState!
            .GetCreaturesOnSide(CombatSide.Enemy)
            .Where(c => c.IsAlive)
            .ToList();

        if (alive.Count == 0) return;

        this.Flash();
        Creature target = Owner.RunState.Rng.CombatTargets.NextItem(alive)!;

        await PowerCmd.Apply<MajinaiPower>(
            target,
            block,
            Owner.Creature,
            null);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<MajinaiPower>()];
}