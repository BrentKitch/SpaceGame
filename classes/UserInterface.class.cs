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
        List<(int, int)> xyPair = new List<(int, int)> { };
        public void RenderGame(Universe u, Menu menu)
        {
            this.u = u;
            this.menu = menu;
            foreach(CelestialBody cb in this.u.CelestialBodies)
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
            for(int i = 0; i < 30; i++)  //writes height(y)
            {
                
                for(int a = 0; a < 120; a++, starCounter++) // writes width(x)
                {  
                    if(u.Character.Coordinates.X == a && u.Character.Coordinates.Y == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (xyPair.Contains((i, a)))
                        {
                            Console.BackgroundColor = u.CelestialBodies.ElementAt(xyPair.IndexOf((i, a))).Color;
                            
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
                            a++;
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
                    else if (xyPair.Contains((a, i)))
                    {
                        Console.BackgroundColor = u.CelestialBodies.ElementAt(xyPair.IndexOf((a, i))).Color;
                        Console.Write(" ");
                        Console.Write(" ");
                        a++;
                    }
                    else if ((i != 0 || i != 29) && (a == 0 || a == 119))        // makes box for map
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                        Console.ResetColor();
                    }
                    else if (i == 0 || i == 29)
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
                   
                     if (a == 119)   //returns to the next line when it reaches the set map width. 
                    {
                        Console.Write("\n");
                    }
                    
                }
            }
            Console.ResetColor();
        }
        void RenderMenu()
        {
            
            for(int i = 0; i < 20; i++)
            {
                for(int a = 0; a < 120; a++)
                {
                    //if(i == 2)
                    //{
                    //    if(a == 2)
                    //    {
                    //        Console.ResetColor();
                    //        Console.Write(u.Character.Name);
                    //        a += u.Character.Name.Length;
                    //    }else if( a == 55)
                    //    {
                    //        Console.WriteLine($"${u.Character.Starbucks}");
                    //        a += (u.Character.Starbucks.ToString().Length + 1);
                    //    }
                        
                    //}
                    
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
                    else if(i == 0 || i == 19)
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
            Console.SetCursorPosition(55, 32);
            Console.Write($"¤{u.Character.Starbucks} Starbucks");
        }
		public void ShowStory(Universe u)
		{
			// Clear the input buffer.
			while (Console.KeyAvailable)
			{
				Console.ReadKey(false);
			}
			Console.Clear();

			// The screen should be displayed for a minimum of 2 seconds.
			System.Threading.Thread.Sleep(2000);

			// Show messages.
			this.ShowMessage(u);

			// Take input and hide the screen.
			Console.WriteLine("  Press any key to continue...");
			while (Console.KeyAvailable)
			{
				Console.ReadKey(false);
			}
			Console.ReadKey(false);
			Console.Clear();
		}

		private void ShowMessage(Universe u)
		{
			if (u.Character.Age == 780) // 65 years old.
			{
				this.RenderStory($"" +
					$"You turn 65.\n\n" +
					$"Your body fails you.\n\n" +
					$"Your vitals give way.\n\n" +
					$"You die.");
			}
			else if (u.Character.Age == 768) // 64 years old.
			{
				this.RenderStory($"" +
					$"You receive another letter in your SpaceMail inbox.\n" +
					$"It's from the doctor.!\n\n" +
					$"'You've got exactly one years left to live,' it says.\n\n" +
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
						$"The princess is probably really lonely.\n\n" +
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
			else if (u.Character.Age == 636) // 53 years old.
			{
				this.RenderStory($"");
			}
			else if (u.Character.Age == 612) // 51 years old.
			{
				this.RenderStory($"");
			}
			else if (u.Character.Age == 588) // 49 years old.
			{
				this.RenderStory($"" +
					$"You receive another letter in your SpaceMail inbox.\n" +
					$"It's from the princess!\n\n" +
					$"'Pretty sure I just went through menopause,' it says.\n\n" +
					$"On the plus side, you didn't really want kids anyway.");
			}
			else if (u.Character.Age == 564) // 47 years old.
			{
				this.RenderStory($"" +
					$"You are starting to feel really old.\n\n" +
					$"You have a hunch you won't make it past sixty-five.\n\n" +
					$"But it's just a hunch.");
			}
			else if (u.Character.Age == 540) // 45 years old.
			{
				this.RenderStory($"" +
					$"You receive a letter your SpaceMail inbox.\n" +
					$"It's from the princess!\n\n" +
					$"'Please hurry,' it says.\n\n" +
					$"No shit.");
			}
			else if (u.Character.Age == 480) // 40 years old.
			{
				this.RenderStory($"" +
					$"You just turned 40 years old.\n\n" +
					$"...and you're still single.");
			}
			else if (u.Character.Age == 468) // 39 years old.
			{
				this.RenderStory($"It's been 20 years, and you're still single.\n\n" +
					$"You're 39 years old." +
					$"'Oof,' you mumble to yourself." +
					$"");
			}
			else if (u.Character.Age == 420) // 35 years old.
			{
				this.RenderStory($"" +
					$"Some quick math confirms you are 420 months old.\n\n" +
					$"You chuckle to yourself.\n\n" +
					$"Somehow, this fills you with determination.");
			}
			else if (u.Character.Age == 396) // 33 years old.
			{
				this.RenderStory($"" +
					$"  'What is the point,' you ask yourself. 'She doesn't even know me.'\n\n" +
					$"  You have a point.");
			}
			else if (u.Character.Age == 372) // 31 years old.
			{
				this.RenderStory($"" +
					$"  As of today, you have been traveling for an entire decade.\n\n" +
					$"  The princess needs you now more than ever.");
			}
			else if (u.Character.Age == 276) // 23 years old.
			{
				this.RenderStory($"" +
					$"  It has been five years.\n\n" +
					$"  You only have {u.Character.Starbucks} Starbucks.\n\n\n\n" +
					$"  Get it together. The princess needs your help.");
			}
			else if (u.Character.Age == 252) // 21 years old.
			{
				this.RenderStory($"" +
					$"  In the distance, you think you hear the princess cry for your name.\n\n" +
					$"  'Help me, {u.Character.Name}!'\n\n" +
					$"  But sound doesn't travel in space.");
			}
			else if (u.Character.Age == 228) // 19 years old.
			{
				this.RenderStory($"" +
					$"  It has been one year since you embarked on your adventure.\n\n" +
					$"  You must save the princess!");
			}
		}

		private void RenderStory(string Message)
		{
			Console.WriteLine("\n\n\n");
			Console.WriteLine("+-------------------------------------------------------------+");
			Console.WriteLine("|                                                             |");
			Console.WriteLine("|                     THE STORY CONTINUES...                  |");
			Console.WriteLine("|                                                             |");
			Console.WriteLine("+-------------------------------------------------------------+");
			Console.WriteLine("\n\n\n");
			Console.WriteLine($"\"{Message}\"");
			Console.WriteLine("\n\n\n\n\n\n");
		}
	}
}
