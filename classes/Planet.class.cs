using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Planet : CelestialBody
	{
		public Planet(string name, string description, ConsoleColor color, Coordinates coordinates, List<ItemCategory> favoredItemCategories)
			: base(name, description, color, coordinates, favoredItemCategories)
		{
		}

		override public void AddItem(Item item)
		{
			this.Items.Add(item);
		}
	}
}
