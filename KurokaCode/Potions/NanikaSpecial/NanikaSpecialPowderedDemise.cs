using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialPowderedDemise : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyEnemy;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DynamicVar("Demise", 6M)
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialPowderedDemise powderedDemise = this;
        PotionModel.AssertValidForTargetedPotion(target);
        DemisePower demisePower = await PowerCmd.Apply<DemisePower>(target,
            powderedDemise.DynamicVars["Demise"].BaseValue, powderedDemise.Owner.Creature, (CardModel)null);
    }
}