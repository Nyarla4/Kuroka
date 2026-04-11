using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace Kuroka.KurokaCode.Powers;

public class MajinaiStrengthPower : KurokaPower
{
    
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public static int GetMajinaiedCreatures(IReadOnlyList<Creature> creatures)
    {
        return creatures.Where(c => c.GetPower<MajinaiPower>() != null).ToList().Count;
    }
    
    public override Decimal ModifyDamageAdditive(
        Creature? target,
        Decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        return this.Owner != dealer || !props.IsPoweredAttack() ? 0M : GetMajinaiedCreatures(CombatState.GetCreaturesOnSide(CombatSide.Enemy));
    }
    
    public override async Task AfterPowerAmountChanged(
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        if (power is not MajinaiPower) return;
        if (power.Owner.Side != CombatSide.Enemy) return;

        await RecalculateFromExternal(null);
    }

    public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
    {
        if (creature.Side != CombatSide.Enemy) return;
        await RecalculateFromExternal(choiceContext);
    }

    public async Task RecalculateFromExternal(PlayerChoiceContext? choiceContext)
    {
        int newCount = GetMajinaiedCreatures(
            CombatState.GetCreaturesOnSide(CombatSide.Enemy));

        if (newCount <= 0)
        {
            await PowerCmd.Remove<MajinaiStrengthPower>(this.Owner);
            return;
        }

        decimal diff = newCount - this.Amount;
        if (diff != 0)
            await PowerCmd.ModifyAmount(this, diff, null, null);
    }
}