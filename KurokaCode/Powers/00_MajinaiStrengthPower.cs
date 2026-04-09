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
}