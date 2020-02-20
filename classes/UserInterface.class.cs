using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Linq;

namespace SpaceGame
{
	public class UserInterface
	{

		private Universe U
		{
			get; set;
		}
		private Menu Menu
		{
			get; set;
		}
		public UserInterface(Universe u, Menu menu)
		{
			this.U = u;
			this.Menu = menu;
		}
		List<(int, int)> xyPair = new List<(int, int)> { };
		public void RenderGame(Universe u, Menu menu)
		{
			this.U = u;
			this.Menu = menu;
			foreach (CelestialBody cb in this.U.CelestialBodies)
			{
				xyPair.Add((cb.Coordinates.X, cb.Coordinates.Y));
			}
			Console.Clear();

			RenderMap();
			RenderMenu();
		}
		void RenderMap()
		{
			int starCounter = 0; // counts through the following for loop to give a star distribution
			for (int y = 0; y < 30; y++)  //writes height(y)
			{

				for (int x = 0; x < 120; x++, starCounter++) // writes width(x)
				{
					if (U.Character.Coordinates.X == x && U.Character.Coordinates.Y == y)
					{
						Console.ForegroundColor = this.U.Character.Spaceship.Color;
						if (xyPair.Contains((x, y)))
						{
							Console.BackgroundColor = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Color;

							switch (this.U.Character.Direction)
							{
								case Direction.Up:
									Console.Write("▲");
									break;
								case Direction.Down:
									Console.Write("▼");
									break;
								case Direction.Left:
									Console.Write("◄");
									break;
								case Direction.Right:
									Console.Write("►");
									break;
							}
							Console.Write(" ");
							x++;
							Console.ResetColor();
						}
						else
						{
							switch (this.U.Character.Direction)
							{
								case Direction.Up:
									Console.Write("▲");
									break;
								case Direction.Down:
									Console.Write("▼");
									break;
								case Direction.Left:
									Console.Write("◄");
									break;
								case Direction.Right:
									Console.Write("►");
									break;
							}
							Console.ResetColor();
						}
					}
					else if (xyPair.Contains((x, y)))
					{
						Console.BackgroundColor = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Color;
						Console.Write(" ");
						if (this.U.Character.Coordinates.X == x + 1 && this.U.Character.Coordinates.Y == y)
						{
							Console.ForegroundColor = this.U.Character.Spaceship.Color;
							switch (this.U.Character.Direction)
							{
								case Direction.Up:
									Console.Write("▲");
									break;
								case Direction.Down:
									Console.Write("▼");
									break;
								case Direction.Left:
									Console.Write("◄");
									break;
								case Direction.Right:
									Console.Write("►");
									break;
							}
						}
						else
						{
							Console.Write(" ");
						}
						x++;
						Console.ResetColor();
					}
					else if ((y != 0 || y != 29) && (x == 0 || x == 119))        // makes box for map
					{
						Console.BackgroundColor = ConsoleColor.DarkGray;
						Console.Write(" ");
						Console.ResetColor();
					}
					else if (y == 0 || y == 29)
					{
						Console.BackgroundColor = ConsoleColor.DarkGray;
						Console.Write(" ");
					}
					else
					{
						if (starCounter % 14 == 0 && starCounter % 5 == 0 || starCounter % 100 == 0) //distribution of stars 
						{
							if (starCounter % 14 == 0)                   //changes color of stars on based on mod properties to get a "random" look to the star display
							{
								Console.ForegroundColor = ConsoleColor.DarkGray;
								Console.Write(".");
							}
							else if (starCounter % 200 == 0)
							{
								Console.ForegroundColor = ConsoleColor.DarkYellow;
								Console.Write(".");
							}
							else if (starCounter % 5 == 0)
							{
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.Write(".");
							}
							else
							{
								Console.ResetColor();
								Console.Write(".");
							}

						}
						else if (starCounter % 76 == 0)  // writes an '*' instead of a '.' as a star
						{
							Console.Write("*");
						}
						else
						{
							Console.BackgroundColor = ConsoleColor.Black; //default space color
							Console.Write(" ");  //space
						}
					}

					if (x == 119)   //returns to the next line when it reaches the set map width. 
					{
						Console.Write("\n");
					}

				}
			}
			Console.ResetColor();
		}
		void RenderMenu()
		{

			for (int i = 0; i < 17; i++)
			{
				for (int a = 0; a < 120; a++)
				{
					if (i == 4)
					{
						Console.BackgroundColor = ConsoleColor.DarkRed;
						Console.Write(" ");

					}
					else if ((i != 0 || i != 19) && (a == 0 || a == 119))  // makes box for menu
					{
						Console.BackgroundColor = ConsoleColor.DarkRed;
						Console.Write(" ");
					}
					else if (i == 0 || i == 16)
					{
						Console.BackgroundColor = ConsoleColor.DarkRed;
						Console.Write(" ");
					}
					else
					{
						Console.ResetColor();
						Console.Write(" ");
					}

					if (a == 119)   //returns to the next line when it reaches the set menu width. 
					{
						Console.Write("\n");
					}

				}
			}
			Console.ResetColor();
			DisplayInformation();

		}

