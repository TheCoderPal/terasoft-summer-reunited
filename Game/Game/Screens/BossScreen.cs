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
        private Texture2D[] shields;
        private bool displayRewards=true, swordRewarded=true, shieldRewarded=true;//change this later
        private float rotationAngle;
        private Rectangle swordBounds;
        private Rectangle[] shieldBounds;


        public override void Initialize()
        {
            swordBounds = new Rectangle(0, 400, 80, 80);
            shields = new Texture2D[4];
            shieldBounds = new Rectangle[4];
            shieldBounds[0] = new Rectangle(590, -80, 80, 80);
            shieldBounds[2] = new Rectangle(590, 720, 80, 80);
            shieldBounds[1] = new Rectangle(-90, 500, 80, 80);
            shieldBounds[3] = new Rectangle(1304, 500, 80, 80);            
        }

        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;
            sword = Content.Load<Texture2D>("Textures//sword");
            for(int i=0;i<=3;i++)
            shields[i] = Content.Load<Texture2D>("Textures//shield");
            levelPassed = new Sprite(Content.Load<Texture2D>("Textures//Level"), new Rectangle(1280, 200, 800, 95), Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (displayRewards)
            {
                levelPassed.HorizontalAnimation();
                if(swordRewarded)
                    SwordAnimation(gameTime);

                if (shieldRewarded)
                    ShieldAnimation(gameTime);
            }
        }

        private void SwordAnimation(GameTime gameTime)
        {
            if (swordBounds.X < 625)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                rotationAngle += elapsed + 30;
                float circle = MathHelper.Pi * 2;
                rotationAngle = rotationAngle % circle;
                swordBounds.X += 14;
            }
        }

        private void ShieldAnimation(GameTime gameTime)
        {            
            if (shieldBounds[0].Y<500)
                shieldBounds[0].Y+=29;

            if (shieldBounds[2].Y > 505)
                shieldBounds[2].Y -= 10;

            if (shieldBounds[1].X < 590)
                shieldBounds[1].X += 34;

            if (shieldBounds[3].X > 590)
                shieldBounds[3].X -= 34;            
        }

        public override void Draw(GameTime gametime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            if (displayRewards)
            {
                if (swordRewarded)
                spriteBatch.Draw(sword, swordBounds, null, Color.White, rotationAngle, new Vector2(sword.Width / 2, sword.Height / 2), SpriteEffects.None, 0f);
                
                if(shieldRewarded)
                {
                    for (int i = 0; i <= shields.Length - 1; i++)
                    {
                        spriteBatch.Draw(shields[i], shieldBounds[i], Color.White);
                    }
                }
                
                levelPassed.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

    }
}
