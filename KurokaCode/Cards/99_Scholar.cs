using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Cards;

public class Scholar() : KurokaCard(-2, CardType.Curse, CardRarity.Curse, TargetType.None)
{
    // Java: dontTriggerOnUseCard 가 있을 때만 Vulnerable 을 부여.
    // 현재 C# 코드베이스에서는 해당 플래그를 확인할 수 없어, 카드 사용 시 Vulnerable 을 부여하도록 단순화했습니다.

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<VulnerablePower>(1M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<VulnerablePower>(
            this.Owner.Creature,
            DynamicVars.Power<VulnerablePower>().BaseValue,
            this.Owner.Creature,
            this
        );
    }
    protected override bool IsPlayable => false;
}