		private void DisplayInformation()
		{

			Console.SetCursorPosition(2, 32);
			Console.Write(U.Character.Name);
			switch (U.Character.Gender.ToString())
			{
				case "Male":
					Console.ForegroundColor = ConsoleColor.DarkCyan;
					Console.Write($" ♂");
					Console.ResetColor();
					break;
				case "Female":
					Console.ForegroundColor = ConsoleColor.DarkMagenta;
					Console.Write($" ♀");
					Console.ResetColor();
					break;
				case "Alien":
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write($" §");
					Console.ResetColor();
					break;
				default:
					break;
			}

			//displays HUD
			Console.SetCursorPosition(25, 32);
			Console.Write($"Age: {this.U.Character.Age / 12}");
			Console.SetCursorPosition(50, 32);
			Console.Write($"¤{U.Character.Starbucks} Starbucks");
			Console.SetCursorPosition(80, 32);
			Console.Write($"Health {this.U.Character.Health}/100");
			Console.SetCursorPosition(105, 32);
			Console.Write($"Fuel {this.U.Character.Spaceship.Fuel}/{this.U.Character.Spaceship.FuelCapacity}");

			(int, int) menuPosition = (85, 35);
			foreach (MenuItem menuItem in Menu.MenuItems)
			{
				Console.SetCursorPosition(menuPosition.Item1, menuPosition.Item2);
				Console.Write($"{menuItem.Key}: {menuItem.Label}");
				menuPosition.Item2++;
			}

		}

		public void ShowStory()
		{

			if (U.Character.Age == 780) // 65 years old.
			{
				this.RenderStory($"" +
					$"You turn 65.\n\n" +
					$"Your body fails you.\n\n" +
					$"Your vitals give way.\n\n" +
					$"You die...\n\n\n" +
					$"...single.");
			}
			else if (U.Character.Age == 768) // 64 years old.
			{
				this.RenderStory($"" +
					$"You receive another letter in your SpaceMail inbox.\n" +
					$"It's from the doctor.!\n\n" +
					$"'You've got exactly one year left to live,' it says.\n\n" +
					$"'Well,' you think to yourself. 'It's been a good run.'");
			}
			else if (U.Character.Age == 756) // 63 years old.
			{
				this.RenderStory($"" +
					$"You receive another letter in your SpaceMail inbox.\n" +
					$"It's from the... doctor?!\n\n" +
					$"'You've got exactly two years left to live,' it says.\n\n" +
					$"Somehow, this doesn't put you in a good mood.");
			}
			else if (U.Character.Age == 720) // 60 years old.
			{
				this.RenderStory($"" +
					$"You just turned 60.\n\n" +
					$"Needless to say, your hair is greying a little bit.");
			}
			else if (U.Character.Age == 660) // 55 years old.
			{
				if ((Universe.StarbucksToSavePrincess - U.Character.Starbucks) > 0)
				{
					this.RenderStory($"" +
						$"The princess, Kanna Endrick, is probably really lonely.\n\n" +
						$"On the plus side, you have {U.Character.Starbucks} Starbucks.\n\n" +
						$"You only need {Universe.StarbucksToSavePrincess - U.Character.Starbucks} more." +
						$"You must hurry.");
				}
				else
				{
					this.RenderStory($"" +
						$"The princess is probably really lonely.\n\n" +
						$"You have enough money to save her.\n\n" +
						$"What are you waiting for?");
				}
			}
			else if (U.Character.Age == 588) // 49 years old.
			{
				this.RenderStory($"" +
					$"You receive another letter in your SpaceMail inbox.\n" +
					$"It's from the princess!\n\n" +
					$"'Pretty sure I just went through menopause,' it says.\n\n" +
					$"You didn't really want kids anyway.");
			}
			else if (U.Character.Age == 540) // 45 years old.
			{
				this.RenderStory($"" +
					$"You receive a letter your SpaceMail inbox.\n" +
					$"It's from the princess!\n\n" +
					$"'Please hurry,' it says.\n\n\n" +
					$"No shit.");
			}
			else if (U.Character.Age == 480) // 40 years old.
			{
				this.RenderStory($"" +
					$"You just turned 40 years old.\n\n" +
					$"...and you're still single.");
			}
			else if (U.Character.Age == 420) // 35 years old.
			{
				this.RenderStory($"" +
					$"Some quick math confirms you are 35 years old.\n\n" +
					$"'Man, I'm getting old,' you chuckle to yourself.\n\n" +
					$"Somehow, this fills you with determination.");
			}
			else if (U.Character.Age == 372) // 31 years old.
			{
				this.RenderStory($"" +
				$"  It has been a decade since your journey began.\n\n" +
				$"  You only have ¤{U.Character.Starbucks} Starbucks.\n\n\n\n" +
				$"  You still need another ¤{Universe.StarbucksToSavePrincess - U.Character.Starbucks} Starbucks to buy her freedom.\n\n\n\n" +
				$"  Get it together. The princess, Kanna Endrick, needs your help.");
			}
			else if (U.Character.Age == 252) // 21 years old.
			{
				this.RenderStory($"" +
					$"  In the distance, you think you hear the princess cry for your name.\n\n" +
					$"  'Help me, {U.Character.Name}!'\n\n" +
					$"  But sound doesn't travel in space.");
			}
		}

