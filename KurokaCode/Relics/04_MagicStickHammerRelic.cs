// 04_MagicStickHammerRelic.cs
// Java: 툴팁만 존재, 실제 효과는 MajinaiPower 오버킬 전파에서 처리
// CS: MajinaiPower.BeforeTurnEnd의 오버킬 전파 로직에서 이 유물 보유 여부 확인 필요
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using Kuroka.KurokaCode.Powers;

namespace Kuroka.KurokaCode.Relics;

public class MagicStickHammerRelic : KurokaRelic
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<MajinaiPower>()];
}