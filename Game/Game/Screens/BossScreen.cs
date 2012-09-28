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
        private Texture2D rightSword;
        private Texture2D leftSword;
        private Texture2D[] shields;
        private bool displayRewards=true, swordRewarded=true, shieldRewarded=true;//change this later
        private float rightRotationAngle,leftRotationAngle;
        private Rectangle rightSwordBounds;
        private Rectangle leftSwordBounds;
        private Rectangle[] shieldBounds;
        private SoundEffect bump;


        public override void Initialize()
        {
            rightSwordBounds = new Rectangle(0, 400, 80, 80);
            leftSwordBounds = new Rectangle(1350, 400, 80, 80);
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
            rightSword = Content.Load<Texture2D>("Textures//sword");
            leftSword = rightSword;
            bump = Content.Load<SoundEffect>("Audio//bump");
            Texture2D shieldSprite = Content.Load<Texture2D>("Textures//shield");
            for (int i = 0; i <= 3; i++)
                shields[i] = shieldSprite;
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
            if (shieldBounds[0].Y<500)
                shieldBounds[0].Y+=29;

            if (shieldBounds[2].Y > 505)
                shieldBounds[2].Y -= 10;

            if (shieldBounds[1].X < 590)
                shieldBounds[1].X += 34;

            if (shieldBounds[3].X > 590)
                shieldBounds[3].X -= 34;

            if (shieldBounds[3].X == 624)
                bump.Play();
        }

        public override void Draw(GameTime gametime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            if (displayRewards)
            {
                if (swordRewarded)
                {
                    spriteBatch.Draw(rightSword, rightSwordBounds, null, Color.White, rightRotationAngle, new Vector2(rightSword.Width / 2, rightSword.Height / 2), SpriteEffects.None, 0f);
                    spriteBatch.Draw(leftSword, leftSwordBounds, null, Color.White, rightRotationAngle, new Vector2(leftSword.Width / 2, leftSword.Height / 2), SpriteEffects.None, 0f);
                }
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
