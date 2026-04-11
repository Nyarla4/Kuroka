using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Powers;

public abstract class RorokaAdditionalPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool ShouldPowerBeRemovedOnDeath(PowerModel power) => power != this;

    /// <summary>로로카가 살아있을 때만 발동</summary>
    protected bool CanUse() => !this.Owner.IsDead && this.Owner.CurrentHp > 0;
}