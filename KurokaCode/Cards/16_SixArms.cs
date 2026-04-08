using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class SixArms() : WitchCard(1, CardType.Attack,
    CardRarity.Common, TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar("common", 6M, ValueProp.Move),
        new DamageVar("witch", 10M, ValueProp.Move)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        PowerModel? pow = Owner.Creature.GetPower<DelusionFactorPower>();
        bool isWitch = false;
        if (pow is { Amount: >= 10 })
        {
            isWitch = true;
        }

        if (play.Target == null)
        {
            logger.Warn($"[{this.Id}] 공격 타겟 없음.");
            return;
        }
        this.DynamicVars.Damage.BaseValue = isWitch ? (decimal)pow.Amount : DynamicVars["common"].BaseValue;
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/strike_kuroka")
            .Execute(choiceContext);
        // Headbutt, Aggression, Dredge 참조해서 버린 카드 더미에서 카드 가져오는 거 확인
    }

    protected override void OnUpgrade() => this.RemoveKeyword(CardKeyword.Exhaust);
}