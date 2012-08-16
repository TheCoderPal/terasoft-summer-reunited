using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
            texture = Content.Load<Texture2D>("Textures/cursor2");
        }

        public void Update(GameTime gameTime)
        {
            Joint RightHand = kinect.GetCursorPosition();
            position = new Vector2(RightHand.Position.X, RightHand.Position.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), null, Color.White, 
                0, new Vector2(texture.Width/2, texture.Height/2), SpriteEffects.None, 0);
        }
    }
}
