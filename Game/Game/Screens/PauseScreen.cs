using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Screens
{
    /// <para>Author: Khaled Salah</para>
    class PauseScreen : GameScreen
    {
        SpriteBatch spriteBatch;
        SpriteFont font;
        GraphicsDevice graphics;
        int screenWidth;
        int screenHeight;
        ContentManager content;
        string message;
        Texture2D gradientTexture;

        public PauseScreen() { message = "No player detected, Game paused"; }
        public PauseScreen(string message) { this.message = message; }

        public override void LoadContent()
        {
            content = ScreenManager.Game.Content;
            graphics = ScreenManager.GraphicsDevice;
            spriteBatch = ScreenManager.SpriteBatch;
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
            gradientTexture = content.Load<Texture2D>("Textures\\gradient");
            font = content.Load<SpriteFont>("SpriteFont1");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (userAvatar.Avatar.Equals(userAvatar.AllAvatars[2]) && PlayScreen.screenPaused == true)
                this.Remove();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Vector2 viewportSize = new Vector2(screenWidth, screenHeight);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;
            const int hPad = 32;
            const int vPad = 16;
            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);

            spriteBatch.Begin();
            spriteBatch.Draw(gradientTexture, backgroundRectangle, Color.White);
            spriteBatch.DrawString(font, message, textPosition, Color.Orange);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Remove()
        {
            base.Remove();
        }


    }
}
