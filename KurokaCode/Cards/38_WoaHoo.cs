using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards;

public class WoaHoo() : KurokaCard(1, CardType.Power,
    CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WoaHooPower>(1M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);

        await PowerCmd.Apply<WoaHooPower>(
            this.Owner.Creature, 
            DynamicVars.Power<WoaHooPower>().BaseValue,
            this.Owner.Creature, 
            this
        );
    }

    protected override void OnUpgrade() { 
        this.EnergyCost.UpgradeBy(-1);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<WoaHooPower>()
    ];
}