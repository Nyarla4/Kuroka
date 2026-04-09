using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class Possible() : WitchCard(1, CardType.Attack,
    CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DelusionFactorPower>(1M),
        new DamageVar(3M, ValueProp.Move),
        new("CommonRepeat", 4M),
        new("WitchRepeat", 6M)
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

        for(int i = 0 ; i < (int)(IsWitch ? DynamicVars["WitchRepeat"].BaseValue : DynamicVars["CommonRepeat"].BaseValue) ; i++) {
            var before = play.Target.CurrentHp;
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(play.Target)
                .WithHitFx("vfx/strike_kuroka")
                .Execute(choiceContext);
            var after = play.Target.CurrentHp;
            if(!IsWitch) {
                if(after < before) {
                    await PowerCmd.Apply<DelusionFactorPower>(
                        this.Owner.Creature, 
                        DynamicVars.Power<DelusionFactorPower>().BaseValue,
                        this.Owner.Creature, 
                        this
                    );
                }
            }
        }
    }

    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(1M);
    }
}