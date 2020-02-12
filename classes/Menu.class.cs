using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Menu
	{
		public Universe U;

		public List<MenuItem> MenuItems
		{
			get; set;
		}

		public Menu(Universe u)
		{
			this.U = u;
			this.MenuItems = new List<MenuItem>();
		}

		public Menu(Universe u, List<MenuItem> menuItems)
		{
			this.U = u;
			this.MenuItems = menuItems;
		}

		public void Add(MenuItem item)
		{
			this.MenuItems.Add(item);
		}

		public void StartMovement()
		{
			this.Add(new MenuItem
				(
				"Move Up",
				new Action("MoveUp", this.U)
				)
			);
			this.Add(new MenuItem
				(
				"Move Right",
				new Action("MoveRight", this.U)
				)
			);
			this.Add(new MenuItem
				(
				"Move Left",
				new Action("MoveLeft", this.U)
				)
			);
			this.Add(new MenuItem
				(
				"Move Down",
				new Action("MoveDown", this.U)
				)
			);
		}

	}
}
