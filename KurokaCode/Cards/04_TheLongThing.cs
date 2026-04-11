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
        var card = Owner.Creature.CombatState.CreateCard(ModelDb.Get<TheThing1>(), Owner);
        if (IsUpgraded) CardCmd.Upgrade(card);
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);

        await CardPileCmd.Add(card, PileType.Draw, CardPilePosition.Random);
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