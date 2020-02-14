using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Menu
	{
		private Universe U;

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

		public Menu Add(MenuItem item)
		{
			this.MenuItems.Add(item);
			return this;
		}

		public Menu StartMovement()
		{
			this.Add(new MenuItem
				(
				"Move Up",
				new Action(this.U, "MoveUp"),
				ConsoleKey.UpArrow
				)
			);
			this.Add(new MenuItem
				(
				"Move Right",
				new Action(this.U, "MoveRight"),
				ConsoleKey.RightArrow
				)
			);
			this.Add(new MenuItem
				(
				"Move Left",
				new Action(this.U, "MoveLeft"),
				ConsoleKey.LeftArrow
				)
			);
			this.Add(new MenuItem
				(
				"Move Down",
				new Action(this.U, "MoveDown"),
				ConsoleKey.DownArrow
				)
			);

			return this;
		}

		public Menu ShowShopBuyMenu(CelestialBody celestialBody)
		{
			int i = 1;

			foreach (Item item in celestialBody.Items)
			{
				this.Add(new MenuItem
					(
					item.Name,
					new Action(
						this.U,
						"Buy"
						),
					(ConsoleKey)(i + 48)
					)
				);
			}

			return this;
		}
		public Menu ShowShopSellMenu(CelestialBody celestialBody)
		{
			int i = 1;

			foreach (Item item in celestialBody.Items)
			{
				this.Add(new MenuItem
					(
					item.Name,
					new Action(
						this.U,
						"Sell"
						),
					(ConsoleKey)(i + 48)
					)
				);
			}

			return this;
		}

	}
}
