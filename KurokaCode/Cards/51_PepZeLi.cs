using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class PepZeLi() : KurokaCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(7M, ValueProp.Move),
        new PowerVar<PepZeLiPower>(1M),
        new EnergyVar(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if(false) { // 추후 마요/라떼 체크
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(Owner.Creature)
                .WithHitFx("vfx/strike_kuroka")
                .Execute(choiceContext);
        }
        await CreatureCmd.GainBlock(
            this.Owner.Creature, 
            this.DynamicVars.Block.BaseValue, 
            this.DynamicVars.Block.Props, 
            cardPlay
        );
        await PowerCmd.Apply<PepZeLiPower>(
                this.Owner.Creature, 
                DynamicVars.Power<PepZeLiPower>().BaseValue,
                this.Owner.Creature, 
                this
        );
    }

    protected override void OnUpgrade() {
        DynamicVars.Power<PepZeLiPower>().UpgradeValueBy(1M);
        DynamicVars.Energy.UpgradeValueBy(1M);
    }
}