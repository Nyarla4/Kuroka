using BaseLib.Extensions;
using Godot;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Logging;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace Kuroka.KurokaCode.Cards;

public class SpicyNakjiKimchiJook() : KurokaCard(1, CardType.Power,
    CardRarity.Rare, TargetType.Self)
{
    private Logger _logger = new Logger("SpicyNakjiKimchiJook", LogType.Actions);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);

        //VfxCmd.PlayOnCreatureCenter(play.Target, "vfx/custom_poison_impact");

        await PowerCmd.Apply<SpicyNakjiKimchiJookPower>(
            this.Owner.Creature,
            1,
            this.Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade()
    {
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<SpicyNakjiKimchiJookPower>()
    ];
}