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
        var card = Owner.Creature.CombatState.CreateCard(ModelDb.Get<TNext>(), Owner);
        if (IsUpgraded) CardCmd.Upgrade(card);
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);

        await CardPileCmd.Add(card, PileType.Draw, CardPilePosition.Random);
        
        if (IsUpgraded)
            await CardPileCmd.Draw(choiceContext, 1M, this.Owner);

        await ExecuteEffect(choiceContext, cardPlay);
    }

    protected abstract Task ExecuteEffect(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay);
}