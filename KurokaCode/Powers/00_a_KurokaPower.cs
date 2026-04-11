using BaseLib.Abstracts;
using BaseLib.Extensions;
using Kuroka.KurokaCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Logging;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace Kuroka.KurokaCode.Powers;

public abstract class KurokaPower : CustomPowerModel
{
    protected Logger logger = new Logger("KurokaPower", LogType.Actions);
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