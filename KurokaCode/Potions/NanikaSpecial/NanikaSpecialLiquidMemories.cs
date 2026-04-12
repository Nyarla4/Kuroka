using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialLiquidMemories : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialLiquidMemories liquidMemories = this;
        NCombatRoom.Instance?.PlaySplashVfx(liquidMemories.Owner.Creature, new Color(Colors.Blue));
        CardModel card = (await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(liquidMemories.Owner).Cards, liquidMemories.Owner, new CardSelectorPrefs(liquidMemories.SelectionScreenPrompt, 1))).FirstOrDefault<CardModel>();
        if (card == null)
            return;
        card.SetToFreeThisTurn();
        CardPileAddResult cardPileAddResult = await CardPileCmd.Add(card, PileType.Hand);
    }
}