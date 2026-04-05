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
    /// <summary>
    /// 커스텀 오디오 재생
    /// </summary>
    /// <param name="customSfxPath"></param>
    public static void PlayAudio(string customSfxPath)
    {
        if (string.IsNullOrEmpty(customSfxPath)) return;

        var fullPath = SceneHelper.GetScenePath(customSfxPath);
        var scene = PreloadManager.Cache?.GetScene(fullPath);

        if (scene != null)
        {
            Node soundNode = scene.Instantiate<Node>();
            NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(soundNode);

            if (soundNode is AudioStreamPlayer audio)
            {
                audio.Play();
                // 재생 완료 후 제거
                audio.Finished += () => audio.QueueFree();
            }
        }
        else
        {
            GD.PrintErr($"[CustomAudio] 사운드 경로를 찾을 수 없습니다: {fullPath}");
        }
    }

    /// <summary>
    /// 방어도 획득
    /// </summary>
    /// <param name="creature">방어도 획득 대상</param>
    /// <param name="amount">획득할 방어도 량</param>
    /// <param name="props"></param>
    /// <param name="cardPlay"></param>
    /// <param name="customVfxPath">이펙트 경로 (선택적(Optional)으로 처리)</param>
    /// <param name="fast"></param>
    /// <returns></returns>
    public static async Task<Decimal> Defend(
        Creature creature,
        Decimal amount,
        ValueProp props,
        CardPlay? cardPlay,
        string customVfxPath = "",
        bool fast = false)
    {
        if (CombatManager.Instance.IsOverOrEnding || creature.CombatState == null)
        {
            return 0M;
        }

        CombatState combatState = creature.CombatState;
        await Hook.BeforeBlockGained(combatState, creature, amount, props, cardPlay?.Card);

        Decimal modifiedAmount = amount;
        IEnumerable<AbstractModel> modifiers;
        modifiedAmount = Hook.ModifyBlock(combatState, creature, modifiedAmount, props, cardPlay?.Card, cardPlay,
            out modifiers);
        modifiedAmount = Math.Max(modifiedAmount, 0M);

        await Hook.AfterModifyingBlockAmount(combatState, modifiedAmount, cardPlay?.Card, cardPlay, modifiers);

        if (modifiedAmount > 0M)
        {
            // 방어도 획득 시각 효과(VFX)만 남음
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