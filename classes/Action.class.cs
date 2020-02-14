using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Action
	{
		private Universe U;

		// Parameters.
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
				case "ChangeMenu":
					this.ChangeMenu(this.NewMenu);
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

		private void ChangeMenu(Menu newMenu)
		{
			this.U.Game.Menu = newMenu;
		}

		private void MoveUp()
		{
			this.U.Character.Coordinates.Y -= 1;
		}

		private void MoveRight()
		{
			this.U.Character.Coordinates.X += 1;
		}

		private void MoveLeft()
		{
			this.U.Character.Coordinates.X -= 1;
		}

		private void MoveDown()
		{
			this.U.Character.Coordinates.Y += 1;
		}
	}
}