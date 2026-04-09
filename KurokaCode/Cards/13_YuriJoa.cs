using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Cards;

public class YuriJoa() : WitchCard(1, CardType.Skill,
    CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DelusionFactorPower>(1M),
        new CardsVar("CommonDraw", 2),
        new CardsVar("WitchDraw", 4)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        PowerModel? pow = Owner.Creature.GetPower<DelusionFactorPower>();
        bool isWitch = false;
        if (pow is { Amount: >= 10 })
        {
            isWitch = true;
        }

        int drawAmount = (int)(isWitch?DynamicVars["CommonDraw"].BaseValue:DynamicVars["WitchDraw"].BaseValue);

        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, (Decimal) drawAmount, this.Owner);

        if(!isWitch) {
            await PowerCmd.Apply<DelusionFactorPower>(
                this.Owner.Creature, 
                DynamicVars.Power<DelusionFactorPower>().BaseValue,
                this.Owner.Creature, 
                this
            );
        }
    }

    protected override void OnUpgrade() {
        DynamicVars["CommonDraw"].UpgradeValueBy(1M);
        DynamicVars["WitchDraw"].UpgradeValueBy(1M);
    }
}