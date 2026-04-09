using Kuroka.KurokaCode.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class Kazoo() : KurokaCard(1, CardType.Skill, CardRarity.Common, TargetType.None)
{
    public override bool GainsBlock => true; 
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new("DrawCount", 2M),
        new BlockVar(6M, ValueProp.Move)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, DynamicVars["DrawCount"].BaseValue, this.Owner);

        await CustomCreatureCmd.Defend(
            this.Owner.Creature,
            DynamicVars.Block.BaseValue,
            DynamicVars.Block.Props,
            play,
            "vfx/vfx_block"
        );
    }

    protected override void OnUpgrade() => this.DynamicVars.Block.UpgradeValueBy(2M);
}

