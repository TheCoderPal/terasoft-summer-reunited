using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.Text;
using Game.Engine;

namespace Game.Screens
{
    class BossFightScreen : GameScreen
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private GraphicsDevice graphics;
        private int timer = 3;
        private int screenWidth;
        private int screenHeight;
        private int virusHealth;
        private ContentManager content;
        private string message;
        private Texture2D gradientTexture;
        private Bar immunityBar;
        private Bar virusBar;
        private PlayScreen playScreen;
        private Boss boss;
        private Player player;

        public BossFightScreen(PlayScreen playScreen, int bossLevel)
        {
            this.playScreen = playScreen;
            immunityBar = playScreen.bar;
            player = playScreen.player;
            boss = new Boss(bossLevel, null, Rectangle.Empty);
        }

        public override void Initialize()
        {
            virusBar = new Bar(100, 20, 15, 270, 30);
            base.Initialize();
        }
        public override void LoadContent()
        {
            content = ScreenManager.Game.Content;
            graphics = ScreenManager.GraphicsDevice;
            spriteBatch = ScreenManager.SpriteBatch;
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
            gradientTexture = content.Load<Texture2D>("Textures\\gradient");
            font = content.Load<SpriteFont>("SpriteFont1");
            virusBar.LoadContent(content);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            virusBar.SetCurrentValue(virusHealth);
            virusBar.Update(gameTime);
            timer--;
            if (timer == 0)
                message = "";

            boss.AttackBoss(400);
            if (boss.BossDied())
                player.RandomReward();

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Vector2 viewportSize = new Vector2(screenWidth, screenHeight);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;
            int hPad = Constants.hPad;
            int vPad = Constants.vPad;
            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);

            spriteBatch.Begin();
            spriteBatch.Draw(gradientTexture, backgroundRectangle, Color.White);
            immunityBar.Draw(spriteBatch);
            spriteBatch.DrawString(font, message, textPosition, Color.Orange);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
