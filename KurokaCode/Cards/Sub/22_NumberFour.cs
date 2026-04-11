using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards.Sub;

public class NumberFour() : KurokaCard(0, CardType.Skill, CardRarity.Token, TargetType.Self)
{
    public override bool GainsBlock => true;
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(4M, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(
            this.Owner.Creature, 
            this.DynamicVars.Block.BaseValue, 
            this.DynamicVars.Block.Props, 
            play
        );

    }

    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Retain);
    }
}