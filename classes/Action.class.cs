using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Action
	{
		private Universe U;

		public string Name
		{
			get; set;
		}

		public Action(string name, Universe universe)
		{
			this.Name = name;
			this.U = universe;
		}

		public void Execute()
		{
			switch (this.Name)
			{
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