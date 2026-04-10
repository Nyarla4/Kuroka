using BaseLib.Abstracts;
using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace Kuroka.KurokaCode.Pets;

public class Roroka : CustomPetModel
{
	[CustomEnum] public static CardTag RorokaTag;
	public Roroka() : base(true)
	{
	}

	public override int MinInitialHp => 24;
	public override int MaxInitialHp => 24;
	public override string? CustomVisualPath => "res://scenes/creature_visuals/kuroka-roroka.tscn";
}
