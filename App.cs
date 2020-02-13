using System;

namespace SpaceGame
{
	class App
	{
		public void Run()
		{
			// Create the universe.
			Universe u = new Universe();

			// Add a character to the universe.
			Character character = new Character("Johnnyboi", Gender.Male);
			u.Add(character);

			// Add a planet to the universe.
			CelestialBody mars = new Planet("Mars", "The big red boi.", Color.Red);
			u.Add(mars);

			// Create a menu.
			Menu menu = new Menu(u);

			// Add movement options.
			menu.StartMovement();

			// Display the menu.
			foreach (MenuItem item in menu.MenuItems)
			{
				Console.WriteLine(item.Label);
			}
			Console.WriteLine();
			
			// Execute the 4th item on the list.
			menu.MenuItems[3].Execute();
		}
	}
}
