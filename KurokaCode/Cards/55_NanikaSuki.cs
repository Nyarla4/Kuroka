using Kuroka.KurokaCode.Cards.Sub;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Cards;

public class NanikaSuki() : KurokaCard(0, CardType.Skill,
    CardRarity.Uncommon, TargetType.None)
{
    protected override bool HasEnergyCostX => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("Repeat", 0M)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var repeatCount = this.ResolveEnergyXValue() + DynamicVars["Repeat"].BaseValue;
        for (int i = 0; i < repeatCount; i++)
        {
            var random = Owner.RunState.Rng.CombatCardGeneration;
            int target = random.NextInt(0, 3);

            CardModel card = null;
            switch (target)
            {
                case 0:
                    card = (CardModel)ModelDb.Get<Chocomint>().MutableClone();
                    break;
                case 1:
                    card = (CardModel)ModelDb.Get<Cookiecream>().MutableClone();
                    break;
                case 2:
                    card = (CardModel)ModelDb.Get<Strawberry>().MutableClone();
                    break;
            }
            if (IsUpgraded)
            {
                CardCmd.Upgrade(card);
            }
            card.Owner = Owner;
            CombatState.AddCard(card);

            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Repeat"].UpgradeValueBy(1M);
    }
}