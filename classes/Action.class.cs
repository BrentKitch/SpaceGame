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
				case "Win":
					this.Win();
					this.U.Game.BuildMenu();
					break;
				case "LeaveCelestialBody":
					this.LeaveCelestialBody();
					this.U.Game.BuildMenu();
					break;
				case "Buy":
					this.Buy();
					this.U.Game.BuildMenu();
					this.U.Game.Menu.MenuItems[1].Execute(); // Select Buy again.
					this.U.Game.Save();
					break;
				case "Sell":
					this.Sell();
					this.U.Game.BuildMenu();
					this.U.Game.Menu.MenuItems[2].Execute(); // Select Sell again.
					this.U.Game.Save();
					break;
				case "PaintSpaceship":
					this.PaintSpaceship();
					this.U.Game.BuildMenu();
					this.U.Game.Save();
					break;
				case "Refuel":
					this.Refuel();
					this.U.Game.BuildMenu();
					this.U.Game.Save();
					break;
				case "Hospital":
					this.Hospital();
					this.U.Game.BuildMenu();
					this.U.Game.Save();
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
					this.U.Game.UserInterface.ShowStory();
					this.U.Game.RandomEvent();
					this.U.Game.BuildMenu();
					break;
				case "MoveRight":
					this.MoveRight();
					this.U.Game.UserInterface.ShowStory();
					this.U.Game.RandomEvent();
					this.U.Game.BuildMenu();
					break;
				case "MoveLeft":
					this.MoveLeft();
					this.U.Game.UserInterface.ShowStory();
					this.U.Game.RandomEvent();
					this.U.Game.BuildMenu();
					break;
				case "MoveDown":
					this.MoveDown();
					this.U.Game.UserInterface.ShowStory();
					this.U.Game.RandomEvent();
					this.U.Game.BuildMenu();
					break;
			}
		}

		private void Win()
		{
			this.U.Character.Starbucks -= Universe.StarbucksToSavePrincess;

			this.U.Game.UserInterface.RenderStory($"" +
				$"  You finally managed to save up ¤{Universe.StarbucksToSavePrincess} Starbucks.\n\n" +
				$"  Full of resolve, you wire transfer that amount to HAIRY TENDERSON.\n" +
				$"  You now have ¤{this.U.Character.Starbucks} Starbucks.\n\n" +
				$"  Two months later, you receive a letter in your SpaceMail inbox.\n\n" +
				$"  It's from the princess!\n\n" +
				$"  'Thanks for the transfer. Now me and HAIRY TENDERSON can live\n" +
				$"  happily ever after." +
				$"'\n\n\n\n\n\n" +
				$"                              THE END\n\n" +
				$"");

			Environment.Exit(0);
		}

		private void LeaveCelestialBody()
		{
			this.U.Character.Spaceship.Fuel -= 15;
			this.U.Character.Direction = Direction.Up;
			this.U.Character.Coordinates.X = this.CelestialBody.Coordinates.X;
			this.U.Character.Coordinates.Y = this.CelestialBody.Coordinates.Y - 1;
			this.U.Message = $"Now leaving {this.CelestialBody}.";
		}

		private void Buy()
		{
			if ((this.U.Character.Starbucks - this.ItemCost) > 0)
			{
				this.U.Character.Starbucks -= this.ItemCost;
				this.U.Character.Inventory.Add(this.Item);
				this.U.Message = $"You bought '{this.Item.Name}'! {this.Item.Description}";
			}
			else
			{
				this.U.Message = $"You can't afford '{this.Item.Name}'.";
			}
		}

		private void Sell()
		{
			this.U.Character.Starbucks += this.ItemCost;
			this.U.Character.Inventory.Remove(this.Item);
			this.U.Message = $"You sold '{this.Item.Name}' for ¤{this.ItemCost}!";
		}

		private void PaintSpaceship()
		{
			if ((this.U.Character.Starbucks - this.ItemCost) > 0)
			{
				this.U.Character.Starbucks -= this.ItemCost;
				this.U.Character.Spaceship.Color = this.PaintColor;
				this.U.Message = $"You painted your spaceship. Lookin' good!";
			}
			else
			{
				this.U.Message = $"You can't afford this paint color. Better save up!";
			}
		}

		private void Refuel()
		{
			if ((this.U.Character.Starbucks - 100) > 0)
			{
				this.U.Character.Starbucks -= 100;
				this.U.Character.Spaceship.Fuel = this.U.Character.Spaceship.FuelCapacity;
				this.U.Message = $"Your fuel has been topped off.";
			}
			else
			{
				this.U.Message = $"You can't afford to refuel!";
			}
		}

		private void Hospital()
		{
			if ((this.U.Character.Starbucks - 100) > 0)
			{
				this.U.Character.Starbucks -= 100;
				this.U.Character.Health = 100;
				this.U.Message = $"Your wounds have been treated.";
			}
			else
			{
				this.U.Message = $"You can't afford to see a doctor!";
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

					if (celestialBody.Name == "Black Hole")
					{
						this.U.Game.UserInterface.GameOver("  You fall into a black hole.\n\n" +
							"  You are never seen again.");
					}
					else
					{
						this.U.Character.Health -= 21;
						this.U.Message = $"You're inside a star ({celestialBody.Name}) and are taking damage!!";
					}
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

					if (celestialBody.Name == "Black Hole")
					{
						this.U.Game.UserInterface.GameOver("  You fall into a black hole.\n\n" +
							"  You are never seen again.");
					}
					else
					{
						this.U.Character.Health -= 21;
						this.U.Message = $"You're inside a star ({celestialBody.Name}) and are taking damage!!";
					}
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

					if (celestialBody.Name == "Black Hole")
					{
						this.U.Game.UserInterface.GameOver("  You fall into a black hole.\n\n" +
							"  You are never seen again.");
					}
					else
					{
						this.U.Character.Health -= 21;
						this.U.Message = $"You're inside a star ({celestialBody.Name}) and are taking damage!!";
					}
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

					if (celestialBody.Name == "Black Hole")
					{
						this.U.Game.UserInterface.GameOver("  You fall into a black hole.\n\n" +
							"  You are never seen again.");
					}
					else
					{
						this.U.Character.Health -= 21;
						this.U.Message = $"You're inside a star ({celestialBody.Name}) and are taking damage!!";
					}
				}
			}
		}
	}
}