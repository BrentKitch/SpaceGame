using Newtonsoft.Json;
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
			int i = 0;

			this.Add(new MenuItem
				(
				"Go Back",
				new Action(
					this.U,
					"ResetMenu"
					),
				(ConsoleKey)(ConsoleKey.D0)
				)
			);

			foreach (Item item in celestialBody.Items)
			{
				int cost = item.BaseCost;

				// Let's check if the celestial body favors this item.
				foreach (ItemCategory favoredItemCategory in celestialBody.FavoredItemCategories)
				{
					// If this item has a category that matches this celestial body's favored item categories,
					// then we can buy and sell this item for a higher cost.
					if (item.ItemCategories.Contains(favoredItemCategory))
					{
						cost = (int)Math.Ceiling((double)cost * 2);
					}
				}

				this.Add(new MenuItem
					(
					$"{item.Name} (COST: {cost})",
					new Action(
						this.U,
						"Buy",
						item,
						cost
						),
					(ConsoleKey)(i + 48)
					)
				);
			}

			return this;
		}

		public Menu ShowShopSellMenu(CelestialBody celestialBody)
		{
			int i = 0;

			this.Add(new MenuItem
				(
				"Go Back",
				new Action(
					this.U,
					"ResetMenu"
					),
				(ConsoleKey)(ConsoleKey.D0)
				)
			);

			foreach (Item item in this.U.Character.Inventory)
			{
				int cost = (int)(item.BaseCost * .9);

				// Let's check if the celestial body favors this item.
				foreach (ItemCategory favoredItemCategory in celestialBody.FavoredItemCategories)
				{
					// If this item has a category that matches this celestial body's favored item categories,
					// then we can buy and sell this item for a higher cost.
					if (item.ItemCategories.Contains(favoredItemCategory))
					{
						cost = (int)Math.Ceiling((double)cost * 2);
					}
				}

				this.Add(new MenuItem
					(
					$"{item.Name} (VALUE: {cost})",
					new Action(
						this.U,
						"Sell",
						item,
						cost
						),
					(ConsoleKey)(i + 48)
					)
				);
			}

			return this;
		}

	}
}
