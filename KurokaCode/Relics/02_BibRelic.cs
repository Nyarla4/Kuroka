// 02_BibRelic.cs
// Java: onEquip → RorokaMaxHp += 15
using Kuroka.KurokaCode.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rooms;

namespace Kuroka.KurokaCode.Relics;

public class BibRelic : KurokaRelic
{
    public override RelicRarity Rarity => RelicRarity.Common;

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is not CombatRoom) return;

        // 장착 시 로로카 최대체력 +15
        // RorokaCmd.AddMax를 전투 시작 시점에 호출
        var roroka = Owner.Creature.CombatState!
            .GetCreaturesOnSide(CombatSide.Player)
            .FirstOrDefault(c => c.Monster is Pets.Roroka && c.PetOwner == Owner);

        if (roroka != null)
            await RorokaCmd.AddMax(Owner.Creature.CombatState!, roroka.CombatId, 15M, Owner);
    }
}