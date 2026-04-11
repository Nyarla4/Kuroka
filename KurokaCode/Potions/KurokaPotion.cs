using BaseLib.Abstracts;
using BaseLib.Utils;
using Kuroka.KurokaCode.Character;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Potions;

[Pool(typeof(KurokaPotionPool))]
public abstract class KurokaPotion : CustomPotionModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    ];
    
    public override IEnumerable<IHoverTip> ExtraHoverTips => [
    ];
}