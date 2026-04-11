
using BaseLib.Extensions;
using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards.Sub;

public class TheThing5() : TheThingChainCard<TheThing6>(CardType.Skill, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<MajinaiPower>(4M)];

    protected override async Task ExecuteEffect(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<MajinaiPower>(
            cardPlay.Target, 
            DynamicVars.Power<MajinaiPower>().BaseValue,
            this.Owner.Creature, 
            this
        );
    }

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
        [HoverTipFactory.FromPower<MajinaiPower>()];
}