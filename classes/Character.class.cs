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

		public Spaceship Spaceship
		{
			get; set;
		}

		public List<Item> Inventory
		{
			get; set;
		}

		public Character(string name, Gender gender, Coordinates coordinates)
		{
			this.Name = name;
			this.Gender = gender;
			this.Coordinates = coordinates;
			this.Age = 18 * 12; // Age, but in months.
			this.Starbucks = 74;
			this.Health = 100;
			this.Direction = Direction.Up;
			this.Spaceship = new Spaceship();
			this.Inventory = new List<Item>();
		}

		public bool InCollisionPlanet(CelestialBody celestialBody)
		{
			if (celestialBody.Type == "planet")
			{
				if ((this.Coordinates.X == celestialBody.Coordinates.X || this.Coordinates.X == celestialBody.Coordinates.X + 1)
					&& this.Coordinates.Y == celestialBody.Coordinates.Y)
				{
					return true;
				}
			}

			return false;
		}

		public bool InCollisionStar(CelestialBody celestialBody)
		{
			if (celestialBody.Type == "star")
			{
				if ((
					this.Coordinates.X == celestialBody.Coordinates.X
					|| this.Coordinates.X == celestialBody.Coordinates.X + 1
					|| this.Coordinates.X == celestialBody.Coordinates.X + 2
					|| this.Coordinates.X == celestialBody.Coordinates.X + 3
					)
					&&
					(
					this.Coordinates.Y == celestialBody.Coordinates.Y
					|| this.Coordinates.Y == celestialBody.Coordinates.Y + 1
					))
				{
					return true;
				}
			}

			return false;
		}
	}
}