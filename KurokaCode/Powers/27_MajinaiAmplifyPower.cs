using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Powers;

public class MajinaiAmplifyPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    /// <summary>
    /// 이 파워 소유자가 MajinaiPower를 적용할 때 Amount만큼 추가 증폭
    /// ModifyDamageAdditive와 동일한 패턴
    /// </summary>
    public override Decimal ModifyPowerAmountGiven(
        PowerModel power,
        Creature giver,
        Decimal amount,
        Creature? target,
        CardModel? cardSource)
    {
        if (power is MajinaiPower && giver == this.Owner)
            return amount+Amount;
        return amount;
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<MajinaiPower>()];
}