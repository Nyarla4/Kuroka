using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Powers;

public class DestinyPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterDamageReceived(
        PlayerChoiceContext choiceContext,
        Creature target,
        DamageResult result,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        if (target != this.Owner || dealer == null || dealer == this.Owner) return;
        
        decimal majinaiAmount = Amount; // buff * stacks
        await PowerCmd.Apply<MajinaiPower>(
            dealer,
            majinaiAmount,
            this.Owner,
            null);
    }

    // 턴 시작 시 자기 제거 (1회성)
    public override async Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        if (this.Owner.IsDead) return;
        await PowerCmd.Remove<DestinyPower>(this.Owner);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<MajinaiPower>()];
}