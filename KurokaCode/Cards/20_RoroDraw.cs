using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Pets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Cards;

public class RoroDraw() : RorokaCard(1, CardType.Skill,
    CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar("CommonDraw", 2),
        new CardsVar("ExtraDraw", 1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        IEnumerable<CardModel> cardModels =
            await CardPileCmd.Draw(choiceContext, DynamicVars["CommonDraw"].BaseValue, this.Owner);

        bool isRoroka = false;

        foreach (CardModel drawedCard in cardModels)
        {
            if (drawedCard.Tags.Contains(Roroka.RorokaTag))
            {
                isRoroka = true;
                break;
            }
        }

        if (isRoroka)
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars["ExtraDraw"].BaseValue, this.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ExtraDraw"].UpgradeValueBy(1M);
    }
}