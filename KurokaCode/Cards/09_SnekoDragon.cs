using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Cards;

public class SnekoDragon() : KurokaCard(1, CardType.Skill, CardRarity.Common, TargetType.None)
{
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var random = Owner.RunState.Rng.Niche;
        int drawAmount = random.NextInt(1, 5);
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, (Decimal) drawAmount, this.Owner);
        foreach (CardModel drawedCard in cardModels)
        {
            int costAmount = random.NextInt(0, 4);
            drawedCard.EnergyCost.SetThisTurn(costAmount);
        }
    }

    protected override void OnUpgrade() {
        this.EnergyCost.UpgradeBy(-1);
    }
}