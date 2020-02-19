using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Star : CelestialBody
	{
		override public string Type
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
