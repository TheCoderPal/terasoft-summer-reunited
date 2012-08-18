using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.Text;

namespace Game.UI
{
    public class Sprite
    {
        private Texture2D texture;
        private Rectangle area;
        private String name;
        private bool soundEffectPlayed,transparent,collided,virusHit,virusSlashed, virusKilled;
        public Texture2D[] textures;
        ContentManager content;

        public Sprite(Texture2D tex, Rectangle area, ContentManager content)
        {
            this.texture = tex;
            this.area = area;
            soundEffectPlayed = false;
            collided = false;
            this.content = content;
            textures = new Texture2D[4];
            textures[0]= content.Load<Texture2D>("Textures//Transparent");
            textures[1] = content.Load<Texture2D>("Textures//splash1");
            textures[2]= content.Load<Texture2D>("Textures//splash2");
            textures[3] = content.Load<Texture2D>("Textures//splash3");
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

        public void SetTransparent(bool t)
        {
            this.transparent = t;
        }

        public bool GetTransparent()
        {
            return this.transparent;
        }

        public void Collide(String name)
        {
            if (!collided)
            {
                if (!virusHit)
                {
                    if (Constants.isPunching && ((area.Y == 299) || (area.Y == 499)))
                    {
                        texture = textures[0];
                    }
                    else
                    {
                        switch (name)
                        {
                            case "gym": break;
                            case "level1": texture =textures[1]; break;
                            case "level2":
                                if (!virusSlashed && !virusKilled)
                                {
                                    texture = textures[3];
                                }
                                else
                                {
                                    texture = textures[1];
                                }
                                break;
                            case "level3": texture = textures[2];
                                if (virusKilled)
                                {
                                    texture = textures[1];
                                }
                                else
                                {
                                    if (virusSlashed)
                                    {
                                        texture = textures[3];
                                    }
                                }
                                break;
                            default: texture = textures[0]; break;
                        }
                    }
                }
                else
                {
                    texture = textures[0];
                }
                collided = true;
            }
        }

        public bool GetCollided()
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

        public bool IsSlashed()
        {
            return virusSlashed;
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

        public void KillVirus()
        {
            virusKilled = true;
        }

        public bool GetKilled()
        {
            return virusKilled;
        }

        public bool IsHit()
        {
            return virusHit;
        }
    }
}