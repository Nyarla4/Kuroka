using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace Kuroka.KurokaCode.Cards;

  
public class RoroCute() : RorokaGrowthCard(1, CardRarity.Uncommon)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("RorokaGrowth", 5M), new PowerVar<RoroCutePower>(1M)];
    private Logger _logger = new Logger("RoroCute", LogType.Actions);
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await base.OnPlay(choiceContext, play);
        
        Creature roroka = GetRoroka();
        
        await PowerCmd.Apply<RoroCutePower>(
            roroka, 
            DynamicVars.Power<RoroCutePower>().BaseValue,
            this.Owner.Creature, 
            this
        );

    }

    protected override void OnUpgrade()
    { 
        DynamicVars["RorokaGrowth"].UpgradeValueBy(6M);
    }
}