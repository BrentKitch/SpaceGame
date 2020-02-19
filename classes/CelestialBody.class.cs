using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public abstract class CelestialBody
	{
		public string Type
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public string Description
		{
			get; set;
		}

		public ConsoleColor Color
		{
			get; set;
		}
		public Coordinates Coordinates
		{
			get; set;
		}

		public List<Item> Items
		{
			get; set;
		}

		public List<ItemCategory> FavoredItemCategories
		{
			get; set;
		}

		public CelestialBody(string name, string description, ConsoleColor color, Coordinates coordinates, List<ItemCategory> favoredItemCategories)
		{
			this.Type = "celestial body";
			this.Name = name;
			this.Description = description;
			this.Color = color;
			this.Coordinates = coordinates;
			this.Items = new List<Item>();
			this.FavoredItemCategories = favoredItemCategories;
		}
		
		public CelestialBody(string name, string description, ConsoleColor color, Coordinates coordinates)
		{
			this.Name = name;
			this.Description = description;
			this.Color = color;
			this.Coordinates = coordinates;
		}

		virtual public void AddItem(Item item)
		{
		}
	}
}