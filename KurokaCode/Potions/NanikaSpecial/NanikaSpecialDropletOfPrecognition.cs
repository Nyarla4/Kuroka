using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialDropletOfPrecognition : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialDropletOfPrecognition dropletOfPrecognition = this;
        CardModel card = 
            (await CardSelectCmd.FromSimpleGrid(
                choiceContext,
                (IReadOnlyList<CardModel>) PileType.Draw.GetPile(dropletOfPrecognition.Owner)
                    .Cards
                    .OrderBy<CardModel, CardRarity>((Func<CardModel, CardRarity>) (c => c.Rarity))
                    .ThenBy<CardModel, ModelId>((Func<CardModel, ModelId>) (c => c.Id))
                    .ToList<CardModel>(),
                dropletOfPrecognition.Owner,
                new CardSelectorPrefs(dropletOfPrecognition.SelectionScreenPrompt, 1)))
            .FirstOrDefault<CardModel>();
        if (card == null)
            return;
        CardPileAddResult cardPileAddResult = await CardPileCmd.Add(card, PileType.Hand);
    }
}