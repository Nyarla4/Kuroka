using MegaCrit.Sts2.Core.Entities.Powers;

namespace Kuroka.KurokaCode.Powers;

public class NanikaFormPower : KurokaPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
}