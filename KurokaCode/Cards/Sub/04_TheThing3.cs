
using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Commands;
using Kuroka.KurokaCode.Pets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards.Sub;

public class TheThing3() : TheThingChainCard<TheThing4>(CardType.Skill, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("Roroka", 4M)];

    protected override async Task ExecuteEffect(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Creature? roroka = GetRoroka();

        uint? rorokaId = roroka?.CombatId;
        await RorokaCmd.Heal(this.CombatState, rorokaId, DynamicVars["Roroka"].BaseValue, this.Owner);
    }
}