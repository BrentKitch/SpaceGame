using System;

namespace SpaceGame
{
	public abstract class CelestialBody
	{
		public string Name
		{
			get; set;
		}

		public string Description
		{
			get; set;
		}

		public ConsoleColor Color
		{
			get; set;
		}

		public CelestialBody(string name, string description, ConsoleColor color)
		{
			this.Name = name;
			this.Description = description;
			this.Color = color;
		}
	}
}