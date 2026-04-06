using BaseLib.Extensions;
using Kuroka.KurokaCode.Commands;
using Kuroka.KurokaCode.Pets;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards;

public class RorokaGrowthCard() : KurokaCard(2, CardType.Skill,
    CardRarity.Basic, TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<RorokaGrowthPower>(5M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Creature roroka = null;
        foreach (var creature in this.CombatState.GetCreaturesOnSide(CombatSide.Player))
        {
            if (creature.Monster is Roroka && creature.PetOwner == this.Owner)
            {
                roroka = creature;
                break;
            }
        }

        uint? rorokaId = roroka?.CombatId;
        await RorokaCmd.SummonOrHeal(this.CombatState, rorokaId, DynamicVars.Power<RorokaGrowthPower>().BaseValue, this.Owner);
        
        await PowerCmd.Apply<RorokaGrowthPower>(
            roroka, 
            DynamicVars.Power<RorokaGrowthPower>().BaseValue,
            this.Owner.Creature, 
            this
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<RorokaGrowthPower>().UpgradeValueBy(6M);
    }
}