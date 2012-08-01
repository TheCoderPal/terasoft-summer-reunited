namespace Game.Engine
{
    public class Item : Object
    {
        private int level;
        private int effect;

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Item(int level, string name): base(name)
        {
            this.level = level;
            this.effect=10*level;
        }

        public int GetEffect()
        {
            return effect;
        }

        public string GetName()
        {
            return base.GetName();
        }

        public int GetLevel()
        {
            return level;
        }
    }
}
