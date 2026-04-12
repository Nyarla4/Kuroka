using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public class NanikaSpecialSneckoOil : NanikaSpecialPotion
{
    public int _testEnergyCostOverride = -1;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(5)];

    public int TestEnergyCostOverride
    {
        get => this._testEnergyCostOverride;
        set
        {
            TestMode.AssertOn();
            this.AssertMutable();
            this._testEnergyCostOverride = value;
        }
    }

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NanikaSpecialSneckoOil sneckoOil = this;
        PotionModel.AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color("6ec46f"));
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, sneckoOil.DynamicVars.Cards.BaseValue, target.Player);
        foreach (CardModel card in PileType.Hand.GetPile(target.Player).Cards.Where<CardModel>((Func<CardModel, bool>) (c => !c.EnergyCost.CostsX)))
        {
            if (card.EnergyCost.GetWithModifiers(CostModifiers.None) >= 0)
            {
                card.EnergyCost.SetThisTurnOrUntilPlayed(sneckoOil.NextEnergyCost());
                NCard.FindOnTable(card)?.PlayRandomizeCostAnim();
            }
        }
    }

    public int NextEnergyCost()
    {
        return this.TestEnergyCostOverride >= 0 ? this.TestEnergyCostOverride : this.Owner.RunState.Rng.CombatEnergyCosts.NextInt(4);
    }
}