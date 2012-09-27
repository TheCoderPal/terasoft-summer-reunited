using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.Text;

namespace Game.Screens
{
    class SettingsScreen : GameScreen
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private GraphicsDevice graphics;
        private int screenWidth;
        private int counter=0;
        private int screenHeight;
        private ContentManager content;
        private string message;
        private Vector2 avatarPosition;
        private Texture2D background;
        private Texture2D[] avatars;
        public Texture2D currentAvatar { get; set; }

        public SettingsScreen() { }
        public override void LoadContent()
        {
            content = ScreenManager.Game.Content;
            graphics = ScreenManager.GraphicsDevice;
            spriteBatch = ScreenManager.SpriteBatch;
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
            background = content.Load<Texture2D>("Textures\\gradient");
            font = content.Load<SpriteFont>("SpriteFont1");
            avatars = new Texture2D[4];
            avatars[0] = content.Load<Texture2D>("Textures\\avatar1");
            avatars[1] = content.Load<Texture2D>("Textures\\avatar2");
            avatars[2] = content.Load<Texture2D>("Textures\\avatar3");
            avatars[3] = content.Load<Texture2D>("Textures\\avatar4");
            avatarPosition = new Vector2((screenWidth + 25), (screenHeight / 3.4f));
            base.LoadContent();
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(currentAvatar, avatarPosition, null, Color.White, 0,
                    new Vector2(currentAvatar.Width, currentAvatar.Height), 1f, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Constants.isSwappingHand)
                for (int i = 0; i < avatars.Length; i++)
                {
                    if (i == 3)
                        counter = 0;
                    if (avatars[i] == currentAvatar)
                        counter = i + 1;
                }
            currentAvatar = avatars[counter];
            base.Update(gameTime);
        }

    }

}
