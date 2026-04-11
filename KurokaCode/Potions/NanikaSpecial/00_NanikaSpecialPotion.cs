
using MegaCrit.Sts2.Core.Helpers;

namespace Kuroka.KurokaCode.Potions.NanikaSpecial;

public abstract class NanikaSpecialPotion : KurokaPotion
{
    // KUROKA-NANIKA_SPECIAL_ 접두사 제거 후 소문자로 경로 생성
    public override string CustomPackedImagePath =>
        ImageHelper.GetImagePath(
            $"atlases/potion_atlas.sprites/{Id.Entry.Replace("KUROKA-NANIKA_SPECIAL_", "").ToLowerInvariant()}.tres");

    public override string CustomPackedOutlinePath =>
        ImageHelper.GetImagePath(
            $"atlases/potion_outline_atlas.sprites/{Id.Entry.Replace("KUROKA-NANIKA_SPECIAL_", "").ToLowerInvariant()}.tres");
}