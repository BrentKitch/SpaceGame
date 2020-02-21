using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Planet : CelestialBody
	{
		override public string Type
		{
			get; set;
		}

		public Planet(string name, string description, ConsoleColor color, Coordinates coordinates, List<ItemCategory> favoredItemCategories, char uniqueIdentifier)
			: base(name, description, color, coordinates, favoredItemCategories, uniqueIdentifier)
		{
			this.Type = "planet";
		}

		override public void AddItem(Item item)
		{
			this.Items.Add(item);
		}
	}
}
