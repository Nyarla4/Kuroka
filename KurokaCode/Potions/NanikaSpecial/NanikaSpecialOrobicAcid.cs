using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialOrobicAcid:NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialOrobicAcid orobicAcid = this;
        List<CardModel> cards = new List<CardModel>();
        cards.AddRange(CardFactory.GetDistinctForCombat(orobicAcid.Owner, orobicAcid.Owner.Character.CardPool.GetUnlockedCards(orobicAcid.Owner.UnlockState, orobicAcid.Owner.RunState.CardMultiplayerConstraint).Where<CardModel>((Func<CardModel, bool>) (c => c.Type == CardType.Attack)), 1, orobicAcid.Owner.RunState.Rng.CombatCardGeneration));
        cards.AddRange(CardFactory.GetDistinctForCombat(orobicAcid.Owner, orobicAcid.Owner.Character.CardPool.GetUnlockedCards(orobicAcid.Owner.UnlockState, orobicAcid.Owner.RunState.CardMultiplayerConstraint).Where<CardModel>((Func<CardModel, bool>) (c => c.Type == CardType.Skill)), 1, orobicAcid.Owner.RunState.Rng.CombatCardGeneration));
        cards.AddRange(CardFactory.GetDistinctForCombat(orobicAcid.Owner, orobicAcid.Owner.Character.CardPool.GetUnlockedCards(orobicAcid.Owner.UnlockState, orobicAcid.Owner.RunState.CardMultiplayerConstraint).Where<CardModel>((Func<CardModel, bool>) (c => c.Type == CardType.Power)), 1, orobicAcid.Owner.RunState.Rng.CombatCardGeneration));
        foreach (CardModel cardModel in cards)
            cardModel.SetToFreeThisTurn();
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) cards, PileType.Hand, true);
    }
}