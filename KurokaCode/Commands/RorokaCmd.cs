using Kuroka.KurokaCode.Pets;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Commands;

public static class RorokaCmd
{
  public static async Task SummonOrHeal(CombatState combatState, uint? rorokaId, Decimal amount)
  {
    Creature? roroka = null;
    
    if (rorokaId.HasValue)
    {
      roroka = combatState.GetCreature(rorokaId.Value);
    }

    if (roroka == null || roroka.IsDead)
    {
      var roroModel = ModelDb.Get<Roroka>();
      
      var clonedRoroka = (Roroka)roroModel.MutableClone();
      
      roroka = await CreatureCmd.Add(clonedRoroka, combatState, CombatSide.Player);
      
      await PowerCmd.Apply<RorokaPower>(roroka, 1M, null, null);
    }

    await CreatureCmd.Heal(roroka, amount);
  }
}