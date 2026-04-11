using HarmonyLib;
using Kuroka.KurokaCode.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace Kuroka.KurokaCode.Patches;

[HarmonyPatch(typeof(TouchOfOrobas), "RefinementUpgrades", MethodType.Getter)]
public static class TouchOfOrobasPatch
{
    [HarmonyPostfix]
    private static void AddKurokaRefinement(ref Dictionary<ModelId, RelicModel> __result)
    {
        __result[ModelDb.Relic<KurokaStartRelic>().Id] = ModelDb.Relic<KurokaAncientRelic>();
    }
}