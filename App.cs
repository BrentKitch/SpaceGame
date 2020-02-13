using System;

namespace SpaceGame
{
	class App
	{
		public void Run()
		{
			UserInterface ui = new UserInterface();
			// Create the universe.
			Universe u = new Universe();

			// Add a character to the universe.
			Character character = new Character("Johnnyboi", Gender.Male);
			u.Add(character);

			// Add a planet to the universe.
			CelestialBody mars = new Planet("Mars", "The big red boi.", ConsoleColor.Red, new Coordinates(10,30));
			CelestialBody earth = new Planet("Earth", "The big watery boi w/ life.", ConsoleColor.Blue, new Coordinates(14, 59));
			u.Add(earth);
			u.Add(mars);

			// Create a menu.
			Menu menu = new Menu(u);
			ui.RenderGame(u, menu);
			//// Add movement options.
			//menu.StartMovement();

			//// Display the menu.
			//foreach (MenuItem item in menu.MenuItems)
			//{
			//	Console.WriteLine(item.Label);
			//}
			//Console.WriteLine();
			
			//// Execute the 4th item on the list.
			//Console.WriteLine($"{u.Character.Coordinates.X}, {u.Character.Coordinates.Y}");
			//menu.MenuItems[3].Execute();
			//Console.WriteLine($"{u.Character.Coordinates.X}, {u.Character.Coordinates.Y}");

		}
	}
}
