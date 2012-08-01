namespace Game.Engine
{
    public class Item
    {
        private int level;
        private int effect;
        private string name;

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Item(int level, string name)
        {
            this.level = level;
            this.name = name;
            this.effect=10*level;
        }

        public Item()
        {

        }

        public int GetEffect()
        {
            return effect;
        }

        public string GetName()
        {
            return name;
        }

        public int GetLevel()
        {
            return level;
        }
    }
}
