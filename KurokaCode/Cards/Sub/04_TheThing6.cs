
using BaseLib.Extensions;
using Kuroka.KurokaCode.Cards.Abstract;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Cards.Sub;

public class TheThing6() : TheThingChainCard<TheThing7>(CardType.Skill, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DexterityPower>(2M)
    ];

    protected override async Task ExecuteEffect(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<DexterityPower>(
            cardPlay.Target, 
            DynamicVars.Power<DexterityPower>().BaseValue,
            this.Owner.Creature, 
            this
        );
    }

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
        [HoverTipFactory.FromPower<DexterityPower>()];
}