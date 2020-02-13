﻿using System;

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

		public Color Color
		{
			get; set;
		}
		public Coordinates Coordinates
		{
			get; set;
		}

		public CelestialBody(string name, string description, Color color, Coordinates Coordinates)
		{
			this.Name = name;
			this.Description = description;
			this.Color = color;
			this.Coordinates = Coordinates;
		}
	}
}