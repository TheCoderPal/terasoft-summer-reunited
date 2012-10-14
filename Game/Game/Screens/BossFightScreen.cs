using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.Engine;
using Microsoft.Xna.Framework.Audio;

namespace Game.Screens
{
    class BossFightScreen : GameScreen
    {
        private SpriteBatch spriteBatch;
        private Camera2D cam;
        private bool flag;
        private SpriteFont font;
        private GraphicsDevice graphics;
        private int screenWidth,screenHeight, virusHealth,bossLevel;
        private Button button;
        private HandCursor hand;
        private ContentManager content;
        private Texture2D gradientTexture;
        private Bar immunityBar,virusBar;
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
            this.bossLevel = bossLevel;
        }

        public override void Initialize()
        {
            showAvatar = true;
            virusBar = new Bar(100, 950, 15, 270, 30);
            rightSwordBounds = new Rectangle(0, 400, 80, 80);
            leftSwordBounds = new Rectangle(1350, 400, 80, 80);
            shields = new Texture2D[4];
            shieldBounds = new Rectangle[4];
            shieldBounds[0] = new Rectangle(590, -80, 80, 80);
            shieldBounds[2] = new Rectangle(590, 720, 80, 80);
            shieldBounds[1] = new Rectangle(-90, 500, 80, 80);
            shieldBounds[3] = new Rectangle(1304, 500, 80, 80);
            cam = new Camera2D();
            cam.Position = new Vector2(500.0f, 200.0f);
            button = new Button();
            hand = new HandCursor();
            hand.Initialize(ScreenManager.Kinect);
            button.Initialize("Buttons/OK", this.ScreenManager.Kinect, new Vector2(620, 500));
            button.Clicked += new Button.ClickedEventHandler(button_Clicked);
            base.Initialize();
        }
        void button_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
            ScreenManager.AddScreen(playScreen);
            playScreen.UnfreezeScreen();
            playScreen.restoreGame();
        }
        public override void LoadContent()
        {
            content = ScreenManager.Game.Content;
            graphics = ScreenManager.GraphicsDevice;
            spriteBatch = ScreenManager.SpriteBatch;
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
            boss = new Boss(bossLevel, content.Load<Texture2D>("Textures\\Transparent"), new Rectangle(1000, 200, 50, 50));
            gradientTexture = content.Load<Texture2D>("Textures\\Gym-Interior");
            font = content.Load<SpriteFont>("SpriteFont1");
            rightSword = content.Load<Texture2D>("Textures//sword");
            bump = content.Load<SoundEffect>("Audio//bump");
            hand.LoadContent(content);
            button.LoadContent(content);
            Texture2D shieldSprite = content.Load<Texture2D>("Textures//shield");
            virusBar.LoadContent(content);
            leftSword = rightSword;
            for (int i = 0; i <= 3; i++)
                shields[i] = shieldSprite;
            levelPassed = new Sprite(content.Load<Texture2D>("Textures//Level"), new Rectangle(1280, 200, 800, 95), content);       
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            virusBar.SetCurrentValue(boss.health);
            //cam.Scale+=0.01f;
            boss.AttackBoss(1);
            if (boss.BossDied())
            {
                flag = true;
                player.RandomReward();
                boss.AttackBoss(3000);
            }
            if (flag == true)
            {
                hand.Update(gameTime);
                button.Update(gameTime);
                FreezeScreen();
            }

            if (player.HasShield())
            {
                shieldRewarded = true; swordRewarded = false;
            }
            else if (player.HasSword())
            {
                swordRewarded = true;
                shieldRewarded = false;
            }

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
//            spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.AlphaBlend,null,null,null,null,cam.Transform);
            spriteBatch.Begin();
            spriteBatch.Draw(gradientTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            virusBar.Draw(spriteBatch);
            immunityBar.Draw(spriteBatch);
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
            if (flag == true)
            {
                hand.Draw(spriteBatch);
                button.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
