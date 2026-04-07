using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class TwoFairy() : KurokaCard(0, CardType.Attack,
    CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(2M, ValueProp.Move),
        new PowerVar<MajinaiPower>(2M),
        new PowerVar<WeakPower>(2M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        for ( int i = 0 ; i < 2 ; i ++) {
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(play.Target)
                //.WithHitFx("vfx/strike_kuroka")
                .Execute(choiceContext);
            await PowerCmd.Apply<MajinaiPower>(
                play.Target, 
                DynamicVars.Power<MajinaiPower>().BaseValue,
                this.Owner.Creature, 
                this);
            if(this.IsUpgraded) {
                await PowerCmd.Apply<WeakPower>(
                    play.Target, 
                    DynamicVars.Power<WeakPower>().BaseValue,
                    this.Owner.Creature, 
                    this);
            }
        }
    }

    protected override void OnUpgrade()
    {
        
    }
}