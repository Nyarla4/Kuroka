using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Kuroka.KurokaCode.Powers;

public class ThreeMeterPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (this.Owner.IsDead)
        {
            await base.AfterPlayerTurnStart(choiceContext, player);
            return;
        }

        await PowerCmd.Remove<ThreeMeterPower>(this.Owner);
        await base.AfterPlayerTurnStart(choiceContext, player);
    }
    // 피격시 찾아서 것 DelusionFactorPower Amount * this.Amount 반격 처리 할 것
}
