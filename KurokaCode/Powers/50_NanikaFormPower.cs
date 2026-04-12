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

        List<Type> potionTypes = new()
        {
            typeof(NanikaSaintPotion),
            typeof(NanikaSaintPotion),
            typeof(NanikaSpecialAshwater),
            typeof(NanikaSpecialAttackPotion),
            typeof(NanikaSpecialBeetleJuice),
            typeof(NanikaSpecialBlessingOfTheForge),
            typeof(NanikaSpecialBlockPotion),
            typeof(NanikaSpecialBloodPotion),
            typeof(NanikaSpecialBottledPotential),
            typeof(NanikaSpecialClarity),
            typeof(NanikaSpecialColorlessPotion),
            typeof(NanikaSpecialCosmicConcoction),
            typeof(NanikaSpecialCureAll),
            typeof(NanikaSpecialDexterityPotion),
            typeof(NanikaSpecialDistilledChaos),
        };

        var rng = player.RunState.Rng.CombatPotionGeneration;
        Type selectedType = potionTypes[rng.NextInt(0, potionTypes.Count)];
        
        PotionModel potion = selectedType switch
        {
            var t when t == typeof(NanikaSaintPotion)          => ModelDb.Get<NanikaSaintPotion>(),
            var t when t == typeof(NanikaSpecialAttackPotion)  => ModelDb.Get<NanikaSpecialAttackPotion>(),
            var t when t == typeof(NanikaSpecialAshwater)      => ModelDb.Get<NanikaSpecialAshwater>(),
            var t when t == typeof(NanikaSpecialBeetleJuice)   => ModelDb.Get<NanikaSpecialBeetleJuice>(),
            _ => null
        };

        if (potion == null)
        {
            logger.Error("potion null");
            return;
        }

        await PotionCmd.TryToProcure(potion, player);
        nanikaPower.Flash();
    }
}