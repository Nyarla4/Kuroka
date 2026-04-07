using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Logging;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace Kuroka.KurokaCode.Powers;

public class SecomMasadaPower : KurokaPower
{
    
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    private Logger _logger = new Logger("SecomMasadaPower", LogType.Actions);
    
}