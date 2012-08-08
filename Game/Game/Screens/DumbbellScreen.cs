using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using Game.Text;

namespace Game.Screens
{
    public class DumbbellScreen : GameScreen
    {
        Sprite background;
        SpriteAnimation dumbbellAnimation;
        Texture2D dumbbellSprite;
        SpriteBatch spriteBatch;
        ContentManager Content;
        int counter = 0;
        PlayScreen playScreen;
        int lifts = 0;

        public DumbbellScreen(PlayScreen playScreen)
        {
            this.playScreen = playScreen;
        }

        public override void Initialize()
        {
            Content = ScreenManager.Game.Content;
            spriteBatch = ScreenManager.SpriteBatch;
            dumbbellSprite = Content.Load<Texture2D>("Sprites/dumbbell-sprite");
            dumbbellAnimation = new SpriteAnimation();

            dumbbellAnimation.Initialize(dumbbellSprite, new Vector2(600, 500), 200, 262, 12, 100, Color.White, 1f, true);    
            base.Initialize();
        }

        public override void LoadContent()
        {
            background = new Sprite(Content.Load<Texture2D>("Textures//Gym-Interior"), new Rectangle(0, 0, 1280, 720));
            base.LoadContent();
        }

        
        public override void Update(GameTime gameTime)
        {
            dumbbellAnimation.Update(gameTime);
            if (counter % 50 == 0)
            {
                if (Constants.isDumbbell)
                {
                    lifts++;
                    Constants.ResetFlags();
                }
            }

            if (counter == 600)
            {
                this.Remove();
                for (int i = 0; i <= lifts-1; i++)
                {
                    playScreen.GetPlayer().Collided(2);
                }
                playScreen.UnfreezeScreen();
            }

            counter++;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            background.Draw(spriteBatch);
            SpriteBatch sprite = spriteBatch;
            sprite.Begin();
            dumbbellAnimation.Draw(spriteBatch);
            sprite.End();
            base.Draw(gameTime);
        }
    }
}
