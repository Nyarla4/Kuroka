using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class MajinaiChop() : KurokaCard(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(4M, ValueProp.Move),
        new ("multiplier", 2M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {        
        if (play.Target == null)
        {
            logger.Warn($"[{this.Id}] 공격 타겟 없음.");
            return;
        }

        if(play.Target.GetPower<MajinaiPower>() != null) {
            DynamicVars.Damage.BaseValue *= DynamicVars["multiplier"].BaseValue;
        }
// 스네코 추가피해는 나중에
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["multiplier"].UpgradeValueBy(1M);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<MajinaiPower>()
    ];
}