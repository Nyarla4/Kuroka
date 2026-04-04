using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Commands;

public class CustomCreatureCmd
{
    public static async Task<Decimal> GainBlockWithCustomVfx(
        Creature creature,
        Decimal amount,
        ValueProp props,
        CardPlay? cardPlay,
        string customSfxPath, // 커스텀 소리 경로
        string customVfxPath, // 커스텀 이펙트 경로
        bool fast = false)
    {
        if (CombatManager.Instance.IsOverOrEnding)
            return 0M;
            
        CombatState combatState = creature.CombatState;
        await Hook.BeforeBlockGained(combatState, creature, amount, props, cardPlay?.Card);
        
        Decimal modifiedAmount = amount;
        IEnumerable<AbstractModel> modifiers;
        modifiedAmount = Hook.ModifyBlock(combatState, creature, modifiedAmount, props, cardPlay?.Card, cardPlay, out modifiers);
        modifiedAmount = Math.Max(modifiedAmount, 0M);
        
        await Hook.AfterModifyingBlockAmount(combatState, modifiedAmount, cardPlay?.Card, cardPlay, modifiers);
        
        if (modifiedAmount > 0M)
        {
            if (!string.IsNullOrEmpty(customSfxPath))
            {
                // [구조] 우리가 정의한 경로 규칙에 따라 .tscn 파일을 가져옵니다.
                var fullPath = SceneHelper.GetScenePath(customSfxPath);
                var scene = PreloadManager.Cache.GetScene(fullPath);

                if (scene != null)
                {
                    // [흐름] Node2D가 아닌 일반 Node로 인스턴스화 (AudioStreamPlayer 수용 가능)
                    Node soundNode = scene.Instantiate<Node>();
        
                    // 컨테이너에 안전하게 추가
                    NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(soundNode);

                    // [실행] AudioStreamPlayer 타입일 경우 재생 및 자동 삭제 예약
                    if (soundNode is AudioStreamPlayer audio)
                    {
                        audio.Play();
                        audio.Finished += () => audio.QueueFree(); // 재생 끝나면 메모리 해제
                    }
                }
                else
                {
                    GD.PrintErr($"[CustomSfx] 경로를 찾을 수 없습니다: {fullPath}");
                }
            }
                
            if (!string.IsNullOrEmpty(customVfxPath))
                VfxCmd.PlayOnCreatureCenter(creature, customVfxPath);

            creature.GainBlockInternal(modifiedAmount);
            CombatManager.Instance.History.BlockGained(combatState, creature, (int)modifiedAmount, props, cardPlay);
            
            if (fast)
                await Cmd.CustomScaledWait(0.0f, 0.03f);
            else
                await Cmd.CustomScaledWait(0.1f, 0.25f);
        }
        
        await Hook.AfterBlockGained(combatState, creature, modifiedAmount, props, cardPlay?.Card);
        
        return modifiedAmount;
    }
}