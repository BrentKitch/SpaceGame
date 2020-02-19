using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Star : CelestialBody
	{
		public new string Type
		{
			get; set;
		}

		public Star(string name, string description, ConsoleColor color, Coordinates coordinates)
			: base(name, description, color, coordinates)
		{
			this.Type = "star";
		}
	}
}
