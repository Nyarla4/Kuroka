using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards;

public class FallToHell() : KurokaCard(2, CardType.Skill,
    CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("multiplier", 1M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var amount = play.Target.GetPower<MajinaiPower>().Amount * DynamicVars["multiplier"].BaseValue;

        await PowerCmd.Apply<MajinaiPower>(
            play.Target, 
            amount,
            this.Owner.Creature, 
            this
        );
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