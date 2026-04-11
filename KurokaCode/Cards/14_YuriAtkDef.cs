using BaseLib.Extensions;
using Kuroka.KurokaCode.Cards.Abstract;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
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
        if(IsWitch) {
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