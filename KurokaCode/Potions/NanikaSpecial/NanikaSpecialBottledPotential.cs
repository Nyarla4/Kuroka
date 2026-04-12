using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialBottledPotential :NanikaSpecialPotion
{   
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(3)
    ];
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialBottledPotential bottledPotential = this;
        PotionModel.AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color("e645db"));
        IReadOnlyList<CardPileAddResult> cardPileAddResultList = await CardPileCmd.Add((IEnumerable<CardModel>) PileType.Hand.GetPile(target.Player).Cards, PileType.Draw);
        await CardPileCmd.Shuffle(choiceContext, target.Player);
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, bottledPotential.DynamicVars.Cards.BaseValue, target.Player);
    }
}