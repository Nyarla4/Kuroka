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

public class NanikaSpecialGamblersBrew : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialGamblersBrew source = this;
        NCombatRoom.Instance?.PlaySplashVfx(source.Owner.Creature, new Color("a19f91"));
        List<CardModel> list = (await CardSelectCmd.FromHandForDiscard(choiceContext, source.Owner, new CardSelectorPrefs(source.SelectionScreenPrompt, 0, 3), (Func<CardModel, bool>) null, (AbstractModel) source)).ToList<CardModel>();
        await CardCmd.DiscardAndDraw(choiceContext, (IEnumerable<CardModel>) list, list.Count);
    }
}