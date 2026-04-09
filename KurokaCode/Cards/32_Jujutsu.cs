using BaseLib.Extensions;
using Kuroka.KurokaCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Kuroka.KurokaCode.Cards;

public class Jujutsu() : KurokaCard(0, CardType.Skill, CardRarity.Token, TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<MajinaiPower>(6M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<MajinaiPower>(
            play.Target, 
            DynamicVars.Power<MajinaiPower>().BaseValue,
            this.Owner.Creature, 
            this
        );
    }

    protected override void OnUpgrade() => this.DynamicVars.Power<MajinaiPower>().UpgradeValueBy(6M);
}