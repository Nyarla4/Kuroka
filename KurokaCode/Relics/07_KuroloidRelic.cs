// 07_KuroloidRelic.cs
// Java: atBattleStart → 모든 적에게 Artifact(1) / onEquip → +1 에너지
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Relics;

public class KuroloidRelic : KurokaRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(1)
    ];
    
    public override async Task BeforeCombatStart()
    {
        this.Flash();
        foreach (var enemy in Owner.Creature.CombatState!
                     .GetCreaturesOnSide(CombatSide.Enemy)
                     .Where(c => c.IsAlive))
        {
            await PowerCmd.Apply<ArtifactPower>(enemy, 1, Owner.Creature, null);
        }
    }
    public override Decimal ModifyMaxEnergy(Player player, Decimal amount)
    {
        return player != this.Owner ? amount : amount + this.DynamicVars.Energy.BaseValue;
    }
}