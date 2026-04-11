using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Kuroka.KurokaCode.Powers;

public class PepZeLiPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    // AfterEnergyReset = 턴 시작 에너지 충전 직후
    public override async Task AfterEnergyReset(Player player)
    {
        if (this.Owner.IsDead) return;
        if (player.Creature != this.Owner) return;

        this.Flash();
        await PlayerCmd.GainEnergy(this.Amount, player);
        await PowerCmd.Remove<PepZeLiPower>(this.Owner);
    }
}