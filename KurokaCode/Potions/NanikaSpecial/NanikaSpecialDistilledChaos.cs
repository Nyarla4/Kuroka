using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialDistilledChaos : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new RepeatVar(2)
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialDistilledChaos distilledChaos = this;
        NCombatRoom.Instance?.PlaySplashVfx(distilledChaos.Owner.Creature, new Color("a296a3"));
        await CardPileCmd.AutoPlayFromDrawPile(choiceContext, distilledChaos.Owner, distilledChaos.DynamicVars.Repeat.IntValue, CardPilePosition.Top, false);
    }
}