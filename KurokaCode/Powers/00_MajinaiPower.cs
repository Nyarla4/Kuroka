using BaseLib.Extensions;
using Godot;
using Kuroka.KurokaCode.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
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
        
        IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(
            choiceContext,
            Owner,
            Amount,
            ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move,
            Owner
        );

        Creature? player = Owner.CombatState.GetCreaturesOnSide(CombatSide.Player).FirstOrDefault();
        
        if (this.Owner.IsDead && overKill > 0)
        {
            //오버킬된 경우
            if (player != null)
            {
                Player p = player.Player;
                MagicStickHammerRelic? playerRelic = player.Player.GetRelic<MagicStickHammerRelic>();
                if (playerRelic != null)
                {
                    var rng = p.RunState.Rng.Niche;

                    var alive = this.CombatState.CreaturesOnCurrentSide.ToList();
                    alive.RemoveAll(c => c.IsDead);
                    
                    Creature target = alive[rng.NextInt(0, alive.Count)];
                    await PowerCmd.Apply<MajinaiPower>(target, overKill, this.Owner, null);
                }
            }
            //_logger.Info($"overkilled {this.Owner.Name}, overed: {overKill}, actual overed: {this.Owner.CurrentHp}");
        }
        
        if (this.Owner.IsAlive && overKill < 0) // 주문 피해를 받고 살아있는 경우
        {
            if (player != null)
            {
                SpicyNakjiKimchiJookPower? playerBuff = player.GetPower<SpicyNakjiKimchiJookPower>();

                if (playerBuff != null)
                {
                    await PowerCmd.ModifyAmount(playerBuff, -1, null,null);
                }
                else
                {
                    await PowerCmd.Remove<MajinaiPower>(this.Owner);
                    
                    MajinaiStrengthPower? strengthPower = player.GetPower<MajinaiStrengthPower>();
                    if (strengthPower != null)
                        await strengthPower.RecalculateFromExternal(choiceContext);
                }
            }
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

        Creature? player = CombatState
            .GetCreaturesOnSide(CombatSide.Player)
            .FirstOrDefault();
        if (player == null) return;

        int count = GetMajinaiedCreatures(
            CombatState.GetCreaturesOnSide(CombatSide.Enemy)).Count;

        // 플레이어에게 MajinaiStrengthPower가 없으면 최초 부여
        // 있으면 MajinaiStrengthPower.AfterPowerAmountChanged가 알아서 재계산
        if (player.GetPower<MajinaiStrengthPower>() == null && count > 0)
        {
            await PowerCmd.Apply<MajinaiStrengthPower>(player, count, null, null);
        }
    }
}