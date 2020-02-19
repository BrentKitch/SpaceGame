using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SpaceGame
{
	public class Game
	{
		public Universe U
		{
			get; set;
		}

		public UserInterface UI;
		public Menu Menu
		{
			get; set;
		}

		public Game(Universe u)
		{
			this.U = u;
			this.Menu = new Menu(u);
			this.UI = new UserInterface(this.U, this.Menu);
		}

		// TODO: Game loop.
		public void Step()
		{
			do
			{
				Console.Clear();
				this.UI.RenderGame(this.U, this.Menu);
				this.Save();
				while (Console.KeyAvailable)
				{
					Console.ReadKey(false);
				}
				bool properKey = false;
				do
				{
					ConsoleKey keyInput = Console.ReadKey(true).Key;
					foreach (MenuItem menuItem in this.Menu.MenuItems)
					{
						if (menuItem.Key == keyInput)
						{
							properKey = true;
							menuItem.Execute();
						}
					}
				}while (!properKey);
				
			} while (true);
		}

		void CheckObjectives()
		{

		}

		// Game initialization (i.e. startup) sequence.
		public void Initialize()
		{
			//displays menu
			ConsoleKeyInfo option = OpeningSequence.Menu();
			Console.Clear();
			//exits if user wants to quit
			if (option.Key == ConsoleKey.D3)
			{
				Environment.Exit(0);
			}

			//runs opening animation
			OpeningSequence.Animation();

			if (option.Key == ConsoleKey.D1)
			{
				this.Load();
			}
			else
			{
				// Add a character to the universe.
				Console.WriteLine("Enter a name");
				string name = Console.ReadLine();

					Character character = new Character(name, Gender.Male, new Coordinates(8, 12));
					character.Spaceship.Fuel = 30; // Low fuel!
					this.U.Add(character);

				//////////////////////////////////////////////////////////////////////////
				//
				// MARS
				//
				//////////////////////////////////////////////////////////////////////////

				// Planet
				CelestialBody mars = new Planet("Mars", "The big red boi.", ConsoleColor.Red,
					new Coordinates(15, 50),
					new List<ItemCategory> { ItemCategory.Medical });

				// Shop
				mars.AddItem(new Item("Broccoli", "It's like a tree, but gross.", 10, 1,
					new List<ItemCategory> { ItemCategory.Military, ItemCategory.Medical }));
				mars.AddItem(new Item("Chocolate Tree", "It's like a tree, but delicious.", 15, 1,
					new List<ItemCategory> { ItemCategory.Military, ItemCategory.Medical }));

				// Create it!
				this.U.Add(mars);

				//////////////////////////////////////////////////////////////////////////

				//////////////////////////////////////////////////////////////////////////
				//
				// NEPTUNE
				//
				//////////////////////////////////////////////////////////////////////////

				// Planet
				CelestialBody neptune = new Planet("Neptune", "Holy Neptune!", ConsoleColor.Blue,
					new Coordinates(5, 12),
					new List<ItemCategory> { ItemCategory.Military });

				// Shop
				neptune.AddItem(new Item("Broccoli", "It's like a tree, but gross.", 10, 1,
					new List<ItemCategory> { ItemCategory.Military, ItemCategory.Medical }));
				neptune.AddItem(new Item("Holy Water", "Don't drink this!", 100, 1,
					new List<ItemCategory> { ItemCategory.Alcohol, ItemCategory.Medical }));

				// Create it!
				this.U.Add(neptune);

				//////////////////////////////////////////////////////////////////////////

				//////////////////////////////////////////////////////////////////////////
				//
				// URANUS
				//
				//////////////////////////////////////////////////////////////////////////

				// Planet
				CelestialBody uranus = new Planet("Uranus", "No, not that one!", ConsoleColor.Yellow,
					new Coordinates(40, 22),
					new List<ItemCategory> { ItemCategory.Alcohol });

				// Shop
				uranus.AddItem(new Item("Broccoli", "It's like a tree, but gross.", 10, 1,
					new List<ItemCategory> { ItemCategory.Military, ItemCategory.Medical }));
				uranus.AddItem(new Item("Space Gunk", "You don't want to know what this is.", 2, 1,
					new List<ItemCategory> { ItemCategory.Military, ItemCategory.Medical }));
				uranus.AddItem(new Item("Flux Capacitor", "Keeps you young. Or old, depending on your perspective.", 500, 5,
					new List<ItemCategory> { ItemCategory.Military, ItemCategory.Medical }));

				// Create it!
				this.U.Add(uranus);

					//////////////////////////////////////////////////////////////////////////
					//
					// STARS
					//
					//////////////////////////////////////////////////////////////////////////

					// Sol
					CelestialBody sol = new Star("Sol", "Your birth star. There's no place like home.", ConsoleColor.Yellow,
						new Coordinates(40, 22));
					this.U.Add(sol);

					// Proxima Centauri
					CelestialBody proximaCentauri = new Star("Proxima Centauri", "The closest sun to the sun. Unremarkable in every other way.", ConsoleColor.Red,
						new Coordinates(40, 22));
					this.U.Add(proximaCentauri);

					//////////////////////////////////////////////////////////////////////////
				}
			}
		

		// Loads the game if a saved file exists.
		public void Load()
		{
			string json;

			// If the save file does not exist... don't load it.
			if (File.Exists("Objectives.BAK") && File.Exists("Character.BAK") && File.Exists("CelestialBodies.BAK"))
			{
				// Load the objectives.
				json = File.ReadAllText("Objectives.BAK");
				this.U.Objectives = JsonConvert.DeserializeObject<List<Objective>>(json, new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
					TypeNameHandling = TypeNameHandling.All
				});

				// Load the character.
				json = File.ReadAllText("Character.BAK");
				this.U.Character = JsonConvert.DeserializeObject<Character>(json, new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
					TypeNameHandling = TypeNameHandling.All
				});

				// Load the celestial bodies.
				json = File.ReadAllText("CelestialBodies.BAK");
				this.U.CelestialBodies = JsonConvert.DeserializeObject<List<CelestialBody>>(json, new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
					TypeNameHandling = TypeNameHandling.All
				});
			}
			else
			{
				this.U = null;
			}
		}

		// Saves the game.
		public void Save()
		{
			string json;

			// Save the objectives.
			json = JsonConvert.SerializeObject(this.U.Objectives, Formatting.Indented, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				TypeNameHandling = TypeNameHandling.All
			});
			File.WriteAllText("Objectives.BAK", json);

			// Save the character.
			json = JsonConvert.SerializeObject(this.U.Character, Formatting.Indented, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				TypeNameHandling = TypeNameHandling.All
			});
			File.WriteAllText("Character.BAK", json);

			// Save the celestial bodies.
			json = JsonConvert.SerializeObject(this.U.CelestialBodies, Formatting.Indented, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				TypeNameHandling = TypeNameHandling.All
			});

			File.WriteAllText("CelestialBodies.BAK", json);
		}

		// Checks for conditions which would result in various menu items, and then
		// builds and returns the corresponding menu.
		public void BuildMenu()
		{
			bool collision = false;
			Menu menu = new Menu(this.U);

			// Is the character in a planet?
			foreach (CelestialBody celestialBody in this.U.CelestialBodies)
			{
				if (this.U.Character.InCollisionPlanet(celestialBody))
				{
					collision = true;

					if (this.U.Character.Spaceship.Fuel >= 10)
					{
						menu.Add(
							new MenuItem($"Leave {celestialBody.Name} (-10 FUEL)",
							new Action(
								this.U,
								"LeaveCelestialBody",
								celestialBody
							),
							ConsoleKey.D0)
						);
					}

					menu.Add(
						new MenuItem("Buy",
						new Action(
							this.U,
							"ChangeMenu",
							new Menu(this.U).ShowShopBuyMenu(celestialBody)
						),
						ConsoleKey.D1)
					);
					menu.Add(
						new MenuItem("Sell",
						new Action(
							this.U,
							"ChangeMenu",
							new Menu(this.U).ShowShopSellMenu(celestialBody)
						),
						ConsoleKey.D2)
					);

					if (this.U.Character.Spaceship.Fuel < this.U.Character.Spaceship.FuelCapacity && this.U.Character.Starbucks >= 20)
					{
						menu.Add(
							new MenuItem($"Refuel (-20 Starbucks)",
							new Action(
								this.U,
								"Refuel"
							),
							ConsoleKey.D3)
						);
					}

					if (this.U.Character.Health < 100 && this.U.Character.Starbucks >= 100)
					{
						menu.Add(
							new MenuItem($"Hospital (-100 Starbucks)",
							new Action(
								this.U,
								"Hospital"
							),
							ConsoleKey.D3)
						);
					}

					break;
				}
			}

			if (!collision)
			{
				menu.StartMovement();
			}

			this.Menu = menu;
		}
	}
}