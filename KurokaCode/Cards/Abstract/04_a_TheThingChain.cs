using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Cards.Abstract;

public abstract class TheThingChainCard<TNext>(CardType type, TargetType target)
    : KurokaCard(0, type, CardRarity.Token, target)
    where TNext : CardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [..HoverTipFactory.FromCardWithCardHoverTips<TNext>(IsUpgraded), ..AdditionalHoverTips];

    /// <summary>
    /// TNext 툴팁 이후 추가할 툴팁. 기본값은 빈 배열.
    /// Power 툴팁 등 추가가 필요한 자식 클래스만 override.
    /// </summary>
    protected virtual IEnumerable<IHoverTip> AdditionalHoverTips => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        var cardPool = new List<CardModel> { ModelDb.Get<TNext>() };
    
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

        if (IsUpgraded)
            await CardPileCmd.Draw(choiceContext, 1M, this.Owner);

        await ExecuteEffect(choiceContext, cardPlay);
    }

    protected abstract Task ExecuteEffect(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay);
}