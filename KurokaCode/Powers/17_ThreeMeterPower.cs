using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Powers;

public class ThreeMeterPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    // 피격 시 DelusionFactor * Amount 반격
    public override async Task AfterDamageReceived(
        PlayerChoiceContext choiceContext,
        Creature target,
        DamageResult result,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        if (target != this.Owner) return;
        if (dealer == null || dealer == this.Owner) return;
        if (result.TotalDamage <= 0) return;

        decimal delusionAmount =
            this.Owner.GetPower<DelusionFactorPower>()?.Amount ?? 0M;
        decimal counterDamage = Amount * delusionAmount;

        if (counterDamage <= 0) return;

        this.Flash();
        await CreatureCmd.Damage(choiceContext, 
            dealer,
            counterDamage,
            ValueProp.Unpowered,
            this.Owner,
            null
        );
    }

    // 턴 시작 시 자기 제거
    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        if (this.Owner.IsDead) return;
        await PowerCmd.Remove<ThreeMeterPower>(this.Owner);
        await base.AfterPlayerTurnStart(choiceContext, player);
    }
}
