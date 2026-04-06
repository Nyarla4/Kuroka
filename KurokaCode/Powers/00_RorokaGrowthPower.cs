using Kuroka.KurokaCode.Pets;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace Kuroka.KurokaCode.Powers;

public class RorokaGrowthPower : KurokaPower // (STS2 파워 베이스 클래스 이름에 맞게 수정)
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    Logger logger = new Logger("RorokaGrowthPower",LogType.Actions);
    // 🌊 [흐름 영역]: 파워가 대상에게 '부여'될 때의 처리
    public override Task BeforePowerAmountChanged(
        PowerModel power,        // 부여되는 파워 객체
        Decimal amount,          // 부여되는 수치
        Creature target,         // 파워를 받는 대상
        Creature? applier,       // 파워를 부여한 주체
        CardModel? cardSource)   // 파워를 부여한 카드 (있을 경우)
    {
        if (power != this)
        {
            return base.BeforePowerAmountChanged(power, amount, target, applier, cardSource);
        }

        logger.Info("로그 체크");
        if (target is Roroka)
        {
            logger.Info("로로카 체크");
            target.MaxHp += (int)amount;   
        }
        
        return base.BeforePowerAmountChanged(power, amount, target, applier, cardSource);
    }

    // 🌊 [흐름 영역]: 파워가 대상에서 '제거'될 때의 처리 (보통 전투 종료 시 파워가 일괄 해제됨)
    public override Task AfterCombatEnd(CombatRoom room)
    {
        // 전투가 끝났으므로 올려두었던 최대 체력을 다시 깎아서 원상 복구합니다.
        Owner.MaxHp -= this.Amount;
            
        // 만약 최대 체력이 깎였는데 현재 체력이 그보다 높다면 깎아내립니다.
        if (Owner.CurrentHp > Owner.MaxHp)
        {
            Owner.CurrentHp = Owner.MaxHp;
        }
        return base.AfterCombatEnd(room);
    }
}