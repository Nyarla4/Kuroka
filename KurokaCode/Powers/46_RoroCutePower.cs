using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Powers;

public class RoroCutePower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        Creature enemy = player.RunState.Rng.CombatTargets.NextItem<Creature>((IEnumerable<Creature>) CombatState.HittableEnemies);
        if (enemy != null)
        {
            await PowerCmd.Apply<WeakPower>(
                enemy,
                Amount,
                Target,
                null);
        }
        await base.AfterPlayerTurnStart(choiceContext, player);
    }
}