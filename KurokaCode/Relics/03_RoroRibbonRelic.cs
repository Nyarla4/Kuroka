// 03_RoroRibbonRelic.cs
// Java: onPlayerEndTurn → AddRorokaHPAction(3)
using Kuroka.KurokaCode.Commands;
using Kuroka.KurokaCode.Pets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Kuroka.KurokaCode.Relics;

public class RoroRibbonRelic : KurokaRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player) return;

        var roroka = Owner.Creature.CombatState!
            .GetCreaturesOnSide(CombatSide.Player)
            .FirstOrDefault(c => c.Monster is Roroka && c.PetOwner == Owner);

        if (roroka == null) return;

        this.Flash();
        await RorokaCmd.Heal(Owner.Creature.CombatState!, roroka.CombatId, 3M, Owner);
    }
}