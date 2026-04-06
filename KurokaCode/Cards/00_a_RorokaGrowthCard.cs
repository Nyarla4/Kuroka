using BaseLib.Extensions;
using Kuroka.KurokaCode.Commands;
using Kuroka.KurokaCode.Pets;
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
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("RorokaGrowth", 5M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Creature roroka = GetRoroka();

        uint? rorokaId = roroka?.CombatId;
        await RorokaCmd.AddMax(this.CombatState, rorokaId, DynamicVars["RorokaGrowth"].BaseValue, this.Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["RorokaGrowth"].UpgradeValueBy(6M);
    }

    private Creature GetRoroka()
    {
        foreach (var creature in this.CombatState.GetCreaturesOnSide(CombatSide.Player))
        {
            if (creature.Monster is Roroka && creature.PetOwner == this.Owner)
            {
                return creature;
            }
        }

        return null;
    }
}