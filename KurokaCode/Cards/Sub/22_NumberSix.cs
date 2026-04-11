using Kuroka.KurokaCode.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards.Sub;

public class NumberSix() : KurokaCard(0, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6M, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (play.Target == null)
        {
            logger.Warn($"[{this.Id}] 공격 타겟 없음.");
            return;
        }
        
        CustomCreatureCmd.PlayAudio("audio/super_strike");

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/silent_strike_kuroka")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Retain);
    }
}