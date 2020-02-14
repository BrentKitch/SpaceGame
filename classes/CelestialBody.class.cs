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
		public Coordinates Coordinates
		{
			get; set;
		}
		public CelestialBody(string name, string description, ConsoleColor color, Coordinates coordinates)
		{
			this.Name = name;
			this.Description = description;
			this.Color = color;
			this.Coordinates = coordinates;
		}
	}
}