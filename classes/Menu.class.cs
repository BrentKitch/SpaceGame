using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Menu
	{
		public List<MenuItem> MenuItems
		{
			get; set;
		}

		public Menu()
		{
			this.MenuItems = new List<MenuItem>();
		}

		public Menu(List<MenuItem> menuItems)
		{
			this.MenuItems = menuItems;
		}

		public void Add(MenuItem item)
		{
			this.MenuItems.Add(item);
		}
	}
}
