using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Kuroka.KurokaCode.Powers;

public class XXMoePower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    private int _endOfTurnHp = -1;

    // 턴 종료 시 HP 기록 (적 턴 동안 피해를 추적)
    public override async Task BeforeTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side)
    {
        if (side != this.Owner.Side || this.Owner.IsDead) return;
        _endOfTurnHp = this.Owner.CurrentHp;
    }

    // 다음 턴 시작 시 HP 변화 확인 후 자기 제거
    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        if (this.Owner.IsDead) return;
        if (_endOfTurnHp < 0)
        {
            // 적용 첫 턴 — 아직 BeforeTurnEnd가 오지 않은 경우
            _endOfTurnHp = this.Owner.CurrentHp;
            return;
        }

        if (_endOfTurnHp > this.Owner.CurrentHp)
        {
            await PowerCmd.Apply<DelusionFactorPower>(
                this.Owner,
                Amount,
                this.Owner,
                null);
        }

        await PowerCmd.Remove<XXMoePower>(this.Owner);
    }
}