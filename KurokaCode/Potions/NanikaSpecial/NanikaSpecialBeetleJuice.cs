using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialBeetleJuice :NanikaSpecialPotion
{   
    public override PotionRarity Rarity => PotionRarity.Rare;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyEnemy;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DynamicVar("DamageDecrease", 20M),
        (DynamicVar)new RepeatVar(4)
    ];
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialBeetleJuice beetleJuice = this;
        PotionModel.AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color("65cf81"));
        ShrinkPower shrinkPower = await PowerCmd.Apply<ShrinkPower>(target, beetleJuice.DynamicVars.Repeat.BaseValue, beetleJuice.Owner.Creature, (CardModel) null);
    }
}