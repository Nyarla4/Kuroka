using Kuroka.KurokaCode.Commands;
using Kuroka.KurokaCode.Pets;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards;

public class NandeWatashi() : KurokaCard(1, CardType.Skill,
    CardRarity.Uncommon, TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("Roroka", 4M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {        
        Creature? roroka = null;
        foreach (var creature in this.CombatState!.GetCreaturesOnSide(CombatSide.Player))
        {
            if (creature.Monster is Roroka && creature.PetOwner == this.Owner)
            {
                roroka = creature;
                break;
            }
        }

        uint? rorokaId = roroka?.CombatId;
        await RorokaCmd.Heal(this.CombatState, rorokaId, DynamicVars["Roroka"].BaseValue, this.Owner);

        foreach (Creature enemy in CombatState.GetCreaturesOnSide(CombatSide.Enemy))
        {
            if (enemy.GetPower<MajinaiPower>() != null)
            {
                await RorokaCmd.Heal(this.CombatState, rorokaId, DynamicVars["Roroka"].BaseValue, this.Owner);
                break;
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Roroka"].UpgradeValueBy(1M);
    }
}