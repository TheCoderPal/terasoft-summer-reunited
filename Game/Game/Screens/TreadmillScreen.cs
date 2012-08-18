using Game.Text;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Screens
{
    public class TreadmillScreen : GameScreen
    {
        private SpriteAnimation treadmillAnimation;
        private Texture2D treadmillSprite, avatar, bubbleBox, background, treadmill;
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        private int counter = 0;
        private PlayScreen playScreen;
        private Bar bar;
        private SpriteFont font;

        public TreadmillScreen(PlayScreen playScreen)
        {
            this.playScreen = playScreen;
            bar = playScreen.bar;
        }

        public override void Initialize()
        {
            Content = ScreenManager.Game.Content;
            spriteBatch = ScreenManager.SpriteBatch;
            treadmillAnimation = new SpriteAnimation();
            Constants.ResetDumbbellsAndRun();
            enablePause = true;
            base.Initialize();
        }

        public override void LoadContent()
        {
            treadmillSprite = Content.Load<Texture2D>("Sprites/Run");
            background = Content.Load<Texture2D>("Textures//Gym-Interior");
            treadmill = Content.Load<Texture2D>("Textures//Treadmill-Side");
            avatar= Content.Load<Texture2D>("Textures/avatar");
            bubbleBox = Content.Load<Texture2D>("Textures/RunBubble");
            font = Content.Load<SpriteFont>("Fontopo");  
            treadmillAnimation.Initialize(treadmillSprite, new Vector2(600, 500), treadmillSprite.Height, treadmillSprite.Height, treadmillSprite.Width / treadmillSprite.Height, 50, Color.White, 1f, true);
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            treadmillAnimation.Update(gameTime);
            if (Constants.isRunning)
            {
                playScreen.GetPlayer().Collided(Constants.runningEffect);
                Constants.ResetFlags();
            }
            if (counter == 600)
            {
               this.Remove();
               playScreen.Player.ReInitializeRunAnimation();
               playScreen.UnfreezeScreen();
            }
            counter++;
            bar.Update(gameTime);
            playScreen.bar.Update(gameTime);
            playScreen.bar.SetCurrentValue(playScreen.Player.Immunity);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sprite = spriteBatch; 
            sprite.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(treadmill, new Rectangle(520, 420, 250, 180), Color.White);
            spriteBatch.DrawString(font, "Meters: " +Constants.numberOfRuns, new Vector2(400, 10), Color.Red);treadmillAnimation.Draw(spriteBatch);
            #region Tamer Avatar +bubble box draw
            spriteBatch.Draw(avatar, new Rectangle(10, 400, avatar.Width*2, avatar.Height*2),Color.White);
            spriteBatch.Draw(bubbleBox, new Rectangle(avatar.Width, 380,bubbleBox.Width,bubbleBox.Height*2),Color.White);
            #endregion
            playScreen.bar.Draw(spriteBatch);
            sprite.End();
            base.Draw(gameTime);
        }
    }
}
