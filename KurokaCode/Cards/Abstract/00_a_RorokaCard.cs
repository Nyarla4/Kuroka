using Kuroka.KurokaCode.Pets;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Logging;

namespace Kuroka.KurokaCode.Cards;

public abstract class RorokaCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    KurokaCard(cost, type, rarity, target)
{
    protected new Logger logger = new Logger("RorokaCard", LogType.Actions);
    
    protected override HashSet<CardTag> CanonicalTags
    {
        get => new HashSet<CardTag>() { Roroka.RorokaTag };
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.FromPower<RorokaPower>()
    ];
}