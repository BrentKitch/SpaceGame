using System;

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

			// Add a character to the universe.
			Character character = new Character("Johnnyboi", Gender.Male);
			u.Add(character);
			u.Character.Coordinates.X = 15;
			u.Character.Coordinates.Y = 50;

			// Add a planet to the universe.
			CelestialBody mars = new Planet("Mars", "The big red boi.", ConsoleColor.Red, new Coordinates(15, 50));
			mars.AddItem(new Item("Broccoli", "It's like a tree, but gross.", 10, 1));
			mars.AddItem(new Item("Chocolate Tree", "It's like a tree, but delicious.", 15, 1));
			u.Add(mars);

			// Display the menu.
			foreach (MenuItem item in g.Menu.MenuItems)
			{
				Console.WriteLine(item.Label);
			}
			Console.WriteLine();

			// Do the first thing on the menu.
			g.Menu.MenuItems[0].Execute();

			// Display the menu.
			Console.WriteLine("AFTER CHANGE");
			foreach (MenuItem item in g.Menu.MenuItems)
			{
				Console.WriteLine(item.Label);
			}
			Console.WriteLine();
		}
	}
}
