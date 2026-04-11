using Kuroka.KurokaCode.Cards.Sub;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Cards;

public class TheLongThing() : KurokaCard(1, CardType.Skill, CardRarity.Common, TargetType.None)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var cardPool = new List<CardModel> { ModelDb.Get<TheThing1>() };
    
        var card = CardFactory.GetDistinctForCombat(
            this.Owner, 
            cardPool, 
            1, 
            this.Owner.RunState.Rng.CombatCardGeneration
        ).FirstOrDefault();

        if (card != null)
        {
            if (IsUpgraded)
            {
                CardCmd.Upgrade(card);
            }
            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Deck, true);
        }
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        ..HoverTipFactory.FromCardWithCardHoverTips<TheThing1>()
    ];
}