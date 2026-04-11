using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class DestinyIsMe() : KurokaCard(2, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override bool GainsBlock => true; 
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(16M, ValueProp.Move),
        new PowerVar<DestinyPower>(6M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(
            Owner.Creature,
            DynamicVars.Block.BaseValue,
            DynamicVars.Block.Props,
            play);

        await PowerCmd.Apply<DestinyPower>(
            Owner.Creature,
            DynamicVars.Power<DestinyPower>().BaseValue,
            this.Owner.Creature, 
            this
        );
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Power<DestinyPower>().UpgradeValueBy(2M);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<MajinaiPower>()
    ];
}