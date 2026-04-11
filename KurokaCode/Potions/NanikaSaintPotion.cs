using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Potions;

public class NanikaSaintPotion : KurokaPotion
{
    public override PotionRarity Rarity => PotionRarity.Rare;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.AnyPlayer;
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        PotionModel.AssertValidForTargetedPotion(target);
        foreach (PowerModel power in target.Powers)
        {
            if (power.Type.Equals(PowerType.Debuff))
            {
                await PowerCmd.Remove(power);
            }
        }
    }
}