using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Kinect;
using Game.Kinect;

namespace Game.UI
{
    class HandCursor
    {
        private Texture2D texture;
        private Kinect.Kinect kinect;
        private Vector2 position;

        public HandCursor()
        {

        }

        public void Initialize(Kinect.Kinect kinect)
        {
            this.kinect = kinect;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Textures/HandImage");
        }

        public void Update(GameTime gameTime)
        {
            Joint RightHand = kinect.GetCursorPosition();
            position = new Vector2(RightHand.Position.X, RightHand.Position.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
