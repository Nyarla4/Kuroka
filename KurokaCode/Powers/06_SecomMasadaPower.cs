using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace Kuroka.KurokaCode.Powers;

public class SecomMasadaPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new("Delusion", 0M)
    ];

    public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        PowerModel pow = Owner.GetPower<DelusionFactorPower>();
        if (pow != null)
        {
            DynamicVars["Delusion"].BaseValue = pow.Amount;   
        }
        return base.AfterPowerAmountChanged(power, amount, applier, cardSource);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player)
            return;
        IEnumerable<DamageResult> damageResults = 
            await CreatureCmd.Damage((PlayerChoiceContext) new ThrowingPlayerChoiceContext(),
                (IEnumerable<Creature>) Owner.CombatState.HittableEnemies,
                (Decimal) DynamicVars["Delusion"].BaseValue + Amount,
                ValueProp.Unpowered,
                Owner,
                (CardModel) null);
    }
}