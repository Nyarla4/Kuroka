using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Kuroka.KurokaCode.Patches;

[HarmonyPatch(typeof(NCreature), "_Ready")]
public class RorokaPositionPatch
{
    [HarmonyPostfix]
    public static void Postfix(NCreature __instance)
    {
        if (__instance.Visuals != null && __instance.Visuals.Name.ToString().Contains("Roroka"))
        {
            __instance.Position = new Vector2(-250, 200);
        }
    }
}