using HarmonyLib;
using Kuroka.KurokaCode.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace Kuroka.KurokaCode.Patches;

[HarmonyPatch(typeof(ArchaicTooth), "TranscendenceUpgrades", MethodType.Getter)]
public static class ArchaicToothPatch
{
    [HarmonyPostfix]
    private static void AddKurokaTranscendence(ref Dictionary<ModelId, CardModel> __result)
    {
        __result[ModelDb.Card<RoroHi>().Id] = ModelDb.Card<RoroSuperHi>();
    }
}