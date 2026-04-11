using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Powers;

public class RoroHochiPower : RorokaAdditionalPower
{
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (this.Owner.IsDead)
        {
            return;
        }
        await CreatureCmd.Damage(
            choiceContext,
            this.CombatState.HittableEnemies,
            Amount,
            ValueProp.Unpowered,
            this.Owner,
            null
        );
    }
}