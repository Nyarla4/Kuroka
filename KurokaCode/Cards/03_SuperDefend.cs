using Kuroka.KurokaCode.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class SuperDefend() : KurokaCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override HashSet<CardTag> CanonicalTags
    {
        get => new HashSet<CardTag>() { CardTag.Defend };
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(8M, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // 커스텀 사운드
        CustomCreatureCmd.PlayAudio("audio/super_defend");
        
        // 방어도 처리
        await CustomCreatureCmd.Defend(
            this.Owner.Creature, 
            this.DynamicVars.Block.BaseValue, 
            this.DynamicVars.Block.Props, 
            cardPlay, 
            "vfx/vfx_block"
        );
    }

    protected override void OnUpgrade() => this.DynamicVars.Block.UpgradeValueBy(3M);
}