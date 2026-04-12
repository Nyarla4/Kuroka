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
        
        List<PotionModel> potions = 
        [
            ModelDb.Potion<NanikaSaintPotion>(),
            ModelDb.Potion<NanikaSpecialAshwater>(),
            ModelDb.Potion<NanikaSpecialAttackPotion>(),
            ModelDb.Potion<NanikaSpecialBeetleJuice>(),
            ModelDb.Potion<NanikaSpecialBlessingOfTheForge>(),
            ModelDb.Potion<NanikaSpecialBlockPotion>(),
            ModelDb.Potion<NanikaSpecialBloodPotion>(),
            ModelDb.Potion<NanikaSpecialBottledPotential>(),
            ModelDb.Potion<NanikaSpecialClarity>(),
            ModelDb.Potion<NanikaSpecialColorlessPotion>(),
            ModelDb.Potion<NanikaSpecialCosmicConcoction>(),
            ModelDb.Potion<NanikaSpecialCureAll>(),
            ModelDb.Potion<NanikaSpecialDexterityPotion>(),
            ModelDb.Potion<NanikaSpecialDistilledChaos>(),
            ModelDb.Potion<NanikaSpecialDropletOfPrecognition>(),
            ModelDb.Potion<NanikaSpecialDuplicator>(),
            ModelDb.Potion<NanikaSpecialEnergyPotion>(),
            ModelDb.Potion<NanikaSpecialEntropicBrew>(),
            ModelDb.Potion<NanikaSpecialExplosiveAmpoule>(),
            ModelDb.Potion<NanikaSpecialFairyInABottle>(),
            ModelDb.Potion<NanikaSpecialFirePotion>(),
            ModelDb.Potion<NanikaSpecialFlexPotion>(),
            ModelDb.Potion<NanikaSpecialFortifier>(),
            ModelDb.Potion<NanikaSpecialFruitJuice>(),
            ModelDb.Potion<NanikaSpecialFyshOil>(),
            ModelDb.Potion<NanikaSpecialGamblersBrew>(),
            ModelDb.Potion<NanikaSpecialGhostInAJar>(),
            ModelDb.Potion<NanikaSpecialGigantificationPotion>(),
            ModelDb.Potion<NanikaSpecialHeartOfIron>(),
            ModelDb.Potion<NanikaSpecialLiquidBronze>(),
            ModelDb.Potion<NanikaSpecialLiquidMemories>(),
            ModelDb.Potion<NanikaSpecialLuckyTonic>(),
            ModelDb.Potion<NanikaSpecialMazalethsGift>(),
            ModelDb.Potion<NanikaSpecialOrobicAcid>(),
            ModelDb.Potion<NanikaSpecialPotionOfBinding>(),
            ModelDb.Potion<NanikaSpecialPowderedDemise>(),
            ModelDb.Potion<NanikaSpecialPowerPotion>(),
            ModelDb.Potion<NanikaSpecialRadiantTincture>(),
            ModelDb.Potion<NanikaSpecialRegenPotion>(),
            ModelDb.Potion<NanikaSpecialShacklingPotion>(),
            ModelDb.Potion<NanikaSpecialShipInABottle>(),
            ModelDb.Potion<NanikaSpecialSkillPotion>(),
            ModelDb.Potion<NanikaSpecialSneckoOil>(),
            ModelDb.Potion<NanikaSpecialSoldiersStew>(),
            ModelDb.Potion<NanikaSpecialSpeedPotion>(),
            ModelDb.Potion<NanikaSpecialStableSerum>(),
            ModelDb.Potion<NanikaSpecialStrengthPotion>(),
            ModelDb.Potion<NanikaSpecialSwiftPotion>(),
            ModelDb.Potion<NanikaSpecialTouchOfInsanity>(),
            ModelDb.Potion<NanikaSpecialVulnerablePotion>(),
            ModelDb.Potion<NanikaSpecialWeakPotion>()
        ];
        var rng = player.RunState.Rng.CombatPotionGeneration;

        // CreateRandomPotion은 rarity 필터링으로 인해 커스텀 풀에 부적합
        // rng.NextItem으로 직접 선택 후 ToMutable()로 mutable 인스턴스 생성
        PotionModel? potion = rng.NextItem(potions)?.ToMutable();

        if (potion == null)
        {
            logger.Error("potion null");
            return;
        }

        await PotionCmd.TryToProcure(potion, player);
        nanikaPower.Flash();
    }
}