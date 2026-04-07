using Kuroka.KurokaCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Cards;

public class SikabaneOdori() : KurokaCard(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(0M, ValueProp.Move),
        new DamageVar("MinDamage", 4M, ValueProp.Move),
        new DamageVar("MaxDamage", 12M, ValueProp.Move),
        new ("Increase", 2M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {

        var rng = this.Owner.RunState.Rng.Niche;
        var damage = rng.NextInt((int)DynamicVars["MinDamage"].BaseValue, (int)DynamicVars["MaxDamage"].BaseValue);
        this.DynamicVars.Damage.BaseValue = damage;
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(this.CombatState!)
            .WithAttackerAnim("Cast", 0.5f)
            .Execute(choiceContext);
        
        if (DynamicVars["MinDamage"] is DamageVar damageVarMin)
        {
            damageVarMin.BaseValue += DynamicVars["Increase"].BaseValue;
        }
        if (DynamicVars["MaxDamage"] is DamageVar damageVarMax)
        {
            damageVarMax.BaseValue += DynamicVars["Increase"].BaseValue;
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Increase"].UpgradeValueBy(3M);
    }
}