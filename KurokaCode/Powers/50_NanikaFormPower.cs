using Kuroka.KurokaCode.Potions;
using Kuroka.KurokaCode.Potions.NanikaSpecial;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Powers;

public class NanikaFormPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task BeforeHandDraw(
        Player player,
        PlayerChoiceContext choiceContext,
        CombatState combatState)
    {
        NanikaFormPower nanikaPower = this;
        if (player != nanikaPower.Owner.Player)
            return;

        List<PotionModel> potions = new()
        {
            ModelDb.Potion<NanikaSaintPotion>(),
            ModelDb.Potion<NanikaSpecialAttackPotion>(),
            ModelDb.Potion<NanikaSpecialAshwater>(),
            ModelDb.Potion<NanikaSpecialBeetleJuice>(),
        };

        var generatedPotions = PotionFactory.CreateRandomPotion(potions, 1, player.RunState.Rng.CombatPotionGeneration);
    
        PotionModel targetPotion = generatedPotions?[0];

        if (targetPotion == null)
        {
            logger.Error("targetPotion null");
            return;
        }

        await PotionCmd.TryToProcure(targetPotion, player);
        nanikaPower.Flash();
    }
}