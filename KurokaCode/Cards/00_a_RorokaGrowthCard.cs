using Kuroka.KurokaCode.Commands;
using Kuroka.KurokaCode.Pets;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Kuroka.KurokaCode.Cards;

/// <summary>
/// 로로카 최대체력 증가 카드 통합
/// protected override IEnumerable<DynamicVar> CanonicalVars => [new("RorokaGrowth", 5M)]; 최대체력 5 증가 예시
/// </summary>
public abstract class RorokaGrowthCard(int cost, CardRarity rarity) : RorokaCard(cost, CardType.Power, rarity, TargetType.Self)
{
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Creature? roroka = GetRoroka();
        uint? rorokaId = roroka?.CombatId;
        await RorokaCmd.AddMax(this.CombatState!, rorokaId, DynamicVars["RorokaGrowth"].BaseValue, this.Owner);
    }

    protected Creature? GetRoroka()
    {
        foreach (var creature in this.CombatState!.GetCreaturesOnSide(CombatSide.Player))
        {
            if (creature.Monster is Roroka && creature.PetOwner == this.Owner)
            {
                return creature;
            }
        }

        return null;
    }
}