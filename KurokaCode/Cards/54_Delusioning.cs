using BaseLib.Extensions;
using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards;

public class Delusioning() : WitchCard(2, CardType.Power,
    CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DelusioningPower>(1M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);

        await PowerCmd.Apply<DelusioningPower>(
            this.Owner.Creature, 
            DynamicVars.Power<DelusioningPower>().BaseValue,
            this.Owner.Creature, 
            this
        );
    }

    protected override void OnUpgrade() { 
        this.EnergyCost.UpgradeBy(-1);
    }
}