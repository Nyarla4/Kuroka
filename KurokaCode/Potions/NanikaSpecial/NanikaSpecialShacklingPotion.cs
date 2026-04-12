using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialShacklingPotion : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AllEnemies;

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>()
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<StrengthPower>(5M)
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialShacklingPotion shacklingPotion = this;
        Creature creature = shacklingPotion.Owner.Creature;
        foreach (Creature hittableEnemy in (IEnumerable<Creature>) creature.CombatState.HittableEnemies)
            NCombatRoom.Instance?.PlaySplashVfx(hittableEnemy, new Color("91a19f"));
        IReadOnlyList<ShacklingPotionPower> shacklingPotionPowerList = await PowerCmd.Apply<ShacklingPotionPower>((IEnumerable<Creature>) creature.CombatState.HittableEnemies, (Decimal) shacklingPotion.DynamicVars.Strength.IntValue, shacklingPotion.Owner.Creature, (CardModel) null);
    }
}