		public void GameOver(string message)
		{
			Console.Clear();

            string deathScreen = @"			  .AMMMMMMMMMMA.          
      			.AV. :::.:.:.::MA.        
      	  	      A' :..        : .:`A       
   		      A'..              . `A.                   YY   YY      A
   		     A' :.    :::::::::  : :`A                   YY YY      AAA
   		     M  .    :::.:.:.:::  . .M                    YYY      A   A
  		     M  :   ::.:.....::.:   .M                     Y      AAAAAAA
  		     V : :.::.:........:.:  :V                     Y      A     A
 		    A  A:    ..:...:...:.   A A                    Y      A     A
 		   .V  MA:.....:M.::.::. .:AM.M                    Y      A     A
		  A'  .VMMMMMMMMM:.:AMMMMMMMV: A  
		 :M .  .`VMMMMMMV.:A `VMMMMV .:M: 
		  V.:.  ..`VMMMV.:AM..`VMV' .: V  
		   V.  .:. .....:AMMA. . .:. .V            DDDDDDD    EEEEEEE      A      DDDDDDD
 		    VMM...: ...:.MMMM.: .: MMV             D     DD   E           AAA     D     DD
		       `VM: . ..M.:M..:::M'                D      D   E          A   A    D      D
   		    	 `M::. .:.... .::M                 D      D   EEEEEEE   AAAAAAA   D      D
		          M:.  :. .... ..M                 D     DD   E         A     A   D      D
		          V:  M:. M. :M .V                 D    DD    E         A     A   D     DD
		          `V.:M.. M. :M.V'                 DDDDDD     EEEEEEE   A     A   DDDDDDD
                                                                   
                                                                   ";

			Console.WriteLine(deathScreen);
			Console.WriteLine("\n\n");
			Console.WriteLine($"{message}");


			// The screen should be displayed for a minimum of 3 seconds.
			System.Threading.Thread.Sleep(3000);

			// Take input and hide the screen.
			Console.WriteLine("\n\n\n\n");
			while (Console.KeyAvailable)
			{
				Console.ReadKey(false);
			}

			Console.Write("  Press any key to exit...  ");
			Console.ReadKey(false);
			Console.Clear();
			Environment.Exit(0);
		}

		public void RenderStory(string message, bool randomEvent = false)
		{
			// Clear the input buffer.
			while (Console.KeyAvailable)
			{
				Console.ReadKey(false);
			}

			// Clear the screen.
			Console.Clear();

			if (randomEvent)
			{
				// Render the random event message.
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine("\n\n\n");
				Console.WriteLine("  +-------------------------------------------------------------+");
				Console.WriteLine("  |                                                             |");
				Console.WriteLine("  |                             ALERT!                          |");
				Console.WriteLine("  |                                                             |");
				Console.WriteLine("  |=============================================================|");
				Console.WriteLine("  |                                                             |");
				Console.WriteLine("  |                     SOMETHING HAS HAPPENED!                 |");
				Console.WriteLine("  |                                                             |");
				Console.WriteLine("  +-------------------------------------------------------------+");
				Console.WriteLine("\n\n\n");
				Console.WriteLine($"{message}");
				Console.WriteLine("\n\n\n\n\n\n");
				Console.ResetColor();
			}
			else
			{
				// Render the story message.
				Console.WriteLine("\n\n\n");
				Console.WriteLine("  +-------------------------------------------------------------+");
				Console.WriteLine("  |                                                             |");
				Console.WriteLine("  |                     THE STORY CONTINUES...                  |");
				Console.WriteLine("  |                                                             |");
				Console.WriteLine("  +-------------------------------------------------------------+");
				Console.WriteLine("\n\n\n");
				Console.WriteLine($"{message}");
				Console.WriteLine("\n\n\n\n\n\n");
			}

			// The screen should be displayed for a minimum of 3 seconds.
			System.Threading.Thread.Sleep(3000);

			// Take input and hide the screen.
			while (Console.KeyAvailable)
			{
				Console.ReadKey(false);
			}

			Console.Write("  Press any key to continue...  ");
			Console.ReadKey(false);
			Console.Clear();
		}
	}
}
