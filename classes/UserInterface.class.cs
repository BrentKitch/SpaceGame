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
        private CelestialBody collisionBody
        {
            get; set;
        }
        private (int,int) prevPosition
        {
            get;set;
        }
        private Universe preUniverse
        {
            get; set;
        }
        private Menu preMenu
        {
            get; set;
        }
        private int messageLength
        {
            get; set;
        }
        private int starbuckLength
        {
            get; set;
        }
        private int healthLength
        {
            get; set;
        }
        private int fuelLength
        {
            get; set;
        }
        private int coordinateLength
        {
            get;set;
        }
        List<(int, int)> xyPair = new List<(int, int)> { };
        char[,] map = new char[120,30];
        List<char> uniqueIdentifierList = new List<char> { };
        public void GenerateMap(Universe u, Menu menu)
        {
            xyPair.Clear();
            this.U = u;
            this.Menu = menu;
            prevPosition = (this.U.Character.Coordinates.X, this.U.Character.Coordinates.Y);
            messageLength = 0;
            foreach (CelestialBody cb in this.U.CelestialBodies)
            {
                xyPair.Add((cb.Coordinates.X, cb.Coordinates.Y));
            }
            int starCounter = 0; // counts through the following for loop to give a star distribution
            for (int y = 0; y < 30; y++)  //writes height(y)
            {

                for (int x = 0; x < 120; x++, starCounter++) // writes width(x)
                {
                    if (xyPair.Contains((x, y)))
                    {
                        if (U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Type == "star")
                        {
                            map[x, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).uniqueIdentifier;
                            map[x + 1, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).uniqueIdentifier;
                            map[x + 2, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).uniqueIdentifier;
                            map[x + 3, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).uniqueIdentifier;
                            x += 3;
                        }
                        else 
                        {
                            map[x, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).uniqueIdentifier;
                            map[x + 1, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).uniqueIdentifier;
                            x++;
                        }

                    }
                    else if (xyPair.Contains((x, y - 1)) && U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1))).Type == "star")
                    {
                        map[x, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1))).uniqueIdentifier;
                        map[x + 1, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1))).uniqueIdentifier;
                        map[x + 2, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1))).uniqueIdentifier;
                        map[x + 3, y] = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1))).uniqueIdentifier;
                        x += 3;
                    }
                    else if ((y != 0 || y != 29) && (x == 0 || x == 119))        // makes box for map
                    {
                        map[x, y] = '+';
                    }
                    else if (y == 0 || y == 29)
                    {
                        map[x, y] = '+';
                    }
                    else
                    {
                        if (starCounter % 14 == 0 && starCounter % 5 == 0 || starCounter % 100 == 0) //distribution of stars 
                        {
                            if (starCounter % 14 == 0)                   //changes color of stars on based on mod properties to get a "random" look to the star display
                            {
                                //Console.ForegroundColor = ConsoleColor.DarkGray;
                                //Console.Write(".");
                                map[x, y] = '9';
                            }
                            else if (starCounter % 200 == 0)
                            {
                                //Console.ForegroundColor = ConsoleColor.DarkYellow;
                                //Console.Write(".");
                                map[x, y] = '8';
                            }
                            else if (starCounter % 5 == 0)
                            {
                                //Console.ForegroundColor = ConsoleColor.Yellow;
                                //Console.Write(".");
                                map[x, y] = '7';
                            }
                            else
                            {
                                //Console.ResetColor();
                                //Console.Write(".");
                                map[x, y] = '6';
                            }

                        }
                        else if (starCounter % 76 == 0)  // writes an '*' instead of a '.' as a star
                        {
                            //Console.ForegroundColor = ConsoleColor.DarkYellow;
                            //Console.Write("*");
                            map[x, y] = '*';
                        }
                        else
                        {
                            map[x, y] = '0';
                        }
                    }
                }
            }
            //displayMap();
        }
        public void displayMap()
        {
            foreach (CelestialBody cb in this.U.CelestialBodies)
            {
                uniqueIdentifierList.Add(cb.uniqueIdentifier);
            }
            for (int y = 0; y < 30; y++)
            {
                for (int x = 0; x < 120; x++)
                {
                    if (uniqueIdentifierList.Contains(map[x, y]))
                    {
                        Console.BackgroundColor = this.U.CelestialBodies.ElementAt(uniqueIdentifierList.IndexOf(map[x, y])).Color;
                        if (this.U.CelestialBodies.ElementAt(uniqueIdentifierList.IndexOf(map[x, y])).Type == "star")
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("\x2592");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                        Console.ResetColor();
                    }
                    else if (map[x, y] == '9')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(".");
                    }
                    else if (map[x, y] == '8')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(".");
                    }
                    else if (map[x, y] == '7')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(".");
                    }
                    else if (map[x, y] == '6')
                    {
                        Console.ResetColor();
                        Console.Write(".");
                    }
                    else if (map[x, y] == '*')
                    {
                        Console.Write("*");
                    }
                    else if (map[x,y] == '+')        // makes box for map
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }
            RenderMenu();

        }

        public void RenderGame(Universe u, Menu menu)
        {
           
            this.U = u;
            this.Menu = menu;
            //foreach (CelestialBody cb in this.U.CelestialBodies)
            //{
            //        xyPair.Add((cb.Coordinates.X,cb.Coordinates.Y));
            //}
            //Console.Clear();
            //GenerateMap();
            //RenderMap();
            //displayMap();
            //RenderMenu();
            drawShip();
            DisplayInformation();
            
        }
        void RenderMap()  
        {   
            int starCounter = 0; // counts through the following for loop to give a star distribution
            for(int y = 0; y < 30; y++)  //writes height(y)
            {
                
                for(int x = 0; x < 120; x++, starCounter++) // writes width(x)
                {  
                    if(U.Character.Coordinates.X == x && U.Character.Coordinates.Y == y)
                    {
                        //if(this.U.Character.Collision)
                        if (xyPair.Contains((x, y)))
                        {
                            Console.BackgroundColor = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Color;
                            collisionBody = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y)));
                            if (U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Type == "star")
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                drawShip();
                                Console.Write("\x2592");
                                Console.Write("\x2592");
                                Console.Write("\x2592");
                                x += 3;
                            }
                            else
                            {
                                drawShip();
                                Console.Write(" ");
                                x++;
                            }
                            Console.ResetColor();
                        }
                        else if(xyPair.Contains((x, y - 1)) && U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y - 1))).Type == "star") // to prevent index out of bounds errors
                        {
                            Console.BackgroundColor = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1))).Color;
                            collisionBody = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1)));
                            Console.ForegroundColor = ConsoleColor.Black;
                            if (U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y - 1))).Type == "star")
                            {
                                drawShip();
                                Console.Write("\x2592");
                                Console.Write("\x2592");
                                Console.Write("\x2592");
                                x += 3;
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            drawShip();
                            Console.ResetColor();
                        }
                    }
                    else if (xyPair.Contains((x, y)) && U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Type == "star")
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Color;
                        Console.Write("\x2592");
                        if (this.U.Character.Coordinates.X == x + 1 && this.U.Character.Coordinates.Y == y)
                        {
                            collisionBody = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y)));
                            drawShip();
                            Console.Write("\x2592");
                            Console.Write("\x2592");
                        }
                        else if(this.U.Character.Coordinates.X == x + 2 && this.U.Character.Coordinates.Y == y)
                        {
                            collisionBody = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y)));
                            Console.Write("\x2592");
                            drawShip();
                            Console.Write("\x2592");
                        }
                        else if (this.U.Character.Coordinates.X == x + 3 && this.U.Character.Coordinates.Y == y)
                        {
                            collisionBody = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y)));
                            Console.Write("\x2592");
                            Console.Write("\x2592");
                            drawShip();
                        }
                        else
                        {
                            Console.Write("\x2592");
                            Console.Write("\x2592");
                            Console.Write("\x2592");
                        }
                        x+=3;
                        Console.ResetColor();
                    }
                    else if (xyPair.Contains((x, y-1)) && U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1))).Type == "star")
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1))).Color;
                        Console.Write("\x2592");
                        if (this.U.Character.Coordinates.X == x + 1 && this.U.Character.Coordinates.Y == y)
                        {
                            collisionBody = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1)));
                            drawShip();
                            Console.Write("\x2592");
                            Console.Write("\x2592");
                        }
                        else if (this.U.Character.Coordinates.X == x + 2 && this.U.Character.Coordinates.Y == y)
                        {
                            collisionBody = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1)));
                            Console.Write("\x2592");
                            drawShip();
                            Console.Write("\x2592");
                        }
                        else if (this.U.Character.Coordinates.X == x + 3 && this.U.Character.Coordinates.Y == y)
                        {
                            collisionBody = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y-1)));
                            Console.Write("\x2592");
                            Console.Write("\x2592");
                            drawShip();
                        }
                        else
                        {
                            Console.Write("\x2592");
                            Console.Write("\x2592");
                            Console.Write("\x2592");
                        }
                        x += 3;
                        Console.ResetColor();
                    }
                    else if (xyPair.Contains((x, y)))
                    {
                        Console.BackgroundColor = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y))).Color;
                        Console.Write(" ");
                        if (this.U.Character.Coordinates.X == x+1 && this.U.Character.Coordinates.Y == y)
                        {
                            collisionBody = U.CelestialBodies.ElementAt(xyPair.IndexOf((x, y)));
                            drawShip();
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
                        Console.ResetColor();
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
            Console.SetCursorPosition(0, 30);
            for(int i = 0; i < 17; i++)
            {
                for(int a = 0; a < 120; a++)
                {                   
                    if(i == 4)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(" ");
                       
                    }
                    else if((i == 7 || i == 13) && a >= 5 && a <= 13)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(" ");
                    }
                    else if(i > 7 && i < 13 && (a == 5 || a == 13))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(" ");
                    }
                    else if(i >= 9 && i <= 11 && a >= 7 && a <= 11)
                    {
                        if (collisionBody != null)
                        {
                            Console.BackgroundColor = collisionBody.Color;
                            Console.Write(" ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.Write(" ");
                        }
                    }
                    else if((i != 0 || i != 16) && (a == 0 || a == 119))  // makes box for menu
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
        }

        private void DisplayInformation() 
        {
            Eraser(messageLength, 2, 28);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(this.U.Message);
            messageLength = this.U.Message.Length;
            Console.ResetColor();
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
            Console.SetCursorPosition(25, 32);
            Console.Write($"Age: {this.U.Character.Age / 12}");
            //Console.SetCursorPosition(50, 32);
            Eraser(starbuckLength, 50, 32);
            Console.Write($"¤{U.Character.Starbucks} Starbucks");
            starbuckLength = ($"¤{U.Character.Starbucks} Starbucks").Length;
            //Console.SetCursorPosition(81, 32);
            Eraser(healthLength, 81, 32);
            Console.Write($"Health {this.U.Character.Health}/100");
            healthLength = ($"Health {this.U.Character.Health}/100").Length;
            //Console.SetCursorPosition(105, 32);
            Eraser(fuelLength, 105, 32);
            Console.Write($"Fuel {this.U.Character.Spaceship.Fuel}/{this.U.Character.Spaceship.FuelCapacity}");
            fuelLength = ($"Fuel {this.U.Character.Spaceship.Fuel}/{this.U.Character.Spaceship.FuelCapacity}").Length;
            Console.SetCursorPosition(5, 35);
            Console.Write("Location");
            Console.SetCursorPosition(4, 36);
            Console.Write("Information");
            //Console.SetCursorPosition(5, 44);
            Eraser(coordinateLength, 5,44);
            Console.Write($"( {this.U.Character.Coordinates.X},{this.U.Character.Coordinates.Y} )");
            coordinateLength = ($"( {this.U.Character.Coordinates.X},{this.U.Character.Coordinates.Y} )").Length;
            Console.SetCursorPosition(20, 36);
            (int, int) cursorTrack;
            if (this.U.Character.Collision || this.U.Character.starMarker)
            {
                
                
                for (int i =0; i < 3; i++)
                {
                    Console.SetCursorPosition(7, 39+i);
                    for (int a = 0; a <5; a++)
                    {
                        Console.BackgroundColor = this.U.Character.collisionBody.Color;
                        Console.Write(" ");
                    }
                }
                Console.ResetColor();
                Console.Write(this.U.Character.collisionBody.Name);
                Console.SetCursorPosition(17, 38);
                if (this.U.Character.collisionBody.Description.Length > 38)
                {
                    Console.Write(this.U.Character.collisionBody.Description.Substring(0, 38));
                    Console.SetCursorPosition(17, 39);
                    Console.Write(this.U.Character.collisionBody.Description.Substring(38));

                }
                else
                {
                    Console.Write(this.U.Character.collisionBody.Description);
                }
                cursorTrack = (17, 41);
                foreach(ItemCategory iC in this.U.Character.collisionBody.FavoredItemCategories)
                {
                    Console.SetCursorPosition(cursorTrack.Item1, cursorTrack.Item2);
                    Console.Write(iC);
                    cursorTrack.Item2++;
                }
                   
            }
            else
            {
                Console.Write("Space");
                Console.SetCursorPosition(17, 38);
                Console.Write("The final frontier");
            }

            //Console.SetCursorPosition(61, 35);
            Eraser($"Inventory: {this.U.Character.Inventory.Count}".Length, 61, 35);
            Console.Write($"Inventory: {this.U.Character.Inventory.Count}");
            cursorTrack = (63, 36);
            foreach(Item item in this.U.Character.Inventory)
            {
                Console.SetCursorPosition(cursorTrack.Item1, cursorTrack.Item2);
                Console.Write(item.Name);
                cursorTrack.Item2++;
            }
            cursorTrack = (85, 35);
            foreach( MenuItem menuItem in Menu.MenuItems)
            {
                Console.SetCursorPosition(cursorTrack.Item1, cursorTrack.Item2);
                Console.Write($"{menuItem.Key}: {menuItem.Label}");
                cursorTrack.Item2++;
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
            Console.Write("  Press any key to continue...  ");
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
            Console.ReadKey(false);
            Console.Clear();
        }
        private void drawShip()
        {
            Console.SetCursorPosition(prevPosition.Item1, prevPosition.Item2);
            Draw(prevPosition.Item1, prevPosition.Item2);
            Console.SetCursorPosition(this.U.Character.Coordinates.X, this.U.Character.Coordinates.Y);
            if (this.U.Character.Collision)
            {
                Console.BackgroundColor = this.U.Character.collisionBody.Color;
            }
            else
            {
                Console.ResetColor();
            }
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
            Console.ResetColor();
            prevPosition = (this.U.Character.Coordinates.X, this.U.Character.Coordinates.Y);
        }
        void Draw(int x, int y)
        {
            if (uniqueIdentifierList.Contains(map[x, y]))
            {
                Console.BackgroundColor = this.U.CelestialBodies.ElementAt(uniqueIdentifierList.IndexOf(map[x, y])).Color;
                if (this.U.CelestialBodies.ElementAt(uniqueIdentifierList.IndexOf(map[x, y])).Type == "star")
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("\x2592");
                }
                else
                {
                    Console.Write(" ");
                }
                Console.ResetColor();
            }
            else if (map[x, y] == '9')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(".");
            }
            else if (map[x, y] == '8')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(".");
            }
            else if (map[x, y] == '7')
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(".");
            }
            else if (map[x, y] == '6')
            {
                Console.ResetColor();
                Console.Write(".");
            }
            else if (map[x, y] == '*')
            {
                Console.Write("*");
            }
            else if (map[x, y] == '+')        // makes box for map
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write(" ");
                Console.ResetColor();
            }
            else
            {
                Console.Write(" ");
            }
        }
        void Eraser(int c, int x, int y )
        {
            Console.ResetColor();
            Console.SetCursorPosition(x, y);
            for(int i = 0; i < c; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(x, y);
        }

    }
}
