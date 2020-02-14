using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SpaceGame
{
	public class MenuItem
	{
		public Action Action
		{
			get; set;
		}

		public string Label
		{
			get; set;
		}

		public ConsoleKey Key
		{
			get; set;
		}

		public MenuItem(string label, Action action, ConsoleKey key)
		{
			this.Label = label;
			this.Action = action;
			this.Key = key;
		}

		public void Execute()
		{
			this.Action.Execute();
		}
	}
}