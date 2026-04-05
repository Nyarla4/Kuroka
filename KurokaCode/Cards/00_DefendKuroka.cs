using Kuroka.KurokaCode.Cards;
using Kuroka.KurokaCode.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

  
public class DefendKuroka() : KurokaCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override HashSet<CardTag> CanonicalTags
    {
        get => new HashSet<CardTag>() { CardTag.Defend };
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(5M, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // 커스텀 소리와 기본 이펙트를 조합하여 실행
        await CustomCreatureCmd.GainBlockWithCustomVfx(
            this.Owner.Creature, 
            this.DynamicVars.Block.BaseValue, 
            this.DynamicVars.Block.Props, 
            cardPlay, 
            "audio/defend_kuroka",
            "vfx/vfx_block"
        );
    }

    protected override void OnUpgrade() => this.DynamicVars.Block.UpgradeValueBy(3M);
}