using BaseLib.Abstracts;
using Kuroka.KurokaCode.Extensions;
using Godot;
using Kuroka.KurokaCode.Cards;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace Kuroka.KurokaCode.Character;

public class Kuroka : PlaceholderCharacterModel
{
    public const string CharacterId = "Kuroka";

    public static readonly Color Color = new("7e7291");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeKuroka>(),
        ModelDb.Card<StrikeKuroka>(),
        ModelDb.Card<StrikeKuroka>(),
        ModelDb.Card<StrikeKuroka>(),
        ModelDb.Card<RoroHi>(),
        ModelDb.Card<DefendKuroka>(),
        ModelDb.Card<DefendKuroka>(),
        ModelDb.Card<DefendKuroka>(),
        ModelDb.Card<DefendKuroka>(),
        ModelDb.Card<TwoFairy>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<BurningBlood>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<KurokaCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<KurokaRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<KurokaPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectBg => SceneHelper.GetScenePath("background/background_kuroka");
}