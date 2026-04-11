using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Powers;

public class RoroMajinaiPower : RorokaAdditionalPower
{
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (this.Owner.IsDead)
        {
            return;
        }
        
        Creature? enemy = player.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
        if (enemy != null)
        {
            await PowerCmd.Apply<MajinaiPower>(
                enemy,
                Amount,
                Target,
                null);
        }
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<MajinaiPower>()
    ];
}