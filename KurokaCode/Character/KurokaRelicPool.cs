using BaseLib.Abstracts;
using Kuroka.KurokaCode.Extensions;
using Godot;

namespace Kuroka.KurokaCode.Character;

public class KurokaRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Kuroka.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}