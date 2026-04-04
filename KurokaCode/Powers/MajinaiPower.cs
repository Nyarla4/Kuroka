using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Kuroka.KurokaCode.Powers;

public class MajinaiPower : KurokaPower
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public static IReadOnlyList<Creature> GetMajinaiedCreatures(IReadOnlyList<Creature> creatures)
    {
        return creatures.Where(c => c.GetPower<MajinaiPower>() != null).ToList();
    }
    
    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (CombatManager.Instance.IsOverOrEnding ||
            this.Owner.CombatState == null ||
            this.Owner.IsDead || 
            side != this.Owner.Side)
        {
            return;
        }
        
        IReadOnlyList<Creature> customedCreatures = GetMajinaiedCreatures(this.Owner.CombatState.GetCreaturesOnSide(side));
        
        if (customedCreatures.FirstOrDefault() != this.Owner)
            return;
            
        IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(
            choiceContext,
            this.Owner,
            this.Amount,
            ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move,
            this.Owner
        );
        
        Creature? player = this.Owner.CombatState.GetCreaturesOnSide(CombatSide.Player).FirstOrDefault();
        
        if (player != null)
        {
            // 플레이어가 가진 SpecificPower(우리가 만든 구조)가 존재하는지 확인합니다.
            SpicyNakjiKimchiJookPower? playerBuff = player.GetPower<SpicyNakjiKimchiJookPower>();

            // 3. [흐름 분기]
            if (playerBuff != null)
            {
                // 분기 A: 플레이어에게 연장 버프가 있다!
                // -> 적의 디버프는 살려두고, 대신 플레이어의 연장 버프(SpecificPower) 스택을 1 깎습니다.
                await PowerCmd.ModifyAmount(playerBuff, -1, null,null);
            }
            else
            {
                // 분기 B: 플레이어에게 연장 버프가 없다!
                // -> 원래 의도대로 적의 디버프(CustomPower)를 삭제합니다.
                await PowerCmd.Remove<MajinaiPower>(this.Owner);
            }
        }
    }
}