using BaseLib.Extensions;
using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class ThreeMeter() : WitchCard(2, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(16M, ValueProp.Move),
        new PowerVar<ThreeMeterPower>(1M)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // 방어도 처리
        await CreatureCmd.GainBlock(
            this.Owner.Creature, 
            this.DynamicVars.Block.BaseValue, 
            this.DynamicVars.Block.Props, 
            cardPlay
        );

        await PowerCmd.Apply<ThreeMeterPower>(
                this.Owner.Creature, 
                DynamicVars.Power<ThreeMeterPower>().BaseValue + (IsWitch ? 1M : 0M),
                this.Owner.Creature, 
                this
        );
    }

    protected override void OnUpgrade() => this.DynamicVars.Power<ThreeMeterPower>().UpgradeValueBy(1M);

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        .. base.ExtraHoverTips, HoverTipFactory.FromPower<ThreeMeterPower>()
    ];
}