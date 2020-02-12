using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
    public class Universe
    {
        public Objective[] Objectives;
        public Character Character;
        public CelestialBody CelestialBody;

        public Universe(Objective[] objectives, Character character, CelestialBody celestialBody)
        {
            this.Objectives = objectives;
            this.Character = character;
            this.CelestialBody = celestialBody;
        }
    }
}
