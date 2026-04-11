using BaseLib.Extensions;
using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards.Sub;
public class TheThing2() : TheThingChainCard<TheThing3>(CardType.Skill, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<DelusionFactorPower>(1M)];

    protected override async Task ExecuteEffect(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<DelusionFactorPower>(
            this.Owner.Creature,
            DynamicVars.Power<DelusionFactorPower>().BaseValue,
            this.Owner.Creature, this);
    }

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
        [HoverTipFactory.FromPower<DelusionFactorPower>()];
}