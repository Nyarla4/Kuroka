using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Powers;

public class DelusioningPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    private bool IsWitch =>
        this.Owner?.GetPower<DelusionFactorPower>()?.Amount >= 10;

    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        if (this.Owner.IsDead) return;

        if (IsWitch)
        {
            decimal delusionAmount = this.Owner.GetPower<DelusionFactorPower>()!.Amount;
            // Java: VigorPower (다음 공격력 증가) → CS에서는 StrengthPower로 근사
            // TODO: VigorPower CS 구현 확인 후 교체
            await PowerCmd.Apply<StrengthPower>(
                this.Owner,
                delusionAmount * Amount,
                this.Owner,
                null);
        }
        else
        {
            await PowerCmd.Apply<DelusionFactorPower>(
                this.Owner,
                Amount,
                this.Owner,
                null);
        }
    }
}