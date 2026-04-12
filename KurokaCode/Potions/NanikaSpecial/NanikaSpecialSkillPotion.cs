using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialSkillPotion : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialSkillPotion skillPotion = this;
        CardModel card = await CardSelectCmd.FromChooseACardScreen(choiceContext, (IReadOnlyList<CardModel>) CardFactory.GetDistinctForCombat(skillPotion.Owner, skillPotion.Owner.Character.CardPool.GetUnlockedCards(skillPotion.Owner.UnlockState, skillPotion.Owner.RunState.CardMultiplayerConstraint).Where<CardModel>((Func<CardModel, bool>) (c => c.Type == CardType.Skill)), 2, skillPotion.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>(), skillPotion.Owner, true);
        if (card == null)
            return;
        card.SetToFreeThisTurn();
        CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
    }
}