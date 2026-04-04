using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Kuroka.KurokaCode.Cards;

public class SpicyNakjiKimchiJook() : KurokaCard(1, CardType.Power,
    CardRarity.Rare, TargetType.Self)
{
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (play.Target == null) return;

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
}