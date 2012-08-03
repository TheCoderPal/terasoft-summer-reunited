using Game.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Engine
{
    public class Item : Sprite
    {
        private int level;
        private int effect;
        private string name;

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Item(Texture2D tex, Rectangle area, int level, string name) : base(tex, area)
        {
            this.name = name;
            this.level = level;
            this.effect = 10 * level;
        }

        public int GetEffect()
        {
            return effect;
        }

        public int GetLevel()
        {
            return level;
        }

        public string GetName()
        {
            return name;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
