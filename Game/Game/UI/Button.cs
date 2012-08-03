using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.Kinect;
using Microsoft.Kinect;
using System;

namespace Game.UI
{
    public class Button
    {

        private Texture2D texture;
        private Game.Kinect.Kinect kinect;
        private Vector2 position;
        private string texturePath;
        private float timer = 0;
        private const float CLICK_TIME_OUT = 1000;
        private Rectangle BoundingRectangle;

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
            hoverColor = new Color(255, 255, 255, 220);
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(texturePath);
            BoundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            Joint rightHand = kinect.GetCursorPosition();
            Point handPoint = new Point((int)rightHand.Position.X, (int)rightHand.Position.Y);
            if (BoundingRectangle.Contains(handPoint))
            {
                timer += gameTime.ElapsedGameTime.Milliseconds;

                if (timer >= CLICK_TIME_OUT)
                {
                    timer -= 100;
                    if (Clicked != null)
                        Clicked(this, null);
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
                overlayHeight = ((timer / CLICK_TIME_OUT) * BoundingRectangle.Height);
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, (int)BoundingRectangle.X, (int)overlayHeight), hoverColor);
            }
        }


    }
}
