using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Kuroka.KurokaCode.Character;
using Kuroka.KurokaCode.Extensions;
using Kuroka.KurokaCode.Pets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Logging;

namespace Kuroka.KurokaCode.Cards;

[Pool(typeof(KurokaCardPool))]
public abstract class KurokaCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    CustomCardModel(cost, type, rarity, target)
{
    protected Logger logger = new Logger("KurokaCard", LogType.Actions);
    
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();

    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    
    protected Creature? GetRoroka()
    {
        if (this.CombatState == null)
        {
            return null;
        }
        
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