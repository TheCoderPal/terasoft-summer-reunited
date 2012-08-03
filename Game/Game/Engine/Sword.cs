using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Engine
{
    class Sword : Collectible
    {
        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Sword(Texture2D tex, Rectangle area, bool acquired) : base(tex, area, acquired)
        {

        }

        public void AcquireSword()
        {
            base.AcquireObject();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
