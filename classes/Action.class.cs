using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Action
	{
		public string Name
		{
			get; set;
		}

		private Universe Universe;

		public Action(string name, Universe universe)
		{
			this.Name = name;
			this.Universe = universe;
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
			this.Universe.Character.Position.Y -= 1;
		}

		private void MoveRight()
		{
			this.Universe.Character.Position.X += 1;
			Console.WriteLine("YOU MOVED RIGHT!!!!");
		}

		private void MoveLeft()
		{
			this.Universe.Character.Position.X -= 1;
		}

		private void MoveDown()
		{
			this.Universe.Character.Position.Y += 1;
			Console.WriteLine("YOU MOVED DOWN!!!!");
			Console.WriteLine($"PS. Your name is {Universe.Character.Name}");
		}
	}
}