using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class Sprite
    {
        Texture2D texture;
        Rectangle area;

        public Sprite(Texture2D tex, Rectangle area)
        {
            this.texture = tex;
            this.area = area;
        }

        public void Update(int speed)
        {
            area.X -= speed;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public int GetX()
        {
            return area.X;
        }

        public int GetY()
        {
            return area.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, area, Color.White);
            spriteBatch.End();
        }
    }
}