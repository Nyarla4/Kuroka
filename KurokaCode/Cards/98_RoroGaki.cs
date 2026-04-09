using BaseLib.Extensions;
using Kuroka.KurokaCode.Pets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Cards;

public class RoroGaki() : KurokaCard(1, CardType.Skill,
    CardRarity.Event, TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags
    {
        get => new HashSet<CardTag>() { Roroka.RorokaTag };
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Turns", 2M),
        new PowerVar<StrengthPower>(4M)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);

        await PowerCmd.Apply<StrengthPower>(
            this.Owner.Creature, 
            DynamicVars.Power<StrengthPower>().BaseValue,
            this.Owner.Creature, 
            this
        );
        
        //DynamicVars["Turns"].BaseValue턴 후 힘을 DynamicVars.Power<StrengthPower>().BaseValue 잃도록 처리
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}