using BaseLib.Abstracts;

namespace Kuroka.KurokaCode.Pets;

public class Roroka : CustomPetModel
{
    public Roroka() : base(true)
    {
    }

    public override int MinInitialHp => 24;
    public override int MaxInitialHp => 24;
    public override string? CustomVisualPath => "res://scenes/creature_visuals/kuroka-roroka.tscn";
}