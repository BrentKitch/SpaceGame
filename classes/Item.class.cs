using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
	public class Item
	{
		public string Name
		{
			get; set;
		}

		public string Description
		{
			get; set;
		}

		public List<ItemCategory> ItemCategories
		{
			get; set;
		}

		public int BaseCost
		{
			get; set;
		}

		public int Weight
		{
			get; set;
		}


		public Item(string name, string description, int baseCost, int weight, List<ItemCategory> itemCategories)
		{
			this.Name = name;
			this.Description = description;
			this.BaseCost = baseCost;
			this.Weight = weight;
			this.ItemCategories = itemCategories;
		}
	}
}
