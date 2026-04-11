
using Kuroka.KurokaCode.Cards.Abstract;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards.Sub;

public class TheThing8() : TheThingChainCard<TheThing9>(CardType.Skill, TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(5M, ValueProp.Move)
    ];
    
    protected override async Task ExecuteEffect(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(
            this.Owner.Creature, 
            this.DynamicVars.Block.BaseValue, 
            this.DynamicVars.Block.Props, 
            cardPlay
        );
    }
}