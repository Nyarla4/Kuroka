using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Logging;

namespace Kuroka.KurokaCode.Cards.Abstract;

/// <summary>
/// 마녀화(Witchification) 기믹이 있는 카드들의 공통 부모 클래스입니다.
/// </summary>
public abstract class WitchCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    KurokaCard(cost, type, rarity, target)
{
    protected new Logger logger = new Logger("WitchCard", LogType.Actions);

    // ==========================================================
    // 1. [불안 요소 해결] 마녀화 판정 로직 중앙화 (DRY 원칙)
    // ==========================================================
    // 개별 카드의 OnPlay에서 복붙하던 코드를 지우고 이 프로퍼티 하나만 호출합니다.
    // TODO: 추후 특정 유물 등으로 발동 조건 스택(현재 10)이 변경될 경우 여기만 수정하면 됩니다.
    public bool IsWitch => this.Owner?.Creature?.GetPower<DelusionFactorPower>()?.Amount >= 10;

    // ==========================================================
    // 2. [누락 요소 해결] 동적 타겟팅 지원 (XXMoe 등)
    // ==========================================================
    // 기본적으로는 생성자에서 받은 target을 사용하되, 하위 클래스에서 마녀화 여부에 따라 덮어쓸 수 있게 합니다.
    // 사용 예시 (XXMoe): protected override TargetType GetDynamicTarget() => IsWitch ? TargetType.AnyEnemy : TargetType.Self;
    public override TargetType TargetType => GetDynamicTarget();
    
    protected virtual TargetType GetDynamicTarget() => target;

    // ==========================================================
    // 4. [누락 요소 해결 대기] 특수 툴팁 & 미리보기 카드 (HoverTips)
    // ==========================================================
    protected override IEnumerable<IHoverTip> ExtraHoverTips => GetWitchHoverTips();

    protected virtual IEnumerable<IHoverTip> GetWitchHoverTips()
    {
        // 1. 기본적으로 망상인자 툴팁을 표시
        yield return HoverTipFactory.FromPower<DelusionFactorPower>();

        // TODO: 미리보기 카드 (Witch Preview Card) 구현 방식 엔진 검증 필요
        // StS1(Java)의 cardsToPreview 기능은 StS2에서 일반적으로 툴팁(IHoverTip)으로 구현합니다.
        // 현재 내 상태와 "반대 상태(!IsWitch)"의 카드를 미리보기로 띄워주기 위해 아래와 같은 방식의 연구가 필요합니다.
        
        /* [구현 구상도]
        if (!_isCloning) 
        {
            _isCloning = true;
            try 
            {
                var previewCard = (WitchCard)this.MutableClone();
                // 엔진 렌더링 시 IsWitch 값을 강제로 반전시킬 수 있는 플래그 주입 필요
                // previewCard.ForceWitchState = !this.IsWitch; 
                yield return HoverTipFactory.FromCard(previewCard);
            }
            finally 
            {
                _isCloning = false;
            }
        }
        */
    }
}

