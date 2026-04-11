using Kuroka.KurokaCode.Commands;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class MajinaiShield() : KurokaCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    public override bool GainsBlock => true;
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(3M, ValueProp.Move),
        new("ExtraBlock", 1M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(
            Owner.Creature,
            this.DynamicVars.Block,
            play);

        if (play.Target == null)
        {
            logger.Warn($"[{this.Id}] 타겟 없음.");
            return;
        }

        PowerModel? pow = play.Target.GetPower<MajinaiPower>();
        if (pow != null)
        {
            await CreatureCmd.GainBlock(
                this.Owner.Creature,
                DynamicVars["ExtraBlock"].BaseValue * pow.Amount,
                this.DynamicVars.Block.Props,
                play
            );
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ExtraBlock"].UpgradeValueBy(1M);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<MajinaiPower>()
    ];
}