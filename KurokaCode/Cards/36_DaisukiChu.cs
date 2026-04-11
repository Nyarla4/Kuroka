using Kuroka.KurokaCode.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Kuroka.KurokaCode.Cards;

public class DaisukiChu() : KurokaCard(3, CardType.Skill, CardRarity.Rare, TargetType.None)
{
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //CustomCreatureCmd.PlayAudio("audio/daisukichu");

        await PowerCmd.Apply<NoDrawPower>(
            this.Owner.Creature,
            1M,
            this.Owner.Creature,
            this
        );

        foreach (CardModel card in PileType.Hand.GetPile(this.Owner).Cards)
        {
            card.EnergyCost.SetThisTurn(0);
        }
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}
