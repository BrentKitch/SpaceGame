using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Planet : CelestialBody
	{
		// TODO: Item multiplier(s).
		// TODO: ItemCategory multiplier(s).


		public Planet(string name, string description, ConsoleColor color, Coordinates coordinates)
			: base(name, description, color, coordinates)
		{
		}

		override public void AddItem(Item item)
		{
			this.Items.Add(item);
		}
	}
}
