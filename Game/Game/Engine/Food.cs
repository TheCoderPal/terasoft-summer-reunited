

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game.Engine
{
    public class Food : Item
    {

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Food(Texture2D tex, Rectangle area, int level, string name) : base(tex, area, level, name)
        {

        }

        public int GetEffect()
        {
            return base.GetEffect();
        }

        public int GetLevel()
        {
            return base.GetLevel();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
