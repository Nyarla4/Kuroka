using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Logging;

namespace Kuroka.KurokaCode.Powers;

public class DelusionFactorPower : KurokaPower
{   
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    private Logger _logger = new Logger("DelusionFactorPower", LogType.Actions);
    
}