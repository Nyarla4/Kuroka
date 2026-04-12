using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace Kuroka.KurokaCode.Patches;

[HarmonyPatch(typeof(NCreature), "_Ready")]
public class RorokaPositionPatch
{
    static Logger logger = new Logger("RorokaPositionPatch",LogType.Actions);
    
// 주의: 반환형을 'async void'로 변경했습니다!
    [HarmonyPostfix]
    public static async void Postfix(NCreature __instance)
    {
        if (__instance.Visuals == null) return; 

        if (__instance.Visuals.Name.ToString().Contains("Roroka"))
        {
            await __instance.ToSignal(__instance.GetTree().CreateTimer(0.1f), "timeout");

            if (!GodotObject.IsInstanceValid(__instance) || __instance.Entity == null) return;

            // 0.1초 뒤에 다시 확인!
            if (__instance.Entity.PetOwner != null)
            {
                if (NCombatRoom.Instance != null)
                {
                    NCreature? playerNCreature = NCombatRoom.Instance.GetCreatureNode(__instance.Entity.PetOwner.Creature);

                    if (playerNCreature == null)
                    {
                        logger.Info("[로그] 6 실패: playerNCreature를 찾을 수 없습니다.");
                    }
                    else
                    {
                        // 원하시는 좌표 세팅
                        __instance.Position = new Vector2(
                            playerNCreature.Position.X + 150,
                            playerNCreature.Position.Y
                        );
                    }
                }   
            }
            else
            {
                logger.Info("[로그] 0.1초를 기다렸는데도 PetOwner가 null입니다.");
            }
        }
    }
}