//Authors: Justin Davis, Brent Kitchen, Nathanael Shermett

using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
	public class Character
	{
		public string Name
		{
			get; set;
		}

		// Age, in months.
		public int Age
		{
			get; set;
		}

		public int AgeMultiplier
		{
			get; set;
		}

		public int Health
		{
			get; set;
		}
		
		public int Starbucks
		{
			get; set;
		}
		
		public Coordinates Coordinates
		{
			get; set;
		}
		
		public Gender Gender
		{
			get; set;
		}

		public Direction Direction
		{
			get; set;
		}

		public List<Item> Inventory
		{
			get; set;
		}

		public Spaceship SpaceShip
		{
			get;set;
		}

		public Character(string name, Gender gender, Coordinates coordinates)
		{
			this.Name = name;
			this.Gender = gender;
			this.Coordinates = coordinates;
			this.Age = 18 * 12; // Age, but in months.
			this.AgeMultiplier = 3; // How quickly our character ages.
			this.Starbucks = 100;
			this.Health = 100;
			this.Direction = Direction.Up;
			this.Inventory = new List<Item>();
			this.SpaceShip = new Spaceship();
		}

		public bool InCollision(Coordinates coordinates)
		{
			if ((this.Coordinates.X == coordinates.X || this.Coordinates.X == coordinates.X + 1)
				&& this.Coordinates.Y == coordinates.Y)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}