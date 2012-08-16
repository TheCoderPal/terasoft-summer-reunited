using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game.UI
{
    public class Sprite
    {
        Texture2D texture;
        Rectangle area;
        String name;
        Boolean soundEffectPlayed;
        Boolean transparent;
        Boolean collided;       

        public Sprite(Texture2D tex, Rectangle area)
        {
            this.texture = tex;
            this.area = area;
            soundEffectPlayed = false;
            collided = false;
        }

        public void Update(int speed)
        {
            area.X -= speed;
        }

        public int GetX()
        {
            return area.X;
        }

        public int GetY()
        {
            return area.Y;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!collided)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, area, null, Color.White);
                spriteBatch.End();
            }
        }

        public void EnterName(String name)
        {
            this.name = name;
        }

        public String GetName()
        {
            return this.name;
        }

        public void SetTransparent(Boolean t)
        {
            this.transparent = t;
        }

        public Boolean GetTransparent()
        {
            return this.transparent;
        }

        public void Collide()
        {
            collided = true;
        }

        public Boolean GetCollided()
        {
            return this.collided;
        }

        public Rectangle GetBounds()
        {
            return area;
        }

        public Color[] GetColorData()
        {
            Color[] temp = new Color[texture.Width * texture.Height];
            texture.GetData(temp);
            return temp;
        }

        public void PlaySoundEffect(SoundEffect effect)
        {
            if (!soundEffectPlayed)
            {
                effect.Play();
            }
            soundEffectPlayed = true;
        }

        public int GetWidth()
        {
            return area.Width;
        }

        public int GetHeight()
        {
            return area.Height;
        }
    }
}