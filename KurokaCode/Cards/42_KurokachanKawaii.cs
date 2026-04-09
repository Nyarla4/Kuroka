using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Kuroka.KurokaCode.Cards;

public class KurokachanKawaii() : WitchCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust]; 

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DelusionFactorPower>(3M),
        new CardsVar(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if(!IsWitch) {
            await PowerCmd.Apply<DelusionFactorPower>(
                this.Owner.Creature, 
                DynamicVars.Power<DelusionFactorPower>().BaseValue,
                this.Owner.Creature, 
                this
            );
        }
        if (IsWitch)
        {
            foreach (CardModel card in PileType.Hand.GetPile(Owner).Cards.Where<CardModel>((Func<CardModel, bool>) (c => c.IsUpgradable)))
                CardCmd.Upgrade(card);
        }
        else
        {
            CardModel card = await CardSelectCmd.FromHandForUpgrade(choiceContext, Owner, (AbstractModel) this);
            if (card == null)
                return;
            CardCmd.Upgrade(card);
        }
        if(IsWitch && IsUpgraded) {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, this.Owner);
        }
    }

    protected override void OnUpgrade() {
        this.RemoveKeyword(CardKeyword.Exhaust);
    }
}