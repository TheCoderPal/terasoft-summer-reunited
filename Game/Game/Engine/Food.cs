

namespace Game.Engine
{
    public class Food : Item
    {

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Food(int level, string name) : base(level,name)
        {

        }

        public int GetEffect() 
        {
            return base.GetEffect();
        }

        public string GetName()
        {
            return base.GetName();
        }

        public int GetLevel()
        {
            return base.GetLevel();
        }

    }
}
