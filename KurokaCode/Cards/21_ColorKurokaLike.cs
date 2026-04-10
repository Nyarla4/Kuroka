using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace Kuroka.KurokaCode.Cards;

public class ColorKurokaLike() : KurokaCard(1, CardType.Skill,
    CardRarity.Common, TargetType.Self)
{
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        List<CardModel> list = new()
        {
            Owner.Creature.CombatState.CreateCard(new ColorBlack(), Owner),
            Owner.Creature.CombatState.CreateCard(new ColorRed(), Owner),
        };
        
        if (IsUpgraded)
            CardCmd.Upgrade((IEnumerable<CardModel>) list, CardPreviewStyle.HorizontalLayout);
        if (!IsUpgraded)
        {
            CardModel card = await CardSelectCmd.FromChooseACardScreen(choiceContext, (IReadOnlyList<CardModel>) list, Owner, true);
            if (card == null)
                return;
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);    
        }
        else
        {
            foreach (CardModel card in list)
            {
                await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);   
            }
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        ..HoverTipFactory.FromCardWithCardHoverTips<ColorBlack>(IsUpgraded),
        ..HoverTipFactory.FromCardWithCardHoverTips<ColorRed>(IsUpgraded)
    ];
}