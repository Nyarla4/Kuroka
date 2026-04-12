using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialPotionOfBinding : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AllEnemies;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<VulnerablePower>(1M),
        new PowerVar<WeakPower>(1M)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<WeakPower>(),
        HoverTipFactory.FromPower<VulnerablePower>()
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialPotionOfBinding potionOfBinding = this;
        IReadOnlyList<Creature> targets = potionOfBinding.Owner.Creature.CombatState.HittableEnemies;
        foreach (Creature target1 in (IEnumerable<Creature>) targets)
        {
            NCombatRoom instance = NCombatRoom.Instance;
            if (instance != null)
                instance.CombatVfxContainer.AddChildSafely((Node) NSmokePuffVfx.Create(target1, NSmokePuffVfx.SmokePuffColor.Green));
        }
        IReadOnlyList<WeakPower> weakPowerList = await PowerCmd.Apply<WeakPower>((IEnumerable<Creature>) targets, (Decimal) potionOfBinding.DynamicVars.Power<VulnerablePower>().IntValue, potionOfBinding.Owner.Creature, (CardModel) null);
        IReadOnlyList<VulnerablePower> vulnerablePowerList = await PowerCmd.Apply<VulnerablePower>((IEnumerable<Creature>) targets, (Decimal) potionOfBinding.DynamicVars.Power<WeakPower>().IntValue, potionOfBinding.Owner.Creature, (CardModel) null);
        targets = (IReadOnlyList<Creature>) null;
    }
}