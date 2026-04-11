using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Kuroka.KurokaCode.Powers;

public class NeredKurokaPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterCardPlayed(
        PlayerChoiceContext context,
        CardPlay cardPlay)
    {
        // 이 파워 소유자가 카드를 사용했을 때만 발동
        if (cardPlay.Card.Owner != this.Owner.PetOwner &&
            cardPlay.Card.Owner?.Creature != this.Owner) return;

        foreach (Creature enemy in this.CombatState.GetCreaturesOnSide(CombatSide.Enemy)
                     .Where(e => e.IsAlive))
        {
            await PowerCmd.Apply<MajinaiPower>(
                enemy,
                Amount,
                this.Owner,
                null);
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<MajinaiPower>()];
}