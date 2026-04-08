using BaseLib.Extensions;
using Kuroka.KurokaCode.Commands;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class NoData() : KurokaCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(5M, ValueProp.Move),
        new PowerVar<MajinaiPower>(7M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        logger.Info(PortraitPath ?? "PortraitPath is null");
        
        if (play.Target == null)
        {
            logger.Warn($"[{this.Id}] 타겟 없음.");
            return;
        }
        // NoData
        CustomCreatureCmd.PlayAudio("audio/defend_kuroka");
        
        // 방어도 처리
        await CreatureCmd.GainBlock(
            Owner.Creature,
            this.DynamicVars.Block,
            play);

        await PowerCmd.Apply<MajinaiPower>(
            play.Target, 
            DynamicVars.Power<MajinaiPower>().BaseValue,
            this.Owner.Creature, 
            this
        );
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(3M);
        this.DynamicVars.Power<MajinaiPower>().UpgradeValueBy(3M);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<MajinaiPower>()
    ];
}