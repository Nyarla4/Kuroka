using Kuroka.KurokaCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

  
public class StrikeKuroka() : KurokaCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    
    private static readonly Logger logger = new Logger("Kuroka", LogType.Generic);
    
    protected override HashSet<CardTag> CanonicalTags
    {
        get => new HashSet<CardTag>() { CardTag.Strike };
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6M, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        logger.Info(PortraitPath);
        StrikeKuroka card = this;
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).Targeting(play.Target).WithHitFx("vfx/strike_kuroka").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3M);
    }
}