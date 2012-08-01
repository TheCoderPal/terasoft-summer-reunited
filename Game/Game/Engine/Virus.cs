

namespace Game.Engine
{
    public class Virus : Item
    {

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Virus(int level, string name) : base(level,name)
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
