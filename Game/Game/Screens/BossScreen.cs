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
        private bool displayRewards=true;

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;
            
            levelPassed = new Sprite(Content.Load<Texture2D>("Textures//Level"), new Rectangle(1280, 200, 800, 95), Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (displayRewards)
            {
                levelPassed.HorizontalAnimation();
            }
        }

        public override void Draw(GameTime gametime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            levelPassed.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
