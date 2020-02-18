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

		public string Name
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

		public void Execute()
		{
			switch (this.Name)
			{
				case "Buy":
					this.Buy();
					break;
				case "ChangeMenu":
					this.ChangeMenu(this.NewMenu);
					break;
				case "ResetMenu":
					this.ResetMenu();
					break;
				case "MoveUp":
					this.MoveUp();
					break;
				case "MoveRight":
					this.MoveRight();
					break;
				case "MoveLeft":
					this.MoveLeft();
					break;
				case "MoveDown":
					this.MoveDown();
					break;
			}
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

		private void ChangeMenu(Menu newMenu)
		{
			this.U.Game.Menu = newMenu;
		}

		private void ResetMenu()
		{
			this.U.Game.BuildMenu();
		}

		private void MoveUp()
		{
			this.U.Character.Age += this.U.Character.AgeMultiplier;
			this.U.Character.Coordinates.Y -= 1;
			this.U.Character.Direction = Direction.Up;
		}

		private void MoveRight()
		{
			this.U.Character.Age += this.U.Character.AgeMultiplier;
			this.U.Character.Coordinates.X += 1;
			this.U.Character.Direction = Direction.Right;
		}

		private void MoveLeft()
		{
			this.U.Character.Age += this.U.Character.AgeMultiplier;
			this.U.Character.Coordinates.X -= 1;
			this.U.Character.Direction = Direction.Left;
		}

		private void MoveDown()
		{
			this.U.Character.Age += this.U.Character.AgeMultiplier;
			this.U.Character.Coordinates.Y += 1;
			this.U.Character.Direction = Direction.Down;
		}
	}
}