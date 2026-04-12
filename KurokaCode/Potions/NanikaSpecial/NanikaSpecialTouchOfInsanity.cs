using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialTouchOfInsanity : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialTouchOfInsanity source = this;
        CardSelectorPrefs prefs = new CardSelectorPrefs(source.SelectionScreenPrompt, 1);
        (await CardSelectCmd.FromHand(choiceContext, source.Owner, prefs, (Func<CardModel, bool>) (c => c.CostsEnergyOrStars(false) || c.CostsEnergyOrStars(true)), (AbstractModel) source)).FirstOrDefault<CardModel>()?.SetToFreeThisCombat();
    }
}