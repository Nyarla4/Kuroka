using BaseLib.Extensions;
using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class JingaiJoa() : WitchCard(1, CardType.Attack,
    CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DelusionFactorPower>(2M),
        new DamageVar(8M, ValueProp.Move)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (play.Target == null)
        {
            logger.Warn($"[{this.Id}] 공격 타겟 없음.");
            return;
        }
        
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/strike_kuroka")
            .Execute(choiceContext);

        if(IsWitch) {
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(play.Target)
                .WithHitFx("vfx/strike_kuroka")
                .Execute(choiceContext);
        }
        else {
            await PowerCmd.Apply<DelusionFactorPower>(
                this.Owner.Creature, 
                DynamicVars.Power<DelusionFactorPower>().BaseValue,
                this.Owner.Creature, 
                this
            );
        }
    }

    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(3M);
        DynamicVars.Power<DelusionFactorPower>().UpgradeValueBy(1M);
    }
}