using BaseLib.Extensions;
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

public class NanikaSpecialGigantificationPotion : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<GigantificationPower>(1M)
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialGigantificationPotion gigantificationPotion = this;
        PotionModel.AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color(Colors.Red));
        GigantificationPower gigantificationPower = await PowerCmd.Apply<GigantificationPower>(target, gigantificationPotion.DynamicVars.Power<GigantificationPower>().BaseValue, gigantificationPotion.Owner.Creature, (CardModel) null);
    }
}