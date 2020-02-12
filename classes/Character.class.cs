//Authors: Justin Davis, Brent Kitchen, Nathanael Shermett

using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame
{
    public class Character
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Health { get; set; }
        public int Currency { get; set; }
        public Coordinates Position { get; set; }
        public Gender Gender{ get; set; }

        public Character(string name, int age, Gender gender)
        {
            this.Name = name;
            this.Age = 18;
            this.Currency = 0;
            this.Health = 100;
            this.Gender = gender;
        }

    }
}