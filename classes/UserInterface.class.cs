using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Linq;

namespace SpaceGame
{
    public class UserInterface
    {

        private Universe u
        {
            get; set;
        }
        private Menu menu
        {
            get; set;
        }       
        public UserInterface(Universe u, Menu menu)
        {
            this.u = u;
            this.menu = menu;
        }
        List<(int, int)> xyPair = new List<(int, int)> { };
        public void RenderGame(Universe u, Menu menu)
        {
            this.u = u;
            this.menu = menu;
            foreach (CelestialBody cb in this.u.CelestialBodies)
            {
                xyPair.Add((cb.Coordinates.X,cb.Coordinates.Y));
            }
            Console.Clear();
            RenderMap();
            RenderMenu();
        }
        void RenderMap()  
        {   
            int starCounter = 0; // counts through the following for loop to give a star distribution
            for(int y = 0; y < 30; y++)  //writes height(y)
            {
                
                for(int x = 0; x < 120; x++, starCounter++) // writes width(x)
                {  
                    if(u.Character.Coordinates.X == x && u.Character.Coordinates.Y == y)
                    {
                        Console.ForegroundColor = this.u.Character.Spaceship.Color;
                        if (xyPair.Contains((x, y)))
                        {
                            Console.BackgroundColor = u.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Color;
                            
                            switch (this.u.Character.Direction)
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
                            switch (this.u.Character.Direction)
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
                        Console.BackgroundColor = u.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Color;
                        Console.Write(" ");
                        if (this.u.Character.Coordinates.X == x+1 && this.u.Character.Coordinates.Y == y)
                        {
                            Console.ForegroundColor = this.u.Character.Spaceship.Color;
                            switch (this.u.Character.Direction)
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
                            } else if (starCounter % 200 == 0)
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
            
            for(int i = 0; i < 17; i++)
            {
                for(int a = 0; a < 120; a++)
                {                   
                    if(i == 4)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(" ");
                       
                    }
                    else if((i != 0 || i != 19) && (a == 0 || a == 119))  // makes box for menu
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(" ");
                    }
                    else if(i == 0 || i == 16)
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
            Console.Write(u.Character.Name);
            switch (u.Character.Gender.ToString())
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
            Console.SetCursorPosition(25, 32);
            Console.Write($"Age: {this.u.Character.Age / 12}");
            Console.SetCursorPosition(50, 32);
            Console.Write($"¤{u.Character.Starbucks} Starbucks");
            Console.SetCursorPosition(105, 32);
            Console.Write($"Fuel {this.u.Character.Spaceship.Fuel}/{this.u.Character.Spaceship.FuelCapacity}");

            (int, int) menuPosition = (85, 35);
            foreach( MenuItem menuItem in menu.MenuItems)
            {
                Console.SetCursorPosition(menuPosition.Item1, menuPosition.Item2);
                Console.Write($"{menuItem.Key}: {menuItem.Label}");
                menuPosition.Item2++;
            }

        }

		public void ShowStory()
		{

			if (u.Character.Age == 780) // 65 years old.
			{
				this.RenderStory($"" +
					$"You turn 65.\n\n" +
					$"Your body fails you.\n\n" +
					$"Your vitals give way.\n\n" +
					$"You die...\n\n\n" +
                    $"...single.");
			}
			else if (u.Character.Age == 768) // 64 years old.
			{
				this.RenderStory($"" +
					$"You receive another letter in your SpaceMail inbox.\n" +
					$"It's from the doctor.!\n\n" +
					$"'You've got exactly one year left to live,' it says.\n\n" +
					$"'Well,' you think to yourself. 'It's been a good run.'");
			}
			else if (u.Character.Age == 756) // 63 years old.
			{
				this.RenderStory($"" +
					$"You receive another letter in your SpaceMail inbox.\n" +
					$"It's from the... doctor?!\n\n" +
					$"'You've got exactly two years left to live,' it says.\n\n" +
					$"Somehow, this doesn't put you in a good mood.");
			}
			else if (u.Character.Age == 720) // 60 years old.
			{
				this.RenderStory($"" +
					$"You just turned 60.\n\n" +
					$"Needless to say, your hair is greying a little bit.");
			}
			else if (u.Character.Age == 660) // 55 years old.
			{
				if ((Universe.StarbucksToSavePrincess - u.Character.Starbucks) > 0)
				{
					this.RenderStory($"" +
						$"The princess, Kanna Endrick, is probably really lonely.\n\n" +
						$"On the plus side, you have {u.Character.Starbucks} Starbucks.\n\n" +
						$"You only need {Universe.StarbucksToSavePrincess - u.Character.Starbucks} more." +
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
			else if (u.Character.Age == 588) // 49 years old.
			{
				this.RenderStory($"" +
					$"You receive another letter in your SpaceMail inbox.\n" +
					$"It's from the princess!\n\n" +
					$"'Pretty sure I just went through menopause,' it says.\n\n" +
					$"You didn't really want kids anyway.");
			}
			else if (u.Character.Age == 540) // 45 years old.
			{
				this.RenderStory($"" +
					$"You receive a letter your SpaceMail inbox.\n" +
					$"It's from the princess!\n\n" +
					$"'Please hurry,' it says.\n\n\n" +
					$"No shit.");
			}
			else if (u.Character.Age == 480) // 40 years old.
			{
				this.RenderStory($"" +
					$"You just turned 40 years old.\n\n" +
					$"...and you're still single.");
			}
			else if (u.Character.Age == 420) // 35 years old.
			{
				this.RenderStory($"" +
					$"Some quick math confirms you are 35 years old.\n\n" +
					$"'Man, I'm getting old,' you chuckle to yourself.\n\n" +
					$"Somehow, this fills you with determination.");
			}
			else if (u.Character.Age == 372) // 31 years old.
			{
                this.RenderStory($"" +
                $"  It has been a decade since your journey began.\n\n" +
                $"  You only have ¤{u.Character.Starbucks} Starbucks.\n\n\n\n" +
                $"  You still need another ¤{Universe.StarbucksToSavePrincess - u.Character.Starbucks} Starbucks to buy her freedom.\n\n\n\n" +
                $"  Get it together. The princess, Kanna Endrick, needs your help.");
            }
			else if (u.Character.Age == 252) // 21 years old.
			{
				this.RenderStory($"" +
					$"  In the distance, you think you hear the princess cry for your name.\n\n" +
					$"  'Help me, {u.Character.Name}!'\n\n" +
					$"  But sound doesn't travel in space.");
			}
			else if (u.Character.Age == 216) // 18 years old.
			{
				this.RenderStory($"" +
                    $"  Your journey begins.\n\n" +
                    $"  You are {this.u.Character.Name}, an 18 year-old adventurer.\n\n" +
                    $"  You hear rumors that the space princess, Kanna Endrick, has been captured by a\n" +
                    $"  space pirate, a nefarious villain known by the name of Hairy Tenderson.\n\n" +
                    $"  According to this rumor, he will only release her if he is wire transferred\n" +
                    $"  ¤10,002 Starbucks.\n\n" +
                    $"  You have ¤74 Starbucks.\n" +
                    $"  You are low on fuel.\n" +
                    $"  'Too easy,' you say to yourself.\n\n\n" +
                    $"  And so beginneth your adventureth.\n\n" +
                    $"");
			}
		}

        public void GameOver(string message)
        {
            Console.Clear();

            string deathScreen = @"                                                                   
                           .AMMMMMMMMMMA.          
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
            Console.WriteLine($"\"{message}\"");

            // The screen should be displayed for a minimum of 3 seconds.
            System.Threading.Thread.Sleep(3000);

            // Take input and hide the screen.
            Console.WriteLine("\n\n\n\n");
            Console.Write("  Press any key to exit...  ");
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
            Console.ReadKey(false);
            Console.Clear();
            Environment.Exit(0);
        }

        private void RenderStory(string Message)
		{
            // Clear the input buffer.
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }

            // Clear the screen.
            Console.Clear();

            // Render the story message.
            Console.WriteLine("\n\n\n");
			Console.WriteLine("  +-------------------------------------------------------------+");
			Console.WriteLine("  |                                                             |");
			Console.WriteLine("  |                     THE STORY CONTINUES...                  |");
			Console.WriteLine("  |                                                             |");
			Console.WriteLine("  +-------------------------------------------------------------+");
			Console.WriteLine("\n\n\n");
			Console.WriteLine($"{Message}");
			Console.WriteLine("\n\n\n\n\n\n");

            // The screen should be displayed for a minimum of 3 seconds.
            System.Threading.Thread.Sleep(3000);

            // Take input and hide the screen.
            Console.Write("  Press any key to continue...  ");
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
            Console.ReadKey(false);
            Console.Clear();
        }
	}
}
