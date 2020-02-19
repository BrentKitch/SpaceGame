using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
	public class Spaceship
	{
		public ConsoleColor Color
		{
			get; set;
		}

		public int Fuel
		{
			get; set;
		}

		public int MaxWeight
		{
			get; set;
		}

		public int FuelCapacity
		{
			get; set;
		}

		public int FuelLossRate
		{
			get; set;
		}

		public int Speed
		{
			get; set;
		}

		public Spaceship()
		{
			this.Color = ConsoleColor.Green;
			this.MaxWeight = 100;
			this.FuelCapacity = 100;
			this.Fuel = 100;
			this.FuelLossRate = 5;
			this.Speed = 3;
		}
	}
}
