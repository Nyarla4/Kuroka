using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Commands;
using Kuroka.KurokaCode.Pets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards;

public class RoroHi() : RorokaCard(2, CardType.Skill,
    CardRarity.Basic, TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("Roroka", 15M)];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        CustomCreatureCmd.PlayAudio("audio/roro_hi");
        Creature? roroka = GetRoroka();
        uint? rorokaId = roroka?.CombatId;
        await RorokaCmd.Heal(this.CombatState, rorokaId, DynamicVars["Roroka"].BaseValue, this.Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Roroka"].UpgradeValueBy(6M);
    }
}