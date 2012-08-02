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
            runTexture = Content.Load<Texture2D>("sprites/run");
            jumpTexture = Content.Load<Texture2D>("sprites/jump");
        }

        public void Initialize()
        {
            runAnimation.Initialize(runTexture, Position, 0.1f, Color.White, 1f, true);
            jumpAnimation.Initialize(jumpTexture, Position, 0.1f, Color.White, 1f, false);
            playerAnimation = runAnimation; 
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

            
        }

        public void CheckDeath()
        {
            if (immunity == 0)
            {

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerAnimation.Draw(spriteBatch);
        }

        public void Collided(int value)
        {
            immunity += value;
        }
    }
}
