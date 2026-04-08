using BaseLib.Abstracts;
using BaseLib.Extensions;
using Kuroka.KurokaCode.Extensions;
using Godot;

namespace Kuroka.KurokaCode.Powers;

public abstract class KurokaPower : CustomPowerModel
{
    //Loads from Kuroka/images/powers/your_power.png
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }
}