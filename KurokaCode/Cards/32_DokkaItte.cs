using Kuroka.KurokaCode.Cards.Sub;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Cards;

public class DokkaItte() : KurokaCard(2, CardType.Skill, CardRarity.Common, TargetType.None)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        List<CardModel> list = PileType.Hand.GetPile(this.Owner).Cards.ToList<CardModel>();
        int cardCount = list.Count;
        foreach (CardModel card2 in list)
            await CardCmd.Exhaust(choiceContext, card2);
        for (int i = 0; i < cardCount; i++)
        {
            var card = (CardModel)ModelDb.Get<Jujutsu>().MutableClone();
            card.Owner = Owner;
            if (IsUpgraded)
            {
                CardCmd.Upgrade(card);
            }
            CombatState.AddCard(card);
            await CardPileCmd.AddGeneratedCardToCombat(card,
                PileType.Hand,
                true);
        }
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..HoverTipFactory.FromCardWithCardHoverTips<Jujutsu>(IsUpgraded)
    ];
}