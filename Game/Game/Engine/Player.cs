using System;
using Game.Text;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Engine
{


    public class Player
    {
        //Enumerator on player state
        enum PlayerStates
        {
            Running,
            Punching,
            Jumping,
            Sliding,
            Dying,
            hasSword
        }

        //
        private PlayerStates State;

        //Animation of the avatar.
        private SpriteAnimation playerAnimation;
        private SpriteAnimation runAnimation;
        private SpriteAnimation jumpAnimation;
        private SpriteAnimation slidingAnimation;
        private SpriteAnimation swordAnimation;
        private SpriteAnimation dieAnimation;
        private SpriteAnimation punchAnimation;

        //Sprite Textures
        private Texture2D runTexture, jumpTexture, slideTexture, swordTexture, punchTexture, dieTexture;
        private float scale;

        //Avatar's Position on screen.
        private Vector2 Position;

        //Player health.
        private int immunity;
        public int Immunity { get { return immunity; } set { immunity = value; } }

        //Player Score
        private int score;
        public int Score { get { return score; } set { score = value; } }

        //Player items status
        private bool hasShield, hasSword;

        public Player()
        {
            Position = new Vector2();
            State = PlayerStates.Running;
            runAnimation = new SpriteAnimation();
            jumpAnimation = new SpriteAnimation();
            slidingAnimation = new SpriteAnimation();
            swordAnimation = new SpriteAnimation();
            dieAnimation = new SpriteAnimation();
            immunity = 100;
            scale = 1f;
        }

        public void LoadContent(ContentManager Content)
        {
            runTexture = Content.Load<Texture2D>("Sprites/run");
            jumpTexture = Content.Load<Texture2D>("Sprites/jump"); 
            slideTexture = Content.Load<Texture2D>("Sprites/bend");
            swordTexture = Content.Load<Texture2D>("Sprites/swap");
            dieTexture = Content.Load<Texture2D>("Sprites/die");
            punchTexture = Content.Load<Texture2D>("Sprites/punch");
            Position = new Vector2(150, 474);
            runAnimation.Initialize(runTexture, Position, runTexture.Height, runTexture.Height, runTexture.Width / runTexture.Height, 50, Color.White, scale, true);
            jumpAnimation.Initialize(jumpTexture, new Vector2(Position.X, Position.Y - 160), runTexture.Height, jumpTexture.Height, jumpTexture.Width / runTexture.Height, 80, Color.White, scale, false);
            slidingAnimation.Initialize(slideTexture, new Vector2(Position.X, Position.Y + 60), slideTexture.Height, slideTexture.Height, slideTexture.Width / slideTexture.Height, 120, Color.White, scale, false);
            swordAnimation.Initialize(swordTexture, Position, swordTexture.Height, swordTexture.Height, swordTexture.Width / swordTexture.Height, 50, Color.White, scale, false);
            dieAnimation.Initialize(dieTexture, Position, dieTexture.Height, dieTexture.Height, dieTexture.Width / dieTexture.Height, 40, Color.White, scale, false);
            punchAnimation.Initialize(punchTexture, Position, punchTexture.Height, punchTexture.Height, punchTexture.Width / punchTexture.Height, 50, Color.White, scale, false);
            playerAnimation = runAnimation;
        }

        public void Initialize()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            if (immunity <= 0)
            {
                State = PlayerStates.Dying;
            }
            else
            {
                if (Constants.isJumping)
                {
                    State = PlayerStates.Jumping;
                    if (!playerAnimation.Active)
                    {
                        Constants.isJumping = false;
                        State = PlayerStates.Running;
                        jumpAnimation.Initialize(jumpTexture, new Vector2(Position.X, Position.Y - 160), runTexture.Height, jumpTexture.Height, jumpTexture.Width / runTexture.Height, 80, Color.White, scale, false);
                    }
                }
                else
                {
                    if (Constants.isBending)
                    {
                        State = PlayerStates.Sliding;
                        if (!playerAnimation.Active)
                        {
                            Constants.isBending = false;
                            State = PlayerStates.Running;
                            slidingAnimation.Initialize(slideTexture, new Vector2(Position.X, Position.Y + 60), slideTexture.Height, slideTexture.Height, slideTexture.Width / slideTexture.Height, 120, Color.White, scale, false);
                        }
                    }
                    else
                    {
                        if (Constants.isSwappingHand && hasSword)
                        {
                            State = PlayerStates.hasSword;
                            if (!playerAnimation.Active)
                            {
                                Constants.isSwappingHand = false;
                                State = PlayerStates.Running;
                                swordAnimation.Initialize(swordTexture, Position, swordTexture.Height, swordTexture.Height, swordTexture.Width / swordTexture.Height, 50, Color.White, scale, false);
                            }
                        }
                        else
                        {
                            if (Constants.isPunching)
                            {
                                State = PlayerStates.Punching;
                                if (!playerAnimation.Active)
                                {
                                    Constants.isPunching = false;
                                    State = PlayerStates.Running;
                                    punchAnimation.Initialize(punchTexture, Position, punchTexture.Height, punchTexture.Height, punchTexture.Width / punchTexture.Height, 50, Color.White, scale, false);
                                }
                            }
                        } 
                    }
                }
            }
                

            switch (State)
            {
                case PlayerStates.Running: playerAnimation = runAnimation; break;
                case PlayerStates.Jumping: playerAnimation = jumpAnimation; break;
                case PlayerStates.Sliding: playerAnimation = slidingAnimation; break;
                case PlayerStates.hasSword: playerAnimation = swordAnimation; break;
                case PlayerStates.Dying: playerAnimation = dieAnimation; break;
                case PlayerStates.Punching: playerAnimation = punchAnimation; break;
                default: playerAnimation = runAnimation; break;
            }
            playerAnimation.Update(gameTime);

            //Increment Score
            score++;
        }

        public bool CheckDeath()
        {
            return immunity <= 0 && State == PlayerStates.Dying && !playerAnimation.Active;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerAnimation.Draw(spriteBatch);
        }

        public void Collided(int value)
        {
            
            immunity += value;
            if (immunity > 100)
                immunity = 100;

            score += value * 100; 
        }

        public Color[] GetColorData()
        {
            return playerAnimation.GetColorData();
        }

        public Rectangle GetBoundingRectangle()
        {
            return new Rectangle(((int)playerAnimation.Position.X - playerAnimation.FrameHeight/2), ((int)playerAnimation.Position.Y-playerAnimation.FrameHeight/2), 
                playerAnimation.FrameWidth, playerAnimation.FrameHeight);
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
