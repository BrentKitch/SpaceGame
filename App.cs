using System;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
namespace SpaceGame
{
	class App
	{
		[DllImport("kernel32.dll", ExactSpelling = true)]

		private static extern IntPtr GetConsoleWindow();
		private static IntPtr ThisConsole = GetConsoleWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		private const int HIDE = 0;
		private const int MAXIMIZE = 3;
		private const int MINIMIZE = 6;
		private const int RESTORE = 9;

		public void Run()
		{
			ShowWindow(ThisConsole, MAXIMIZE);
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			Universe u = new Universe();  // Create the universe.

			// Create the game.
			Game g = new Game(u);
			u.Game = g;

			// Start the game.			
			g.Initialize();
			g.UI.ShowStory();

			g.BuildMenu();


			g.Step();
			//// Display the menu.
			
			//foreach (MenuItem item in g.Menu.MenuItems)
			//{
			//	Console.WriteLine(item.Label);
			//}
			//Console.WriteLine();

			////// Do the first thing on the menu.
			////g.Menu.MenuItems[0].Execute();

			//// Display the menu.
			//foreach (MenuItem item in g.Menu.MenuItems)
			//{
			//	Console.WriteLine(item.Label);
			//}
			//Console.WriteLine();

			//// Go Back.
			//g.Menu.MenuItems[0].Execute();

			//// Display the menu.
			//foreach (MenuItem item in g.Menu.MenuItems)
			//{
			//	Console.WriteLine(item.Label);
			//}
			//Console.WriteLine();
		}
	}
}