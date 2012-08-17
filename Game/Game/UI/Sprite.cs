using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.UI
{
    public class Sprite
    {
        private Texture2D texture;
        private Rectangle area;
        private String name;
        private Boolean soundEffectPlayed,transparent,collided,virusHit,virusSlashed, virusKilled;

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
            spriteBatch.Begin();
            spriteBatch.Draw(texture, area, null, Color.White);
            spriteBatch.End();
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

        public void Collide(ContentManager Content, String name)
        {
            if (!virusHit && GetY()==399)
            {
                switch (name)
                {
                    case "gym": break;
                    case "level1": texture = Content.Load<Texture2D>("Textures//splash1"); break;
                    case "level2":
                        if (!virusSlashed)
                        {
                            texture = Content.Load<Texture2D>("Textures//splash3");
                        }
                        else
                        {
                            texture = Content.Load<Texture2D>("Textures//splash1");
                        }
                        break;
                    case "level3": texture = Content.Load<Texture2D>("Textures//splash2");                        
                        if(virusKilled)
                        {
                            texture = Content.Load<Texture2D>("Textures//splash1");
                        }
                        break;
                    default: texture = Content.Load<Texture2D>("Textures//Transparent"); break;
                }
            }
            else
            {
                texture = Content.Load<Texture2D>("Textures//Transparent");
            }
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

        public void HitVirus()
        {
            virusHit = true;
        }

        public void SlashVirus()
        {
            virusSlashed = true;
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