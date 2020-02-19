using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Action
	{
		private Universe U;

		// Parameters.

		private Item Item
		{
			get; set;
		}

		private int ItemCost
		{
			get; set;
		}

		private List<Item> Items
		{
			get; set;
		}

		private Menu NewMenu
		{
			get; set;
		}

		private CelestialBody CelestialBody
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public ConsoleColor PaintColor
		{
			get; set;
		}

		public Action(Universe u, string name)
		{
			this.U = u;
			this.Name = name;
		}

		public Action(Universe u, string name, Item item, int itemCost)
		{
			this.U = u;
			this.Name = name;
			this.Item = item;
			this.ItemCost = itemCost;
		}
		public Action(Universe u, string name, ConsoleColor paintColor, int itemCost)
		{
			this.U = u;
			this.Name = name;
			this.PaintColor = paintColor;
			this.ItemCost = itemCost;
		}

		public Action(Universe u, string name, List<Item> items)
		{
			this.U = u;
			this.Name = name;
			this.Items = items;
		}

		public Action(Universe u, string name, Menu newMenu)
		{
			this.U = u;
			this.Name = name;
			this.NewMenu = newMenu;
		}
		public Action(Universe u, string name, CelestialBody celestialBody)
		{
			this.U = u;
			this.Name = name;
			this.CelestialBody = celestialBody;
		}

		public void Execute()
		{
			switch (this.Name)
			{
				case "LeaveCelestialBody":
					this.LeaveCelestialBody();
					this.U.Game.BuildMenu();
					break;
				case "Buy":
					this.Buy();
					this.U.Game.BuildMenu();
					this.U.Game.Menu.MenuItems[1].Execute(); // Select Buy again.
					break;
				case "Sell":
					this.Sell();
					this.U.Game.BuildMenu();
					this.U.Game.Menu.MenuItems[2].Execute(); // Select Sell again.
					break;
				case "PaintSpaceship":
					this.PaintSpaceship();
					this.U.Game.BuildMenu();
					break;
				case "Refuel":
					this.Refuel();
					this.U.Game.BuildMenu();
					break;
				case "Hospital":
					this.Hospital();
					this.U.Game.BuildMenu();
					break;
				case "ChangeMenu":
					this.ChangeMenu(this.NewMenu);
					break;
				case "ResetMenu":
					this.ResetMenu();
					this.U.Game.BuildMenu();
					break;
				case "MoveUp":
					this.MoveUp();
					this.U.Game.UI.ShowStory();
					this.U.Game.BuildMenu();
					break;
				case "MoveRight":
					this.MoveRight();
					this.U.Game.UI.ShowStory();
					this.U.Game.BuildMenu();
					break;
				case "MoveLeft":
					this.MoveLeft();
					this.U.Game.UI.ShowStory();
					this.U.Game.BuildMenu();
					break;
				case "MoveDown":
					this.MoveDown();
					this.U.Game.UI.ShowStory();
					this.U.Game.BuildMenu();
					break;
			}
		}

		private void LeaveCelestialBody()
		{
			this.U.Character.Spaceship.Fuel -= 10;
			this.U.Character.Direction = Direction.Up;
			this.U.Character.Coordinates.X = this.CelestialBody.Coordinates.X;
			this.U.Character.Coordinates.Y = this.CelestialBody.Coordinates.Y - 1;
		}

		private void Buy()
		{
			if ((this.U.Character.Starbucks - this.ItemCost) > 0)
			{
				this.U.Character.Starbucks -= this.ItemCost;
				this.U.Character.Inventory.Add(this.Item);
			}
		}

		private void Sell()
		{
			this.U.Character.Starbucks += this.ItemCost;
			this.U.Character.Inventory.Remove(this.Item);
		}

		private void PaintSpaceship()
		{
			if ((this.U.Character.Starbucks - this.ItemCost) > 0)
			{
				this.U.Character.Starbucks -= this.ItemCost;
				this.U.Character.Spaceship.Color = this.PaintColor;
			}
		}

		private void Refuel()
		{
			if ((this.U.Character.Starbucks - 20) > 0)
			{
				this.U.Character.Starbucks -= 20;
				this.U.Character.Spaceship.Fuel = this.U.Character.Spaceship.FuelCapacity;
			}
		}

		private void Hospital()
		{
			if ((this.U.Character.Starbucks - 100) > 0)
			{
				this.U.Character.Starbucks -= 100;
				this.U.Character.Health = 100;
			}
		}

		private void ChangeMenu(Menu newMenu)
		{
			this.U.Game.Menu = newMenu;
		}

		private void ResetMenu()
		{
		}

		private void MoveUp()
		{
			if (this.U.Character.Direction != Direction.Up)
			{
				this.U.Character.Spaceship.Fuel -= this.U.Character.Spaceship.FuelLossRate;
				if (this.U.Character.Spaceship.Fuel < 0)
				{
					this.U.Character.Spaceship.Fuel = 0;
				}
			}

			this.U.Character.Age += this.U.Character.Spaceship.Speed;
			this.U.Character.Coordinates.Y -= 1;
			this.U.Character.Direction = Direction.Up;

			// If the character is inside a star, hurt them.
			foreach (CelestialBody celestialBody in this.U.CelestialBodies)
			{
				if (this.U.Character.InCollisionStar(celestialBody))
				{
					this.U.Character.Health -= 21;
				}
			}
		}

		private void MoveRight()
		{
			if (this.U.Character.Direction != Direction.Right)
			{
				this.U.Character.Spaceship.Fuel -= this.U.Character.Spaceship.FuelLossRate;
				if (this.U.Character.Spaceship.Fuel < 0)
				{
					this.U.Character.Spaceship.Fuel = 0;
				}
			}

			this.U.Character.Age += this.U.Character.Spaceship.Speed;
			this.U.Character.Coordinates.X += 1;
			this.U.Character.Direction = Direction.Right;

			// If the character is inside a star, hurt them.
			foreach (CelestialBody celestialBody in this.U.CelestialBodies)
			{
				if (this.U.Character.InCollisionStar(celestialBody))
				{
					this.U.Character.Health -= 21;
				}
			}
		}

		private void MoveLeft()
		{
			if (this.U.Character.Direction != Direction.Left)
			{
				this.U.Character.Spaceship.Fuel -= this.U.Character.Spaceship.FuelLossRate;
				if (this.U.Character.Spaceship.Fuel < 0)
				{
					this.U.Character.Spaceship.Fuel = 0;
				}
			}

			this.U.Character.Age += this.U.Character.Spaceship.Speed;
			this.U.Character.Coordinates.X -= 1;
			this.U.Character.Direction = Direction.Left;

			// If the character is inside a star, hurt them.
			foreach (CelestialBody celestialBody in this.U.CelestialBodies)
			{
				if (this.U.Character.InCollisionStar(celestialBody))
				{
					this.U.Character.Health -= 21;
				}
			}
		}

		private void MoveDown()
		{
			if (this.U.Character.Direction != Direction.Down)
			{
				this.U.Character.Spaceship.Fuel -= this.U.Character.Spaceship.FuelLossRate;
				if (this.U.Character.Spaceship.Fuel < 0)
				{
					this.U.Character.Spaceship.Fuel = 0;
				}
			}

			this.U.Character.Age += this.U.Character.Spaceship.Speed;
			this.U.Character.Coordinates.Y += 1;
			this.U.Character.Direction = Direction.Down;

			// If the character is inside a star, hurt them.
			foreach (CelestialBody celestialBody in this.U.CelestialBodies)
			{
				if (this.U.Character.InCollisionStar(celestialBody))
				{
					this.U.Character.Health -= 21;
				}
			}
		}
	}
}