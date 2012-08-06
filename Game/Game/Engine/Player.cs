using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Engine
{


    class Player
    {
        //Enumerator on player state
        enum PlayerStates
        {
            Running,
            Jumping,
            Sliding,
            Dying
        }

        //
        PlayerStates State;

        //Animation of the avatar.
        SpriteAnimation playerAnimation;
        SpriteAnimation runAnimation;
        SpriteAnimation jumpAnimation;

        //Sprite Textures
        Texture2D runTexture, jumpTexture;

        //Avatar's Position on screen.
        Vector2 Position;

        //Player health.
        private int immunity;

        //Player Score
        private int score;
        public int Score { get { return score; } set { score = value; } }

        //Player items status
        bool hasShield, hasSword;

        public Player()
        {
            Position = new Vector2();
            State = PlayerStates.Running;
            runAnimation = new SpriteAnimation();
            jumpAnimation = new SpriteAnimation();
        }

        public void LoadContent(ContentManager Content)
        {
            runTexture = Content.Load<Texture2D>("Sprites/run");
            jumpTexture = Content.Load<Texture2D>("Sprites/jump"); Position = new Vector2(210, 550 - runTexture.Height);
            runAnimation.Initialize(runTexture, Position, runTexture.Height, runTexture.Height, runTexture.Width / runTexture.Height, 0.1f, Color.White, 1f, true);
            jumpAnimation.Initialize(jumpTexture, Position, jumpTexture.Height, jumpTexture.Height, jumpTexture.Width / jumpTexture.Height, 0.1f, Color.White, 1f, false);
            playerAnimation = runAnimation;
        }

        public void Initialize()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            switch (State)
            {
                case PlayerStates.Running: playerAnimation = runAnimation; break;
                case PlayerStates.Jumping: playerAnimation = jumpAnimation; break;
                default: playerAnimation = runAnimation; break;
            }
            playerAnimation.Update(gameTime);

            //Increment Score
            score += 10;
        }

        public bool CheckDeath()
        {
            if (immunity == 0)
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerAnimation.Draw(spriteBatch);
        }

        public void Collided(int value)
        {
            immunity += value;

            if (value < 0)
                score -= 100;
            else
                score += 100;
        }

        public Color[] GetColorData()
        {
            return playerAnimation.GetColorData();
        }

        public Rectangle GetBoundingRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, playerAnimation.FrameWidth, playerAnimation.FrameHeight);
        }

        public void AcquireSword(Boolean t)
        {
            hasSword = t;
        }

        public void AcquireShield(Boolean t)
        {
            hasShield = t;
        }

        public Boolean HasSword()
        {
            return this.hasSword;
        }

        public Boolean HasShield()
        {
            return this.hasShield;
        }
    }
}
