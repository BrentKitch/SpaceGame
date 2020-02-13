using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SpaceGame
{
    public class UI
    {
        private 
        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();

        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int HIDE = 0;

        private const int MAXIMIZE = 3;

        private const int MINIMIZE = 6;

        private const int RESTORE = 9;

        public static void Main(string[] args)
        {
            new UI().RenderGame();
        }
        void RenderGame()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            ShowWindow(ThisConsole, MAXIMIZE);
            RenderMap();
            RenderMenu();
            Console.ReadLine();
        }
        void RenderMap()  
        {   
            int starCounter = 0; // counts through the following for loop to give a star distribution
            for(int i = 0; i < 30; i++)  //writes height
            {
                
                for(int a = 0; a < 120; a++, starCounter++) // writes width
                {
                    if ((i != 0 || i != 29) && (a == 0 || a == 119))        // makes box for map
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
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
                    //if ((a * (i +1)) % 24 == 0) 
                    //{
                    //    Console.Write(".");
                    //}
                   
                     if (a == 119)   //returns to the next line when it reaches the set map width. 
                    {
                        Console.Write("\n");
                    }
                    
                }
            }
        }
        void RenderMenu(/*Menu menuUI*/)
        {
            for(int i = 0; i < 20; i++)
            {
                for(int a = 0; a < 120; a++)
                {
                    if((i != 0 || i != 19) && (a == 0 || a == 119))  // makes box for menu
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
        }
    }
}
