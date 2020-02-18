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
                    else if (xyPair.Contains((i, a)))
                    {
                        Console.BackgroundColor = u.CelestialBodies.ElementAt(xyPair.IndexOf((i, a))).Color;
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
    }
}
