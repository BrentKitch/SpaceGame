using System;
using Newtonsoft.Json;

namespace SpaceGame
{
	class App
	{
		public void Run()
		{
			// Create the universe.
			Universe u = new Universe();

			// Create the game.
			Game g = new Game(u);
			u.Game = g;

			// Start the game.
			g.Initialize();

			// Display the menu.
			g.BuildMenu();
			foreach (MenuItem item in g.Menu.MenuItems)
			{
				Console.WriteLine(item.Label);
			}
			Console.WriteLine();

			// Do the first thing on the menu.
			g.Menu.MenuItems[0].Execute();

			// Display the menu.
			foreach (MenuItem item in g.Menu.MenuItems)
			{
				Console.WriteLine(item.Label);
			}
			Console.WriteLine();

			// Go Back.
			g.Menu.MenuItems[0].Execute();

			// Display the menu.
			foreach (MenuItem item in g.Menu.MenuItems)
			{
				Console.WriteLine(item.Label);
			}
			Console.WriteLine();
		}
	}
}