﻿using System;
using System.Collections.Generic;
using Game.Engine;
using Game.Text;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Game.Screens
{
    public class BossScreen : GameScreen
    {

        private Sprite levelPassed;
        private Texture2D sword;
        private bool displayRewards=true, swordRewarded=true, shieldRewarded=true;//change this later
        private float rotationAngle;
        private Rectangle swordBounds;


        public override void Initialize()
        {
            swordBounds = new Rectangle(0, 400, 80, 80);
        }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;
            sword = Content.Load<Texture2D>("Textures//sword");
            levelPassed = new Sprite(Content.Load<Texture2D>("Textures//Level"), new Rectangle(1280, 200, 800, 95), Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (displayRewards)
            {
                levelPassed.HorizontalAnimation();
                if(swordRewarded)
                SwordAnimation(gameTime);
            }
        }

        private void SwordAnimation(GameTime gameTime)
        {
            if (swordBounds.X < 615)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                rotationAngle += elapsed + 15;
                float circle = MathHelper.Pi * 2;
                rotationAngle = rotationAngle % circle;
                swordBounds.X += 10;
            }
        }

        public override void Draw(GameTime gametime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(sword, swordBounds, null, Color.White, rotationAngle, new Vector2(sword.Width / 2, sword.Height / 2), SpriteEffects.None, 0f);
            levelPassed.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
