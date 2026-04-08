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

public class YuriAtkDef() : WitchCard(1, CardType.Skill,
    CardRarity.Common, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DelusionFactorPower>(3M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        PowerModel ? pow = Owner.Creature.GetPower<DelusionFactorPower>();
        bool isWitch = false;
        if (pow is { Amount: >= 10 })
        {
            isWitch = true;
        }

        if(isWitch) {
            await PowerCmd.Apply<StrengthPower>(
                this.Owner.Creature, 
                1,
                this.Owner.Creature, 
                this
            );
            await PowerCmd.Apply<DexterityPower>(
                this.Owner.Creature, 
                1,
                this.Owner.Creature, 
                this
            );
//상향평준화
        }
        else {
//힘<->민첩
            await PowerCmd.Apply<DelusionFactorPower>(
                this.Owner.Creature, 
                DynamicVars.Power<DelusionFactorPower>().BaseValue,
                this.Owner.Creature, 
                this
            );
        }
    }

    protected override void OnUpgrade() {
        this.EnergyCost.UpgradeBy(-1);
    }
}