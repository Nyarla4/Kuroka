using Kuroka.KurokaCode.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace Kuroka.KurokaCode.Powers;

public class MajinaiPower : KurokaPower
{
    
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    private Logger _logger = new Logger("MajinaiPower", LogType.Actions);
    
    public static IReadOnlyList<Creature> GetMajinaiedCreatures(IReadOnlyList<Creature> creatures)
    {
        return creatures.Where(c => c.GetPower<MajinaiPower>() != null).ToList();
    }
    
    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (CombatManager.Instance.IsOverOrEnding ||
            Owner.CombatState == null ||
            Owner.IsDead || 
            side != Owner.Side)
        {
            return;
        }
        
        IReadOnlyList<Creature> customedCreatures = GetMajinaiedCreatures(Owner.CombatState.GetCreaturesOnSide(side));
        
        if (customedCreatures.FirstOrDefault() != Owner)
            return;

        int overKill = Amount - Owner.CurrentHp;
        
        var combatState = Owner.CombatState;
        var players = combatState.GetCreaturesOnSide(CombatSide.Player).ToList();

        IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(
            choiceContext, Owner, Amount,
            ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, Owner);

        if (this.Owner.IsDead && overKill > 0)
        {
            // MagicStickHammer는 유물이므로 소유자 플레이어 탐색
            foreach (Creature player in players)
            {
                if(!player.IsPlayer)
                    continue;
                MagicStickHammerRelic? playerRelic = player.Player.GetRelic<MagicStickHammerRelic>();
                if (playerRelic == null) continue;

                var rng = player.Player.RunState.Rng.Niche;
                var alive = combatState
                    .GetCreaturesOnSide(CombatSide.Enemy)
                    .Where(c => c.IsAlive && c != this.Owner)
                    .ToList();

                if (alive.Count == 0) break;

                Creature target = alive[rng.NextInt(0, alive.Count)];
                await PowerCmd.Apply<MajinaiPower>(target, overKill, this.Owner, null);
                break; // 오버킬 전파는 1회만
            }
        }

        if (this.Owner.IsAlive && overKill < 0)
        {
            foreach (Creature player in players)
            {
                SpicyNakjiKimchiJookPower? playerBuff = player.GetPower<SpicyNakjiKimchiJookPower>();
                if (playerBuff != null)
                {
                    await PowerCmd.ModifyAmount(playerBuff, -1, null, null);
                    return; // SpicyNakjiKimchiJook가 있으면 제거 스킵
                }
            }
            // 어느 플레이어도 SpicyNakjiKimchiJookPower 없으면 제거
            await PowerCmd.Remove<MajinaiPower>(this.Owner);
        }

        // ⬇ 모든 플레이어의 MajinaiStrengthPower 재계산
        foreach (Creature player in players)
        {
            if(!player.IsPlayer)
                continue;
            MajinaiStrengthPower? strengthPower = player.GetPower<MajinaiStrengthPower>();
            if (strengthPower != null)
                await strengthPower.RecalculateFromExternal(choiceContext);
        }
    }
    
    public override async Task AfterPowerAmountChanged(
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        if (power != this) return;
        if (this.Owner.Side != CombatSide.Enemy) return;

        int count = GetMajinaiedCreatures(
            CombatState.GetCreaturesOnSide(CombatSide.Enemy)).Count;

        // ⬇ FirstOrDefault → 모든 플레이어 순회
        foreach (Creature player in CombatState.GetCreaturesOnSide(CombatSide.Player))
        {
            if(!player.IsPlayer)
                continue;
            if (player.GetPower<MajinaiStrengthPower>() == null && count > 0)
                await PowerCmd.Apply<MajinaiStrengthPower>(player, count, null, null);
        }
    }
}