using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpaceGame
{
	public class Universe
	{
		public Game Game
		{
			get; set;
		}

		public List<Objective> Objectives
		{
			get; set;
		}

		public Character Character
		{
			get; set;
		}

		public List<CelestialBody> CelestialBodies
		{
			get; set;
		}

		public Universe()
		{
			this.CelestialBodies = new List<CelestialBody>();
			this.Objectives = new List<Objective>();
		}

		public void Add(Character character)
		{
			this.Character = character;
		}

		public void Add(Objective objective)
		{
			this.Objectives.Add(objective);
		}

		public void Add(CelestialBody celestialBody)
		{
			this.CelestialBodies.Add(celestialBody);
		}
	}
}
