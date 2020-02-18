using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
	public class Spaceship
	{
		public ConsoleColor color
		{
			get; set;
		}

		public int Fuel
		{
			get; set;
		}

		public int FuelCapacity
		{
			get; set;
		}


		public Spaceship()
		{
			this.color = ConsoleColor.White;
			this.FuelCapacity = 100;
			this.Fuel = 100;
		}
	}
}
