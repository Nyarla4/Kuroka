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

public class NanikaSpecialAshwater :NanikaSpecialPotion
{
    public readonly Color _tint = new Color("83ebdf");
    
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {   
        NanikaSpecialAshwater source = this;
        NCombatRoom.Instance?.PlaySplashVfx(source.Owner.Creature, source._tint);
        CardSelectorPrefs prefs = new CardSelectorPrefs(source.SelectionScreenPrompt, 0, 3);
        foreach (CardModel card in await CardSelectCmd.FromHand(choiceContext, source.Owner, prefs, (Func<CardModel, bool>) null, (AbstractModel) source))
            await CardCmd.Exhaust(choiceContext, card);
    }
}