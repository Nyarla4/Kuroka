using Kuroka.KurokaCode.Powers;
using Kuroka.KurokaCode.Cards.Abstract;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class Flick() : WitchCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar("Common", 10M, ValueProp.Move),
        new DamageVar("Witch", 10M, ValueProp.Move),
        new PowerVar<StrengthPower>(1M)
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

        int multiplier = 1;
        PowerModel pow = Owner.Creature.GetPower<DelusionFactorPower>();
        if(pow != null) {
            if(pow.Amount >= 3) {
                pow.Amount -= 3;
                multiplier = 2;
            }
        }

        await DamageCmd.Attack((IsWitch ? DynamicVars["Witch"].BaseValue : DynamicVars["Common"].BaseValue) * multiplier)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Witch"].UpgradeValueBy(5M);
        DynamicVars["Common"].UpgradeValueBy(5M);
    }
}