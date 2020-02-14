using System;

namespace SpaceGame
{
	public class Planet : CelestialBody
	{
		// TODO: Item array.
		// TODO: Item multiplier(s).
		// TODO: ItemCategory multiplier(s).

		public Planet(string name, string description, ConsoleColor color, Coordinates coordinates)
			: base(name, description, color, coordinates)
		{
		}
	}
}
