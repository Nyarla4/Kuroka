using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialShipInABottle : NanikaSpecialPotion
{
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(7M, ValueProp.Unpowered)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialShipInABottle shipInAbottle = this;
        PotionModel.AssertValidForTargetedPotion(target);
        Decimal num = await CreatureCmd.GainBlock(target, shipInAbottle.DynamicVars.Block, (CardPlay) null);
        BlockNextTurnPower blockNextTurnPower = await PowerCmd.Apply<BlockNextTurnPower>(target, (Decimal) shipInAbottle.DynamicVars.Block.IntValue, shipInAbottle.Owner.Creature, (CardModel) null);
    }
}