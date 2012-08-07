using Game.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Game.Screens
{
    class PauseScreen : GameScreen
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphics;
        int screenWidth;
        int screenHeight;
        ContentManager content;
        public override void LoadContent()
        {
            content = ScreenManager.Game.Content;
            graphics = ScreenManager.GraphicsDevice;
            spriteBatch = ScreenManager.SpriteBatch;
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (userAvatar.Avatar[0].Equals(userAvatar.AllAvatars[2]) && PlayScreen.screenPaused == true)
                this.Remove();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Remove()
        {
            base.Remove();
        }


    }
}
