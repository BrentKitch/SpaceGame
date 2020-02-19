using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Planet : CelestialBody
	{
		public new string Type
		{
			get; set;
		}

		public Planet(string name, string description, ConsoleColor color, Coordinates coordinates, List<ItemCategory> favoredItemCategories)
			: base(name, description, color, coordinates, favoredItemCategories)
		{
			this.Type = "planet";
		}

		override public void AddItem(Item item)
		{
			this.Items.Add(item);
		}
	}
}
