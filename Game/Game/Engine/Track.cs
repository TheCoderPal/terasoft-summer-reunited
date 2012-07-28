

namespace Game.Engine
{
    public class Track
    {

        public static readonly int trackWidth = 10;

        public Item[,] Items { get; private set; }

        public int PlayerIndex { get; private set; }

        public Track()
        {
 
        }

        private void Generate()
        {
            Shift();
            //generate items
        }

        private void Shift()
        {

        }

        public void Move()
        {
            CheckCollision();
            //update position
        }

        private void CheckCollision()
        {

        }

    }
}
