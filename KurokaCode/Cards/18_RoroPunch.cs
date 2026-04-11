using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Pets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class RoroPunch() : RorokaCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(0M, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {        
        if (play.Target == null)
        {
            logger.Warn($"[{this.Id}] 공격 타겟 없음.");
            return;
        }
        
        Creature? roroka = GetRoroka();

        int damage = 0;
        if(roroka != null) {
            damage = (IsUpgraded ? roroka.CurrentHp : roroka.CurrentHp / 2);
        }

        DynamicVars.Damage.BaseValue = damage;

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        
    }
}