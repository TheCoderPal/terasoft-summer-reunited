using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Engine
{
    class Gym : Sprite
    {
        public Gym(Texture2D tex, Rectangle area) : base(tex,area)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
