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
			string moveUpCost = (this.U.Character.Direction != Direction.Up) ? $" (-{this.U.Character.Spaceship.FuelLossRate} FUEL)" : null;
			string moveRightCost = (this.U.Character.Direction != Direction.Right) ? $" (-{this.U.Character.Spaceship.FuelLossRate} FUEL)" : null;
			string moveLeftCost = (this.U.Character.Direction != Direction.Left) ? $" (-{this.U.Character.Spaceship.FuelLossRate} FUEL)" : null;
			string moveDownCost = (this.U.Character.Direction != Direction.Down) ? $" (-{this.U.Character.Spaceship.FuelLossRate} FUEL)" : null;

			if (this.U.Character.Spaceship.Fuel >= this.U.Character.Spaceship.FuelLossRate
				|| this.U.Character.Direction == Direction.Up)
			{
				this.Add(new MenuItem
					(
					$"Move Up {moveUpCost}",
					new Action(this.U, "MoveUp"),
					ConsoleKey.UpArrow
					)
				);
			}

			if (this.U.Character.Spaceship.Fuel >= this.U.Character.Spaceship.FuelLossRate
				|| this.U.Character.Direction == Direction.Right)
			{
				this.Add(new MenuItem
					(
					$"Move Right {moveRightCost}",
					new Action(this.U, "MoveRight"),
					ConsoleKey.RightArrow
					)
				);
			}

			if (this.U.Character.Spaceship.Fuel >= this.U.Character.Spaceship.FuelLossRate
				|| this.U.Character.Direction == Direction.Left)
			{
				this.Add(new MenuItem
					(
					$"Move Left {moveLeftCost}",
					new Action(this.U, "MoveLeft"),
					ConsoleKey.LeftArrow
					)
				);
			}

			if (this.U.Character.Spaceship.Fuel >= this.U.Character.Spaceship.FuelLossRate
				|| this.U.Character.Direction == Direction.Down)
			{
				this.Add(new MenuItem
					(
					$"Move Down {moveDownCost}",
					new Action(this.U, "MoveDown"),
					ConsoleKey.DownArrow
					)
				);
			}

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
				i++;
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
				i++;
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

		public Menu ShowPaintSpaceshipMenu()
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

			var colors = new List<(string color, ConsoleColor cColor)>();
			colors.Add(("Red", ConsoleColor.Red));
			colors.Add(("Green", ConsoleColor.Green));
			colors.Add(("Blue", ConsoleColor.Cyan));
			colors.Add(("Yellow", ConsoleColor.DarkYellow));
			colors.Add(("Pink", ConsoleColor.Magenta));
			colors.Add(("White", ConsoleColor.White));
			colors.Add(("Black", ConsoleColor.Black));

			foreach ((string color, ConsoleColor cColor) color in colors)
			{
				i++;
				int cost = (int)Math.Ceiling((double)this.U.Character.Starbucks * .2);

				this.Add(new MenuItem
					(
					$"{color.color} (COST: {cost})",
					new Action(
						this.U,
						"PaintSpaceship",
						color.cColor,
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
