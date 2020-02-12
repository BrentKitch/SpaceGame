using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
    class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        List<ItemCategory> ItemCategories { get; set; }
        public int BaseCost { get; set; }
        public int Weight { get; set; }

        public Item(string name, string description, int baseCost, int weight, List<ItemCategory> itemCategories)
        {
            this.Name = name;
            this.Description = description;
            this.BaseCost = baseCost;
            this.ItemCategories = itemCategories;
            this.Weight = weight;
        }

    }
}
