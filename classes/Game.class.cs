using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SpaceGame
{
	public class Game
	{
		public Random Random;

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
			this.Random = new Random();
			this.U = u;
			this.Menu = new Menu(u);
			this.UserInterface = new UserInterface(this.U, this.Menu);
		}

		// TODO: Game loop.
		public void Step()
		{
			Console.Clear();
			this.UserInterface.GenerateMap(this.U, this.Menu);
			this.UserInterface.displayMap();
			
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
								"  You are burnt to a crisp.");
						}
					}

					this.UserInterface.GameOver("  You ran out of health and died.\n\n" +
						"  It was a good run!");
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
							"  You drift off into space for a few more days, but ultimately die alone in the abyss.\n\n\n" +
							"  The space princess is never rescued.");
					}
					else
					{
						this.UserInterface.GameOver("  You drift off into space.\n\n" +
							"  You get lost.\n\n" +
							"  You never find your way back, and you never save the princess.");
					}
				}

				// If the character has no items, not enough money to buy fuel, and not enough fuel to escape the planet
				// (and is on a planet).
				if (this.U.Character.Inventory.Count <= 0
					&& this.U.Character.Starbucks < 100
					&& this.U.Character.Spaceship.Fuel < 15)
				{
					foreach (CelestialBody celestialBody in this.U.CelestialBodies)
					{
						// If the character is on a planet.
						if (this.U.Character.InCollisionPlanet(celestialBody))
						{
							this.UserInterface.GameOver("  With no money to your name and no fuel to escape,\n" +
							$"  you spend the rest of your days on {celestialBody.Name}.\n\n" +
							$"  You die single and princessless.");
						}
					}
				}
				//Console.Clear();
				
				this.UserInterface.RenderGame(this.U, this.Menu);
				while (Console.KeyAvailable)
				{
					Console.ReadKey(true);
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
					Console.WriteLine("    What's your name?");
					Console.Write("    ");
					name = Console.ReadLine().ToUpper();
					Console.WriteLine("\n\n");
				} while (name == "");

				Gender gender = Gender.Alien;
				ConsoleKeyInfo genderChoice;
				Console.WriteLine("    Are you a Boy (1), Girl (2), or Alien (3)?");
				Console.Write("    ");
				do
				{
					genderChoice = Console.ReadKey(true);

					if (genderChoice.Key == ConsoleKey.D1)
					{
						gender = Gender.Male;
					}
					else if (genderChoice.Key == ConsoleKey.D2)
					{
						gender = Gender.Female;
					}
					else if (genderChoice.Key == ConsoleKey.D3)
					{
						gender = Gender.Alien;
					}
				} while (genderChoice.Key != ConsoleKey.D1 && genderChoice.Key != ConsoleKey.D2 && genderChoice.Key != ConsoleKey.D3);
				
				Console.Clear();
				//runs opening animation
				OpeningSequence.Animation();

				Character character = new Character(name, gender, new Coordinates(17, 22));

				character.Inventory.Add(new Item("Space Gunk", "Looks like the poop emoji.", 5,
					new List<ItemCategory> { ItemCategory.Junk }));
				character.Inventory.Add(new Item("Earwax", "From an alien. Species unknown.", 25,
					new List<ItemCategory> { ItemCategory.Junk }));

				character.Spaceship.Fuel = 30; // Low fuel!
				this.U.Add(character);

				// Build the galaxy.
				this.BuildGalaxy();

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

					if (this.U.Character.Spaceship.Fuel >= 15)
					{
						menu.Add(
							new MenuItem($"Leave {celestialBody.Name} (-15 FUEL)",
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
							new MenuItem($"Refuel (-100 Starbucks)",
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

		// Create the whole galaxy.
		private void BuildGalaxy()
		{
			//////////////////////////////////////////////////////////////////////////
			//
			// ITEMS
			//
			//////////////////////////////////////////////////////////////////////////
			//
			// Alcohol
			// Computers
			// Crafting
			// Creatures
			// Dessert
			// Elements
			// Food
			// Gems
			// Junk
			// Medical
			// Robots
			// Sacred
			// Software
			// Weapons
			//
			//////////////////////////////////////////////////////////////////////////

			var item = new Dictionary<string, Item>();
			item.Add(
				"Space Beer", new Item("Space Beer", "Made from space hops (or something like that).", 10,
					new List<ItemCategory> { ItemCategory.Alcohol, ItemCategory.Food, ItemCategory.Medical }));
			item.Add(
				"Hyperseltzer", new Item("Hyperseltzer", "Its flavor is out of this world. And this galaxy.", 35,
					new List<ItemCategory> { ItemCategory.Alcohol, ItemCategory.Food }));
			item.Add(
				"Dimensional Whiskey", new Item("Dimensional Whiskey", "Literally tastes like the 5th dimension.", 100,
					new List<ItemCategory> { ItemCategory.Alcohol }));
			item.Add(
				"Xenowine", new Item("Xenowine", "Xenoberry extract, aged via time dilation.", 200,
					new List<ItemCategory> { ItemCategory.Alcohol }));
			item.Add(
				"Alienware", new Item("Alienware", "No, not THAT Alienware. Literally wares from an alien.", 80,
					new List<ItemCategory> { ItemCategory.Computers }));
			item.Add(
				"A.I. Chip", new Item("A.I. Chip", "10x smarter than the average human. Smaller than a xenoberry.", 200,
					new List<ItemCategory> { ItemCategory.Computers, ItemCategory.Robots, ItemCategory.Software }));
			item.Add(
				"V.R. Implant", new Item("V.R. Implant", "Allows one to see the 'pataphysical side of (un)reality.", 400,
					new List<ItemCategory> { ItemCategory.Computers, ItemCategory.Medical }));
			item.Add(
				"Superdrive", new Item("Superdrive", "Like a flash drive, but superer.", 850,
					new List<ItemCategory> { ItemCategory.Computers }));
			item.Add(
				"Galaxy Nails", new Item("Galaxy Nails", "It's a combo pack.", 50,
					new List<ItemCategory> { ItemCategory.Crafting }));
			item.Add(
				"Galaxy Hammer", new Item("Galaxy Hammer", "Using for hammering galaxy nails.", 100,
					new List<ItemCategory> { ItemCategory.Crafting }));
			item.Add(
				"Solder Beam", new Item("Solder Beam", "Allows you to solder exotic metals from over one lightyear away.", 190,
					new List<ItemCategory> { ItemCategory.Crafting, ItemCategory.Weapons }));
			item.Add(
				"Robotic Screwdriver", new Item("Robotic Screwdriver", "It really screws!", 320,
					new List<ItemCategory> { ItemCategory.Crafting, ItemCategory.Robots }));
			item.Add(
				"Tribble", new Item("Tribble", "It's a fuzzy wuzzy buddy.", 100,
					new List<ItemCategory> { ItemCategory.Creatures }));
			item.Add(
				"Pet Droid", new Item("Pet Droid", "It's not really alive, but who could tell?", 500,
					new List<ItemCategory> { ItemCategory.Creatures, ItemCategory.Robots }));
			item.Add(
				"Baby Ewok", new Item("Baby Ewok", "Not to be confused with a baby bear.", 500,
					new List<ItemCategory> { ItemCategory.Creatures }));
			item.Add(
				"Fish", new Item("Fish", "Literally just a fish.", 2002,
					new List<ItemCategory> { ItemCategory.Creatures }));
			item.Add(
				"Cocoa Jumbo", new Item("Cocoa Jumbo", "It's a large chocolate... something.", 15,
					new List<ItemCategory> { ItemCategory.Dessert }));
			item.Add(
				"Vanilla Jumbo", new Item("Vanilla Jumbo", "It's a large vanilla... something.", 15,
					new List<ItemCategory> { ItemCategory.Dessert }));
			item.Add(
				"Jumbo Jumbo", new Item("Jumbo Jumbo", "It's a large something... something.", 150,
					new List<ItemCategory> { ItemCategory.Dessert }));
			item.Add(
				"Recursive Jumbo", new Item("Recursive Jumbo", "It's a large [It's a large [It's a large [ It's a large ...] ...] ...] ...", 1515,
					new List<ItemCategory> { ItemCategory.Dessert }));
			item.Add(
				"Hydrogen", new Item("Hydrogen", "2/3 of an entire water molecule.", 50,
					new List<ItemCategory> { ItemCategory.Elements }));
			item.Add(
				"Xenon", new Item("Xenon", "Noble AF.", 250,
					new List<ItemCategory> { ItemCategory.Elements }));
			item.Add(
				"Plutonium", new Item("Plutonium", "Don't breathe this!", 500,
					new List<ItemCategory> { ItemCategory.Elements }));
			item.Add(
				"Platinum Chunk", new Item("Platinum Chunk", "Literally a chunk of expensive junk.", 2000,
					new List<ItemCategory> { ItemCategory.Elements, ItemCategory.Junk }));
			item.Add(
				"Ectoburger", new Item("Ectoburger", "Made with mystery alien meat.", 50,
					new List<ItemCategory> { ItemCategory.Food }));
			item.Add(
				"Xenoberry", new Item("Xenoberry", "Sweet, tart, spicy... and savory?", 80,
					new List<ItemCategory> { ItemCategory.Food }));
			item.Add(
				"Superhot Dog", new Item("Superhot Dog", "Grilled with the energy of a thousand supernovae.", 120,
					new List<ItemCategory> { ItemCategory.Food }));
			item.Add(
				"Edible Stardust", new Item("Edible Stardust", "Guess what it's made out of.", 600,
					new List<ItemCategory> { ItemCategory.Food }));
			item.Add(
				"Broccoli", new Item("Broccoli", "May or may not have magical properties. It's literally just broccoli though.", 9999,
					new List<ItemCategory> { ItemCategory.Food, ItemCategory.Sacred }));
			item.Add(
				"Diamond Supercluster", new Item("Diamond Supercluster", "This is what you get when you combine multiple diamonds into one diamond", 800,
					new List<ItemCategory> { ItemCategory.Gems }));
			item.Add(
				"Purple Gemaflex", new Item("Purple Gemaflex", "The most beautiful jewelry there be.", 1250,
					new List<ItemCategory> { ItemCategory.Gems }));
			item.Add(
				"Space Gunk", new Item("Space Gunk", "Looks like the poop emoji.", 5,
					new List<ItemCategory> { ItemCategory.Junk }));
			item.Add(
				"Earwax", new Item("Earwax", "From an alien. Species unknown.", 25,
					new List<ItemCategory> { ItemCategory.Junk }));
			item.Add(
				"Space Claritin", new Item("Space Claritin", "Cures space allergies with surprising efficacy.", 100,
					new List<ItemCategory> { ItemCategory.Medical }));
			item.Add(
				"Throxnard Pills", new Item("Throxnard Pills", "These make you feel really warm.", 150,
					new List<ItemCategory> { ItemCategory.Medical }));
			item.Add(
				"Xenoberry Extract", new Item("Xenoberry Extract", "Don't worry, it's 'natural'.", 325,
					new List<ItemCategory> { ItemCategory.Medical }));
			item.Add(
				"Android (female)", new Item("Android (female)", "Indistinguishable from a human female. Equally annoying.", 1000,
					new List<ItemCategory> { ItemCategory.Robots }));
			item.Add(
				"Android (male)", new Item("Android (male)", "Indistinguishable from a human male. Equally unintelligent.", 1000,
					new List<ItemCategory> { ItemCategory.Robots }));
			item.Add(
				"Spyder Tank", new Item("Spyder Tank", "Hexapod heavy weapon. Effective on all terrains.", 1000,
					new List<ItemCategory> { ItemCategory.Robots, ItemCategory.Weapons }));
			item.Add(
				"Nanomyte", new Item("Nanomyte", "1mm in diameter, but fully capable of killing, healing, and logical reasoning.", 1000,
					new List<ItemCategory> { ItemCategory.Medical, ItemCategory.Robots, ItemCategory.Weapons }));
			item.Add(
				"Holy Water", new Item("Holy Water", "Tastes like sactification.", 77,
					new List<ItemCategory> { ItemCategory.Alcohol, ItemCategory.Sacred }));
			item.Add(
				"Consciousness Incarnate", new Item("Consciousness Incarnate", "The physical manifestation of conscious existence.", 777,
					new List<ItemCategory> { ItemCategory.Sacred }));
			item.Add(
				"Artificial God", new Item("Artificial God", "Has technology gone too far?", 7777,
					new List<ItemCategory> { ItemCategory.Sacred, ItemCategory.Robots }));
			item.Add(
				"Windows 3000", new Item("Windows 3000", "The immediate successor to Windows 2998.", 100,
					new List<ItemCategory> { ItemCategory.Software }));
			item.Add(
				"OS XY FatPanda", new Item("OS XY FatPanda", "It's safe to say they're running out of animal names.", 250,
					new List<ItemCategory> { ItemCategory.Software }));
			item.Add(
				"Linux", new Item("Linux", "Technically still free, but a man's gotta make a living somehow.", 600,
					new List<ItemCategory> { ItemCategory.Software }));
			item.Add(
				"Raygun", new Item("Raygun", "Makes a satisfying 'pew' sound.", 400,
					new List<ItemCategory> { ItemCategory.Weapons }));
			item.Add(
				"Portable Nuke", new Item("Portable Nuke", "Completely silent if detonated in space.", 800,
					new List<ItemCategory> { ItemCategory.Weapons }));
			item.Add(
				"Tactical Lego", new Item("Tactical Lego", "Inflicts irreparable damage if an adversary steps on it.", 900,
					new List<ItemCategory> { ItemCategory.Weapons }));
			item.Add(
				"Disintegration Sauce", new Item("Disintegration Sauce", "Basically liquid death.", 1500,
					new List<ItemCategory> { ItemCategory.Alcohol, ItemCategory.Weapons }));



			//////////////////////////////////////////////////////////////////////////
			//
			// PLANETS
			//
			//////////////////////////////////////////////////////////////////////////

			CelestialBody earth = new Planet("Sol-3", "Ground zero. Home base. Mi casita.", ConsoleColor.Blue,
				new Coordinates(18, 26),
				new List<ItemCategory> { ItemCategory.Alcohol, ItemCategory.Junk, ItemCategory.Weapons },
				'E');
			earth.AddItem(item["Ectoburger"]);
			earth.AddItem(item["Xenoberry"]);
			earth.AddItem(item["Superhot Dog"]);
			earth.AddItem(item["Edible Stardust"]);
			earth.AddItem(item["Cocoa Jumbo"]);
			earth.AddItem(item["Vanilla Jumbo"]);
			earth.AddItem(item["Jumbo Jumbo"]);
			earth.AddItem(item["Recursive Jumbo"]);

			CelestialBody venus = new Planet("Venus", "Really hot, but oddly beautiful.", ConsoleColor.White,
				new Coordinates(10, 22),
				new List<ItemCategory> { ItemCategory.Crafting, ItemCategory.Medical },
				'V');
			venus.AddItem(item["Space Beer"]);
			venus.AddItem(item["Hyperseltzer"]);
			venus.AddItem(item["Dimensional Whiskey"]);
			venus.AddItem(item["Xenowine"]);
			venus.AddItem(item["Alienware"]);
			venus.AddItem(item["A.I. Chip"]);
			venus.AddItem(item["V.R. Implant"]);
			venus.AddItem(item["Superdrive"]);

			CelestialBody mars = new Planet("Mars", "Fully terraformed, and still red!", ConsoleColor.Red,
				new Coordinates(25, 18),
				new List<ItemCategory> { ItemCategory.Food, ItemCategory.Dessert },
				'M');
			mars.AddItem(item["Hydrogen"]);
			mars.AddItem(item["Xenon"]);
			mars.AddItem(item["Plutonium"]);
			mars.AddItem(item["Platinum Chunk"]);
			mars.AddItem(item["Space Claritin"]);
			mars.AddItem(item["Throxnard Pills"]);
			mars.AddItem(item["Xenoberry Extract"]);

			CelestialBody vulcan = new Planet("Vulcan", "Home to the Vulcans, surprisingly.", ConsoleColor.DarkRed,
				new Coordinates(50, 15),
				new List<ItemCategory> { ItemCategory.Computers, ItemCategory.Software },
				'v');
			vulcan.AddItem(item["Android (female)"]);
			vulcan.AddItem(item["Android (male)"]);
			vulcan.AddItem(item["Spyder Tank"]);
			vulcan.AddItem(item["Nanomyte"]);
			vulcan.AddItem(item["Raygun"]);
			vulcan.AddItem(item["Portable Nuke"]);
			vulcan.AddItem(item["Tactical Lego"]);
			vulcan.AddItem(item["Disintegration Sauce"]);

			CelestialBody tatooine = new Planet("Tatooine", "Home of the Ewoks and unjust war.", ConsoleColor.Green,
				new Coordinates(80, 18),
				new List<ItemCategory> { ItemCategory.Creatures, ItemCategory.Robots, ItemCategory.Gems },
				'T');
			tatooine.AddItem(item["Windows 3000"]);
			tatooine.AddItem(item["OS XY FatPanda"]);
			tatooine.AddItem(item["Linux"]);
			tatooine.AddItem(item["Earwax"]);
			tatooine.AddItem(item["Xenoberry"]);
			tatooine.AddItem(item["Xenoberry Extract"]);
			tatooine.AddItem(item["Xenowine"]);
			tatooine.AddItem(item["Platinum Chunk"]);

			CelestialBody proximaCentauriB = new Planet("Proxima Centauri b", "A close friend of our second nearest star.", ConsoleColor.DarkMagenta,
				new Coordinates(10, 2),
				new List<ItemCategory> { ItemCategory.Crafting, ItemCategory.Elements },
				'C');
			proximaCentauriB.AddItem(item["Holy Water"]);
			proximaCentauriB.AddItem(item["Consciousness Incarnate"]);
			proximaCentauriB.AddItem(item["Artificial God"]);
			proximaCentauriB.AddItem(item["Diamond Supercluster"]);
			proximaCentauriB.AddItem(item["Purple Gemaflex"]);
			proximaCentauriB.AddItem(item["Broccoli"]);

			CelestialBody camazotz = new Planet("Camazotz", "Home of extreme militant conformity and colorless food.", ConsoleColor.Gray,
				new Coordinates(90, 6),
				new List<ItemCategory> { ItemCategory.Robots, ItemCategory.Weapons },
				'c');
			camazotz.AddItem(item["A.I. Chip"]);
			camazotz.AddItem(item["V.R. Implant"]);
			camazotz.AddItem(item["Superdrive"]);
			camazotz.AddItem(item["Galaxy Nails"]);
			camazotz.AddItem(item["Galaxy Hammer"]);
			camazotz.AddItem(item["Solder Beam"]);
			camazotz.AddItem(item["Robotic Screwdriver"]);
			camazotz.AddItem(item["Hydrogen"]);
			camazotz.AddItem(item["Xenon"]);

			CelestialBody naboo = new Planet("Naboo", "Home of Darth Jar Jar.", ConsoleColor.DarkCyan,
				new Coordinates(34, 5),
				new List<ItemCategory> { ItemCategory.Elements, ItemCategory.Medical, ItemCategory.Weapons },
				'N');
			naboo.AddItem(item["Space Gunk"]);
			naboo.AddItem(item["Earwax"]);
			naboo.AddItem(item["Tribble"]);
			naboo.AddItem(item["Pet Droid"]);
			naboo.AddItem(item["Baby Ewok"]);
			naboo.AddItem(item["Fish"]);
			naboo.AddItem(item["Holy Water"]);
			naboo.AddItem(item["Consciousness Incarnate"]);
			naboo.AddItem(item["Artificial God"]);

			CelestialBody jakku = new Planet("Jakku", "After a scientific disaster turned Arizona into a planet... we got Jakku.", ConsoleColor.DarkYellow,
				new Coordinates(41, 7),
				new List<ItemCategory> { ItemCategory.Alcohol, ItemCategory.Elements, ItemCategory.Robots, ItemCategory.Weapons },
				'J');
			jakku.AddItem(item["Space Claritin"]);
			jakku.AddItem(item["Throxnard Pills"]);
			jakku.AddItem(item["Xenoberry Extract"]);
			jakku.AddItem(item["Windows 3000"]);
			jakku.AddItem(item["OS XY FatPanda"]);
			jakku.AddItem(item["Linux"]);
			jakku.AddItem(item["Galaxy Nails"]);
			jakku.AddItem(item["Galaxy Hammer"]);

			CelestialBody marioWorld = new Planet("Mario World", "Lots of pipes and strange creatures can be found here.", ConsoleColor.DarkGreen,
				new Coordinates(100, 23),
				new List<ItemCategory> { ItemCategory.Dessert, ItemCategory.Food, ItemCategory.Creatures },
				'm');
			marioWorld.AddItem(item["Tribble"]);
			marioWorld.AddItem(item["Pet Droid"]);
			marioWorld.AddItem(item["Baby Ewok"]);
			marioWorld.AddItem(item["Fish"]);

			CelestialBody asgard = new Planet("Asgard", "Full of scientific wonder and magic (i.e. scientific wonder).", ConsoleColor.Yellow,
				new Coordinates(95, 15),
				new List<ItemCategory> { ItemCategory.Alcohol, ItemCategory.Gems, ItemCategory.Sacred, ItemCategory.Weapons },
				'A');
			mars.AddItem(item["Galaxy Nails"]);
			mars.AddItem(item["Galaxy Hammer"]);
			mars.AddItem(item["Solder Beam"]);
			mars.AddItem(item["Robotic Screwdriver"]);
			mars.AddItem(item["Cocoa Jumbo"]);
			mars.AddItem(item["Vanilla Jumbo"]);
			mars.AddItem(item["Jumbo Jumbo"]);
			mars.AddItem(item["Recursive Jumbo"]);

			// Add the planets to the universe.
			this.U.Add(earth);
			this.U.Add(venus);
			this.U.Add(mars);
			this.U.Add(vulcan);
			this.U.Add(tatooine);
			this.U.Add(proximaCentauriB);
			this.U.Add(camazotz);
			this.U.Add(naboo);
			this.U.Add(jakku);
			this.U.Add(marioWorld);
			this.U.Add(asgard);

			//////////////////////////////////////////////////////////////////////////
			//
			// STARS
			//
			//////////////////////////////////////////////////////////////////////////

			CelestialBody sol = new Star("Sol", "Your birth star. There's no place like home.", ConsoleColor.Yellow,
				new Coordinates(26, 20),
				's');
			CelestialBody proximaCentauri = new Star("Proxima Centauri", "The closest sun to the sun. Unremarkable in every other way.", ConsoleColor.Red,
				new Coordinates(4, 8),
				'P');
			CelestialBody solaris = new Star("Solaris", "Something feels... spooky about this place.", ConsoleColor.Magenta,
				new Coordinates(19, 72),
				'L');
			CelestialBody uyScuti = new Star("UY Scuti", "The biggest star that there be.", ConsoleColor.Blue,
				new Coordinates(64, 18),
				'U');

			CelestialBody blackHole = new Star("Black Hole", $"{this.U.Character.Name} + Black Hole = Spaghetti", ConsoleColor.Black,
				new Coordinates(70, 8),
				'B');

			// Add the stars to the universe.
			this.U.Add(sol);
			this.U.Add(proximaCentauri);
			this.U.Add(solaris);
			this.U.Add(uyScuti);
			this.U.Add(blackHole);
		}

		public void RandomEvent()
		{
			if (this.Random.Next(1, 75) == 1)
			{
				int r = this.Random.Next(1, 10);

				if (r == 1)
				{
					int damage = this.Random.Next(5, 30);
					List<string> hitBy = new List<string> {
						"are hit by a flying meteor",
						"are attacked by a beautiful alien",
						"are poisoned by a rotten xenoberry",
						"trip over your own feet",
						"are slapped by a beautiful android",
						"caught in the middle of an interplanetary crossfire",
					};

					this.U.Game.UserInterface.RenderStory($"" +
						$"  You are {hitBy[this.Random.Next(hitBy.Count)]}.\n\n" +
						$"  You sustain {damage} damage!\n\n" +
						$"  That sucks.", true);

					this.U.Character.Health -= damage;
				}

				else if (r == 2)
				{
					List<ConsoleColor> color = new List<ConsoleColor>
					{
						ConsoleColor.Red,
						ConsoleColor.Green,
						ConsoleColor.Cyan,
						ConsoleColor.Blue,
						ConsoleColor.Magenta,
						ConsoleColor.Yellow,
						ConsoleColor.White,
						ConsoleColor.Green
					};

					ConsoleColor randomColor = color[this.Random.Next(color.Count)];

					this.U.Game.UserInterface.RenderStory($"" +
						$"  You fly through a mysterious cloud of {randomColor} paint.\n" +
						$"  It stuck to your spaceship!\n\n" +
						$"  I hope you like {randomColor}.", true);

					this.U.Character.Spaceship.Color = randomColor;
				}

				else if (r == 3)
				{
					this.U.Game.UserInterface.RenderStory($"" +
						$"  A mysterious solar wind turns your ship around.\n\n" +
						$"  It was completely unexpected.\n\n" +
						$"  I hope you had extra fuel!", true);

					switch (this.U.Character.Direction)
					{
						case Direction.Up:
							this.U.Character.Direction = Direction.Down;
							break;
						case Direction.Right:
							this.U.Character.Direction = Direction.Left;
							break;
						case Direction.Left:
							this.U.Character.Direction = Direction.Right;
							break;
						case Direction.Down:
							this.U.Character.Direction = Direction.Up;
							break;
					}
				}

				else if (r == 4)
				{
					this.U.Game.UserInterface.RenderStory($"" +
						$"  A mysterious force intersects your spaceship.\n\n" +
						$"  Your fuel capacity has increased!\n\n" +
						$"  Perhaps it was magic.", true);

					this.U.Character.Spaceship.FuelCapacity += 25;
					this.U.Character.Spaceship.Fuel = this.U.Character.Spaceship.FuelCapacity;
				}

				else if (r == 5)
				{
					this.U.Game.UserInterface.RenderStory($"" +
						$"  A mysterious force intersects your spaceship.\n\n" +
						$"  Your fuel capacity has decreased!\n\n" +
						$"  How unfortunate.", true);

					this.U.Character.Spaceship.FuelCapacity -= 10;
					if (this.U.Character.Spaceship.FuelCapacity < 1)
					{
						this.U.Character.Spaceship.FuelCapacity = 1;
					}

					if (this.U.Character.Spaceship.Fuel > this.U.Character.Spaceship.FuelCapacity)
					{
						this.U.Character.Spaceship.Fuel = this.U.Character.Spaceship.FuelCapacity;
					}
				}

				else if (r == 6)
				{
					if (this.U.Character.Health < 100)
					{
						this.U.Game.UserInterface.RenderStory($"" +
							$"  You feel strangely invigorated.\n\n" +
							$"  Your wounds are completely healed!", true);

						this.U.Character.Health = 100;
					}
					else if (this.U.Character.Health == 100)
					{
						this.U.Game.UserInterface.RenderStory($"" +
							$"  You are infected with the Wuhan Fluhan.\n\n" +
							$"  Quick, you must to the hospital!", true);

						this.U.Character.Health = 1;
					}
				}

				else if (r == 7)
				{
					if (this.U.Character.Spaceship.Fuel > 0)
					{
						this.U.Game.UserInterface.RenderStory($"" +
							$"  A hole appears in your fuel tank.\n\n" +
							$"  Needless to say, you don't have any left.", true);

						this.U.Character.Spaceship.Fuel = 0;
					}
				}

				else if (r == 8)
				{
					int newMoney = this.Random.Next(1, this.U.Character.Starbucks + 1);

					this.U.Game.UserInterface.RenderStory($"" +
						$"  A negative numeric overflow at the bank works in your favor.\n\n" +
						$"  You are now ¤{newMoney} Starbucks richer!", true);

					this.U.Character.Starbucks += newMoney;

				}

				else if (r == 9)
				{
					int newMoney = this.Random.Next(1, this.U.Character.Starbucks + 1) * -1;

					this.U.Game.UserInterface.RenderStory($"" +
						$"  In a drunken rampage, you enter (and lose) an online poker game.\n\n" +
						$"  You are now ¤{newMoney * -1} Starbucks poorer!", true);

					this.U.Character.Starbucks -= newMoney;
				}
			}
		}
	}
}