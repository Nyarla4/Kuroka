using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Kuroka.KurokaCode.Cards;

public class TheLongThing() : KurokaCard(1, CardType.Skill, CardRarity.Common, TargetType.None)
{
//소멸카드 아무거나 찾아서 소멸 처리
//단조카드 아무거나 찾아서 Preview랑 카드생성 처리
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        
    }

    protected override void OnUpgrade() {
        this.EnergyCost.UpgradeBy(-1);
    }
}