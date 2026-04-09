using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Cards;

public class DiaTomo() : WitchCard(2, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DexterityPower>(2M)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        PowerModel pow = Owner.Creature.GetPower<DelusionFactorPower>();
        if(IsWitch) {
            DynamicVars.Power<DexterityPower>().BaseValue = pow.Amount / 2;
        }
        await PowerCmd.Apply<DexterityPower>(
                this.Owner.Creature, 
                DynamicVars.Power<DexterityPower>().BaseValue,
                this.Owner.Creature, 
                this
        );
        if(pow != null && pow.Amount >= 5) {
            await PowerCmd.Apply<BufferPower>(
                this.Owner.Creature, 
                1,
                this.Owner.Creature, 
                this
            );
        }
    }

    protected override void OnUpgrade() {
        this.EnergyCost.UpgradeBy(-1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        .. base.ExtraHoverTips,
        HoverTipFactory.FromPower<DexterityPower>(),
        HoverTipFactory.FromPower<BufferPower>()
    ];
}