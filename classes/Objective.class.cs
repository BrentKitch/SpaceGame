namespace SpaceGame
{
    public class Objective
    {
        public string Mission { get; set; }
        public bool Optional { get; set; }
        public bool Story { get; set; }
        public bool Completed { get; set; }

        public Objective(string mission, bool optional, bool story)
        { 
            this.Mission = mission;
            this.Optional = optional;
            this.Story = story;
            this.Completed = false;
        }
    }
}
