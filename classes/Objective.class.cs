using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceGame.classes
{
    class Objective
    {
        string Mission { get; set; }
        bool Optional { get; set; }
        bool Story { get; set; }
        bool Completed { get; set; }

        public Objective(string mission, bool optional, bool story)
        { 
            this.Mission = mission;
            this.Optional = optional;
            this.Story = story;
            this.Completed = false;
        }
    }
}
