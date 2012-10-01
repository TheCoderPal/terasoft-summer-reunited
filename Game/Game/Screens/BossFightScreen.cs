using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.Text;
using Game.Engine;
using Microsoft.Xna.Framework.Audio;

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

        #region Shirin's fields
        private Sprite levelPassed;
        private Texture2D rightSword;
        private Texture2D leftSword;
        private Texture2D[] shields;
        private bool displayRewards = true, swordRewarded = true, shieldRewarded = true;//change this later
        private float rightRotationAngle, leftRotationAngle;
        private Rectangle rightSwordBounds;
        private Rectangle leftSwordBounds;
        private Rectangle[] shieldBounds;
        private SoundEffect bump;
        #endregion

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
            rightSwordBounds = new Rectangle(0, 400, 80, 80);
            leftSwordBounds = new Rectangle(1350, 400, 80, 80);
            shields = new Texture2D[4];
            shieldBounds = new Rectangle[4];
            shieldBounds[0] = new Rectangle(590, -80, 80, 80);
            shieldBounds[2] = new Rectangle(590, 720, 80, 80);
            shieldBounds[1] = new Rectangle(-90, 500, 80, 80);
            shieldBounds[3] = new Rectangle(1304, 500, 80, 80);  
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
            rightSword = content.Load<Texture2D>("Textures//sword");
            leftSword = rightSword;
            bump = content.Load<SoundEffect>("Audio//bump");
            Texture2D shieldSprite = content.Load<Texture2D>("Textures//shield");
            for (int i = 0; i <= 3; i++)
                shields[i] = shieldSprite;
            levelPassed = new Sprite(content.Load<Texture2D>("Textures//Level"), new Rectangle(1280, 200, 800, 95), content);       
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

            if (displayRewards)
            {
                levelPassed.HorizontalAnimation();
                if (swordRewarded)
                    SwordAnimation(gameTime);

                if (shieldRewarded)
                    ShieldAnimation(gameTime);
            }

            base.Update(gameTime);
        }



        private void SwordAnimation(GameTime gameTime)
        {

            if (leftSwordBounds.X > 630)
            {
                leftRotationAngle += ((float)gameTime.ElapsedGameTime.TotalSeconds + 28) % (MathHelper.Pi * 2);
                leftSwordBounds.X -= 16;
            }

            if (rightSwordBounds.X < 625)
            {
                rightRotationAngle += ((float)gameTime.ElapsedGameTime.TotalSeconds + 30) % (MathHelper.Pi * 2);
                rightSwordBounds.X += 14;
            }
        }

        private void ShieldAnimation(GameTime gameTime)
        {
            if (shieldBounds[0].Y < 500)
                shieldBounds[0].Y += 29;

            if (shieldBounds[2].Y > 505)
                shieldBounds[2].Y -= 10;

            if (shieldBounds[1].X < 590)
                shieldBounds[1].X += 34;

            if (shieldBounds[3].X > 590)
                shieldBounds[3].X -= 34;

            if (shieldBounds[3].X == 624)
                bump.Play();
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
            if (displayRewards)
            {
                if (swordRewarded)
                {
                    spriteBatch.Draw(rightSword, rightSwordBounds, null, Color.White, rightRotationAngle, new Vector2(rightSword.Width / 2, rightSword.Height / 2), SpriteEffects.None, 0f);
                    spriteBatch.Draw(leftSword, leftSwordBounds, null, Color.White, rightRotationAngle, new Vector2(leftSword.Width / 2, leftSword.Height / 2), SpriteEffects.None, 0f);
                }
                if (shieldRewarded)
                {
                    for (int i = 0; i <= shields.Length - 1; i++)
                    {
                        spriteBatch.Draw(shields[i], shieldBounds[i], Color.White);
                    }
                }

                levelPassed.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
