using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class MenuItem
	{
		public string Label
		{
			get; set;
		}

		public Action Action
		{
			get; set;
		}

		public MenuItem(string label, Action action)
		{
			this.Label = label;
			this.Action = action;
		}

		public void Execute()
		{
			this.Action.Execute();
		}
	}
}