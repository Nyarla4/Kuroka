using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialFruitJuice : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.AnyTime;

    public override TargetType TargetType => TargetType.AnyPlayer;

    public override bool CanBeGeneratedInCombat => false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new MaxHpVar(1M)];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialFruitJuice fruitJuice = this;
        PotionModel.AssertValidForTargetedPotion(target);
        await CreatureCmd.GainMaxHp(target, fruitJuice.DynamicVars.MaxHp.BaseValue);
    }
}