using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SpaceGame
{
    class Game
    {
        public Universe U {get; set;}

        void Step()
        {

        }

        void Start()
        {

        }
        
        void CheckObjectives()
        {

        }

        public void Run()
        {
            Console.WriteLine("Welcome to Space Mario 2049 with some science.");
            Console.WriteLine("Do you want to load an old save file? (Y or N)");
            string areWeLoading = Console.ReadLine();
            areWeLoading = areWeLoading.ToUpper();

            Game game = new Game();
            game.Load();

            if (areWeLoading == "Y" && game.U != null)
            {
                Console.WriteLine("Welcome back " + game.U.Character.Name);
                Console.WriteLine("Your current Coordinates are (" + game.U.Character.Position.X + "," + game.U.Character.Position.Y + ")");
                Console.WriteLine("Your current health is " + game.U.Character.Health);
                Console.WriteLine("Your have " + game.U.Character.Currency + " starbucks");
            }
            else
            {
                game.U = new Universe();

                // Add a character to the universe.
                Console.WriteLine("Enter a name");
                string name = Console.ReadLine();

                Character character = new Character(name, Gender.Male);
                game.U.Add(character);

                // add a planet to the universe.
                CelestialBody mars = new Planet("mars", "the big red boi.", Color.Red);
                game.U.Add(mars);

            }

            // Create a menu.
            Menu menu = new Menu(game.U);

            // Add movement options.
            menu.StartMovement();

            game.Save();
        }
        public void Load()
        {
            // Load the saved file, if possible.
            string path = "SpaceGame.BAK";

            // If the save file does not exist... don't load it.
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                this.U = JsonConvert.DeserializeObject<Universe>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            }
            else
            {
                this.U = null;
            }
        }
        public void Save(string path = "SpaceGame.BAK")
        {
            // Serialize the Tasks list.
            string json = JsonConvert.SerializeObject(this.U, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            // Save the serialized Tasks list to the specified path.
            File.WriteAllText(path, json);
        }
    }
}
