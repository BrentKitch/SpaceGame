using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class Action
	{
		public string Name
		{
			get; set;
		}

		private Universe Universe;

		public Action(string name, Universe universe)
		{
			this.Name = name;
			this.Universe = universe;
		}

		public void Execute()
		{
		}


	}
}