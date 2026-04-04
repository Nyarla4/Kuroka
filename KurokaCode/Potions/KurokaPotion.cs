using BaseLib.Abstracts;
using BaseLib.Utils;
using Kuroka.KurokaCode.Character;

namespace Kuroka.KurokaCode.Potions;

[Pool(typeof(KurokaPotionPool))]
public abstract class KurokaPotion : CustomPotionModel;