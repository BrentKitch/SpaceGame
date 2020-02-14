using System;

namespace SpaceGame
{
	public class Game
	{
		public Universe U
		{
			get; set;
		}

		private Menu _menu;
		public Menu Menu
		{
			get
			{
				return this.BuildMenu();
			}

			set
			{
				this._menu = value;
			}
		}

		public Game(Universe u)
		{
			this.U = u;
		}

		void Step()
		{

		}

		void Start()
		{

		}

		void CheckObjectives()
		{

		}

		// Checks for conditions which would result in various menu items, and then
		// builds and returns the corresponding menu.
		private Menu BuildMenu()
		{
			if (this._menu == null)
			{
				bool collision = false;
				Menu menu = new Menu(this.U);

				// Is the character in a planet?
				foreach (CelestialBody CelestialBody in this.U.CelestialBodies)
				{
					if (this.U.Character.InCollision(CelestialBody.Coordinates))
					{
						collision = true;

						menu.Add(
							new MenuItem("Buy",
							new Action(
								this.U,
								"ChangeMenu",
								new Menu(this.U).ShowShopBuyMenu(CelestialBody)
							),
							ConsoleKey.D1)
						);
						menu.Add(
							new MenuItem("Sell",
							new Action(
								this.U,
								"ChangeMenu",
								new Menu(this.U).ShowShopSellMenu(CelestialBody)
							),
							ConsoleKey.D2)
						);

						break;
					}
				}

				if (!collision)
				{
					menu.StartMovement();

				}

				this._menu = null;
				return menu;
			}
			else
			{
				return this._menu;
			}
		}
	}
}