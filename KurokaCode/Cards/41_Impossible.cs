using BaseLib.Extensions;
using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class Impossible() : WitchCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(10M, ValueProp.Move),
        new("CommonLose", 2M),
        new("WitchLose", 4M),
        new PowerVar<BlurPower>(1M)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        PowerModel pow = Owner.Creature.GetPower<DelusionFactorPower>();

        if(pow == null) {
            return;
        }

        Decimal loseAmount = Math.Min((IsWitch ? DynamicVars["WitchLose"].BaseValue : DynamicVars["CommonLose"].BaseValue), pow.Amount);

        await PowerCmd.Apply<DelusionFactorPower>(
                this.Owner.Creature, 
                -loseAmount,
                this.Owner.Creature, 
                this
            );

        await CreatureCmd.GainBlock(
            this.Owner.Creature, 
            (IsWitch ? 30M : this.DynamicVars.Block.BaseValue * loseAmount), 
            this.DynamicVars.Block.Props, 
            cardPlay
        );

        if(IsWitch) {
            await PowerCmd.Apply<BlurPower>(
                this.Owner.Creature, 
                DynamicVars.Power<BlurPower>().BaseValue,
                this.Owner.Creature, 
                this
            );
        }
    }

    protected override void OnUpgrade() {
        DynamicVars.Power<BlurPower>().UpgradeValueBy(1M);
        DynamicVars.Block.UpgradeValueBy(4M);
    }
}