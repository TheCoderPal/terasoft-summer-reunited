using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.Kinect;
using Microsoft.Kinect;
using System;
using System.Diagnostics;

namespace Game.UI
{
    public class Button
    {

        private Texture2D texture;
        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }
        private string texturePath;
        private int TextureWidth, TextureHeight;

        private Game.Kinect.Kinect kinect;
        private float timer = 0;
        private const float CLICK_TIME_OUT = 2500;

        private Rectangle BoundingRectangle;
        private bool TextureBoundsSet = false;

        private bool clicked;
        private Color hoverColor;

        //Event firing
        public delegate void ClickedEventHandler(object sender, EventArgs a);
        public event ClickedEventHandler Clicked;

        public Button()
        {

        }

        public void Initialize(string Path, Game.Kinect.Kinect kinect, Vector2 Position)
        {
            texturePath = Path;
            this.kinect = kinect;
            position = Position;
            TextureBoundsSet = false;
            hoverColor = new Color(255, 255, 255, 220);
        }

        public void Initialize(string Path, Game.Kinect.Kinect kinect, Vector2 Position, int buttonWidth, int buttonHeight)
        {
            texturePath = Path;
            this.kinect = kinect;
            position = Position;
            TextureWidth = buttonWidth;
            TextureHeight = buttonHeight;
            TextureBoundsSet = true;
            hoverColor = new Color(255, 255, 255, 220);
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(texturePath);
            if (!TextureBoundsSet)
            {
                TextureWidth = texture.Width;
                TextureHeight = texture.Height;
                BoundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
            else
                BoundingRectangle = new Rectangle((int)position.X, (int)position.Y, TextureWidth, TextureHeight);
        }

        public void Update(GameTime gameTime)
        {
            Joint rightHand = kinect.GetCursorPosition();
            Point handPoint = new Point((int)rightHand.Position.X, (int)rightHand.Position.Y);
            if (BoundingRectangle.Contains(handPoint))
            {

                //timer += gameTime.ElapsedGameTime.Milliseconds;

                //if (timer >= CLICK_TIME_OUT)
                //{
                //    timer -= 100;
                //    if (Clicked != null)
                //        Clicked(this, null);
                //}

                if (timer == 0 || !clicked)
                {
                    timer += gameTime.ElapsedGameTime.Milliseconds;

                    if (timer >= CLICK_TIME_OUT)
                    {
                        timer -= 100;
                        if (Clicked != null)
                            Clicked(this, null);
                        clicked = true;
                    }
                }
                else
                {
                    if (timer != 0)
                        timer -= 200;
                    else
                        clicked = false;
                }
            }
            else
            {
                timer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingRectangle, Color.White);

            if (timer > 0 && timer < CLICK_TIME_OUT)
            {
                float overlayHeight = 0f;
                overlayHeight = ((timer / CLICK_TIME_OUT) * texture.Height);
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, (int)overlayHeight), hoverColor);
            }
        }


    }
}
