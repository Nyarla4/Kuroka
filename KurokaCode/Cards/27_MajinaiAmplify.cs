using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Cards;

public class MajinaiAmplify() : KurokaCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<MajinaiAmplifyPower>(3M),
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
}