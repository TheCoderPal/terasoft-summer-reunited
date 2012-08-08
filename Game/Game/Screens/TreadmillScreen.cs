using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Game.Text;

namespace Game.Screens
{
    public class TreadmillScreen : GameScreen
    {
        Sprite background;
        Sprite treadmill;
        SpriteAnimation treadmillAnimation;
        Texture2D treadmillSprite;
        SpriteBatch spriteBatch;
        ContentManager Content;
        int counter = 0;
        int running = 0;
        PlayScreen playScreen;
        Bar bar;

        public TreadmillScreen(PlayScreen playScreen)
        {
            this.playScreen = playScreen;
            bar = playScreen.bar;
        }

        public override void Initialize()
        {
            Content = ScreenManager.Game.Content;
            spriteBatch = ScreenManager.SpriteBatch;
            treadmillSprite = Content.Load<Texture2D>("Sprites/Run");
            treadmillAnimation = new SpriteAnimation();
            treadmillAnimation.Initialize(treadmillSprite, new Vector2(600, 500), treadmillSprite.Height, treadmillSprite.Height, treadmillSprite.Width / treadmillSprite.Height, 50, Color.White, 1f, true);
            base.Initialize();
        }

        public override void LoadContent()
        {
            background = new Sprite(Content.Load<Texture2D>("Textures//Gym-Interior"), new Rectangle(0, 0, 1280, 720));
            treadmill = new Sprite(Content.Load<Texture2D>("Textures//Treadmill-Side"), new Rectangle(520, 420, 250, 180));
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            treadmillAnimation.Update(gameTime);
            if (counter % 50 == 0)
            {
                if (Constants.isRunning)
                {
                    running++;
                    Constants.ResetFlags();
                }
            }

            if (counter == 600)
            {
                this.Remove();
                for (int i = 0; i <= running - 1; i++)
                {
                    playScreen.GetPlayer().Collided(10);
                }
                playScreen.UnfreezeScreen();
            }

            counter++;
         //   bar.Update(gameTime);
            playScreen.bar.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            background.Draw(spriteBatch);
            treadmill.Draw(spriteBatch);
            SpriteBatch sprite = spriteBatch;
            sprite.Begin();
            treadmillAnimation.Draw(spriteBatch);
            sprite.End();
            playScreen.bar.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
