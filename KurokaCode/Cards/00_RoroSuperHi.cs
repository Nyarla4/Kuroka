using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards;

public class RoroSuperHi() : RorokaCard(2, CardType.Skill,
    CardRarity.Ancient, TargetType.None)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("Roroka", 30M)];
    
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