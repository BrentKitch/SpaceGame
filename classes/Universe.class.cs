using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpaceGame
{
	public class Universe
	{
		public const int StarbucksToSavePrincess = 10002;

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

		public List<string> Messages
		{
			get; set;
		}

		public Universe()
		{
			this.CelestialBodies = new List<CelestialBody>();
			this.Objectives = new List<Objective>();
			this.Messages = new List<string>();
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

		public void ClearMessages()
		{
			this.Messages = new List<string>();
		}
	}
}
