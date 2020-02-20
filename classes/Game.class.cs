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

		public UserInterface UserInterface;
		public Menu Menu
		{
			get; set;
		}

		public Game(Universe u)
		{
			this.U = u;
			this.Menu = new Menu(u);
			this.UserInterface = new UserInterface(this.U, this.Menu);
		}

		// TODO: Game loop.
		public void Step()
		{
			do
			{
				// Check if the character is dead.
				if (this.U.Character.Health <= 0)
				{
					// If the character is inside a star, hurt them.
					foreach (CelestialBody celestialBody in this.U.CelestialBodies)
					{
						if (this.U.Character.InCollisionStar(celestialBody))
						{
							this.UserInterface.GameOver("  Stars are not good places to pass time.\n\n" +
								"You are burnt to a crisp.");
						}
					}

					this.UserInterface.GameOver("  You ran out of health and died.\n\n" +
						"It was a good run!");
				}

				if (this.U.Character.Coordinates.X <= 0
					|| this.U.Character.Coordinates.X >= 119
					|| this.U.Character.Coordinates.Y <= 0
					|| this.U.Character.Coordinates.Y >= 29
					)
				{
					if (this.U.Character.Spaceship.Fuel <= 0)
					{
						this.UserInterface.GameOver("  You ran out of fuel!\n\n" +
							"You drift off into space for a few more days, but ultimately die alone in the abyss.\n\n\n" +
							"The space princess is never rescued.");
					}
					else
					{
						this.UserInterface.GameOver("  You drift off into space.\n\n" +
							"You get lost.\n\n" +
							"You never find your way back, and you never save the princess.");
					}
				}



				Console.Clear();
				this.UserInterface.RenderGame(this.U, this.Menu);
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
				} while (!properKey);

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
			if (option.Key == ConsoleKey.D1)
			{
				
				//runs opening animation
				OpeningSequence.Animation();
				this.Load();

				if (this.U.Character.Starbucks < Universe.StarbucksToSavePrincess)
				{
					this.UserInterface.RenderStory($"" +
						$"  Welcome back!\n\n" +
						$"  Your name is {this.U.Character.Name}. You are {this.U.Character.Age / 12} years old.\n\n" +
						$"  You have ¤{this.U.Character.Starbucks} Starbucks.\n" +
						$"  You need ¤{Universe.StarbucksToSavePrincess - this.U.Character.Starbucks} more Starbucks to save the princess, KANNA ENDRICK.\n\n" +
						$"  And thus continueth your adventureth.\n\n" +
						$"");
				}
				else
				{
					this.UserInterface.RenderStory($"" +
						$"  Welcome back!\n\n" +
						$"  Your name is {this.U.Character.Name}. You are {this.U.Character.Age / 12} years old.\n\n" +
						$"  You have ¤{this.U.Character.Starbucks} Starbucks.\n" +
						$"  That's enough to save the princess!\n\n" +
						$"  You must hurry!\n\n" +
						$"");
				}
			}
			else
			{
				// Add a character to the universe.
				OpeningSequence.NewCharacterHeader();

				string name;
				do
				{
					Console.WriteLine("Whats your name?");
					name = Console.ReadLine().ToUpper();
				} while (name == "");

				Gender gender = Gender.Alien;
				int genderChoice = 4;
				do
				{
					Console.WriteLine("Are you a boy(1), girl(2), or alien(3)?");
					genderChoice = int.Parse(Console.ReadLine());

					if (genderChoice == 1)
					{
						gender = Gender.Male;
					}
					else if (genderChoice == 2)
					{
						gender = Gender.Female;
					}
					else if (genderChoice == 3)
					{
						gender = Gender.Alien;
					}
						} while (genderChoice != 1 && genderChoice != 2 && genderChoice != 3);

				Character character = new Character(name, gender, new Coordinates(8, 12));
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
					new Coordinates(110, 05));
				this.U.Add(sol);

				// Proxima Centauri
				CelestialBody proximaCentauri = new Star("Proxima Centauri", "The closest sun to the sun. Unremarkable in every other way.", ConsoleColor.Red,
					new Coordinates(50, 13));
				this.U.Add(proximaCentauri);

				//////////////////////////////////////////////////////////////////////////

				Console.Clear();
				//runs opening animation
				OpeningSequence.Animation();

				// Start the game.

				this.UserInterface.RenderStory($"" +
					$"  Your journey begins.\n\n" +
					$"  You are {this.U.Character.Name}, an 18 year-old adventurer.\n\n" +
					$"  You hear rumors that the space princess, KANNA ENDRICK, has been captured by a\n" +
					$"  space pirate, a nefarious villain known by the name of HAIRY TENDERSON.\n\n" +
					$"  According to this rumor, he will only release her if he is wire transferred\n" +
					$"  ¤10,002 Starbucks.\n\n" +
					$"  You have ¤{this.U.Character.Starbucks} Starbucks.\n" +
					$"  You are low on fuel.\n\n" +
					$"  'Too easy,' you say to yourself.\n\n" +
					$"  And so beginneth your adventureth.\n\n" +
					$"");
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

			if (this.U.Character.Starbucks >= Universe.StarbucksToSavePrincess)
			{
				menu.Add(
					new MenuItem($"Wire Transfer (-¤{Universe.StarbucksToSavePrincess} Starbucks)",
					new Action(
						this.U,
						"Win"
					),
					ConsoleKey.Tab)
				);
			}

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

					menu.Add(
						new MenuItem("Paint Spaceship",
						new Action(
							this.U,
							"ChangeMenu",
							new Menu(this.U).ShowPaintSpaceshipMenu()
						),
						ConsoleKey.D3)
					);

					if (this.U.Character.Spaceship.Fuel < this.U.Character.Spaceship.FuelCapacity && this.U.Character.Starbucks >= 20)
					{
						menu.Add(
							new MenuItem($"Refuel (-20 Starbucks)",
							new Action(
								this.U,
								"Refuel"
							),
							ConsoleKey.D4)
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
							ConsoleKey.D5)
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