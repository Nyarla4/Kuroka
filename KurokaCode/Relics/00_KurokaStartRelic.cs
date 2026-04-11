using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace Kuroka.KurokaCode.Relics;

public class KurokaStartRelic : KurokaRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;
 
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<MajinaiPower>(1M),
        new PowerVar<DelusionFactorPower>(1M)
    ];
    
    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (!(room is CombatRoom))
            return;
        
        var alive = Owner.Creature.CombatState!
            .GetCreaturesOnSide(CombatSide.Enemy)
            .Where(c => c.IsAlive)
            .ToList();

        if (alive.Count == 0) return;

        var rng = this.Owner.RunState.Rng.Niche;
        Creature target = alive[rng.NextInt(0, alive.Count)];
        await PowerCmd.Apply<MajinaiPower>(
            target,
            DynamicVars.Power<MajinaiPower>().BaseValue,
            this.Owner.Creature,
            null
        );
        
        await PowerCmd.Apply<DelusionFactorPower>(
            this.Owner.Creature, 
            DynamicVars.Power<DelusionFactorPower>().BaseValue,
            this.Owner.Creature, 
            null
        );
    }
}