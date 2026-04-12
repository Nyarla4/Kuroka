using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialExplosiveAmpoule : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AllEnemies;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8M, ValueProp.Unpowered)];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialExplosiveAmpoule explosiveAmpoule = this;
        Creature player = explosiveAmpoule.Owner.Creature;
        DamageVar damage = explosiveAmpoule.DynamicVars.Damage;
        IReadOnlyList<Creature> targets = player.CombatState.HittableEnemies;
        foreach (Creature target1 in (IEnumerable<Creature>) targets)
        {
            NCombatRoom instance = NCombatRoom.Instance;
            if (instance != null)
                instance.CombatVfxContainer.AddChildSafely((Godot.Node) NFireSmokePuffVfx.Create(target1));
        }
        await Cmd.CustomScaledWait(0.2f, 0.3f);
        IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext, (IEnumerable<Creature>) targets, damage.BaseValue, damage.Props, player, (CardModel) null);
        player = (Creature) null;
        damage = (DamageVar) null;
        targets = (IReadOnlyList<Creature>) null;
    }
}