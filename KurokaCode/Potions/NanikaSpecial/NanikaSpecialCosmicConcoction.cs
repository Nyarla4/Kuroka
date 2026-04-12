using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Potions;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialCosmicConcoction :NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(2)
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialCosmicConcoction cosmicConcoction = this;
        foreach (CardModel card in CardFactory.GetDistinctForCombat(cosmicConcoction.Owner, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(cosmicConcoction.Owner.UnlockState, cosmicConcoction.Owner.RunState.CardMultiplayerConstraint), cosmicConcoction.DynamicVars.Cards.IntValue, cosmicConcoction.Owner.RunState.Rng.CombatCardGeneration))
        {
            CardCmd.Upgrade(card);
            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
        }
    }
}