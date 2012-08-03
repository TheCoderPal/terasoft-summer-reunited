using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.UI;

namespace Game.Engine
{
    class Collectible : Sprite
    {
        private bool acquired;

        public Collectible(Texture2D tex, Rectangle area, bool acquired)
            : base(tex, area)
        {
            this.acquired = acquired;
        }

        public bool GetAcquired()
        {
            return acquired;
        }

        public void AcquireObject()
        {
            this.acquired = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
