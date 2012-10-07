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
using Microsoft.Xna.Framework.Input;

namespace Game.Screens
{
    public class PlayScreen : GameScreen
    {

        //Array of Background songs.
        private Song[] songs;

        //Index of current song.
        private int playQueue;

        //Alert texture and sound effects.
        private SoundEffect immunityAudio;
        private Texture2D alertTexture;
        private int alertTimer, updateImmunityCounter;
        private bool displayAlert;

        private List<byte[]> colorDataList;

        //Instance of player.
        public Player player { get; set; }

        //Background variables
        private ParallaxingBackground bgLayer1, bgLayer2, bgLayer3;

        //Immunity bar.
        public Bar bar;

        //Player score.
        Score score;

        private ItemsGenerator generator;
        private string[,] current;
        private int spriteCounter = 0, globalCounter = 0, time=0, swordTimer=0, spriteSpeed = 4;
        private int screenWidth, screenHeight, jumpTimer;
        private bool jumping,swordUsed;
        private List<Sprite> currentSprite;
        private Sprite sword,shield, swordAcquired, shieldAcquired;
        private ContentManager Content;
        private SoundEffect[] soundEffects;
        private SpriteFont textFont;
        private Color[] playerData;
        private Rectangle playerBounds;
        private List<Texture2D> items;
        private Texture2D splashDead, splashSemiDead, splashInjured,correct,swordTexture,shieldTexture,trans;

        public override void Initialize()
        {
            screenWidth = ScreenManager.GraphicsDevice.Viewport.Width;
            screenHeight = ScreenManager.GraphicsDevice.Viewport.Height;

            colorDataList = new List<byte[]>();
            player = new Player();
            generator = new ItemsGenerator();
            current = generator.GenerateMore();
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();
            bgLayer3 = new ParallaxingBackground();
            bar = new Bar(100, 20, 15, 270, 30);
            score = new Score(870, 10, Color.Peru);
            currentSprite = new List<Sprite>();
            items = new List<Texture2D>();
            songs = new Song[2];
            soundEffects = new SoundEffect[10];

            updateImmunityCounter = 0;     
            alertTimer = 0;
            playQueue = 1;
            displayAlert = false;
            enablePause = true;


 
            Constants.ResetFlags();
            player.Initialize();
            base.Initialize();
        }

        /// <remarks>
        ///<para>AUTHOR: Khaled Salah, Ahmed Shirin </para>
        ///</remarks>
        public override void LoadContent()
        {
            ContentManager Content = ScreenManager.Game.Content;

            songs[0] = Content.Load<Song>("Audio\\song");
            songs[1] = Content.Load<Song>("Audio\\song2");
            //songs[2] = Content.Load<Song>("Directory\\songtitle");
            MediaPlayer.IsRepeating = false;

            bar.LoadContent(Content);
            score.LoadContent(Content);
            player.LoadContent(Content);

            #region Tamer 
            immunityAudio = Content.Load<SoundEffect>("Audio\\Immunity2");
            textFont = Content.Load<SpriteFont>("newFont2");
            splashDead = Content.Load<Texture2D>("Textures\\splash1");
            splashSemiDead = Content.Load<Texture2D>("Textures\\splash3");
            splashInjured = Content.Load<Texture2D>("Textures\\splash2");
            #endregion


            #region Shirin
            correct = Content.Load<Texture2D>("Textures//correct");
            swordTexture = Content.Load<Texture2D>("Textures//sword");
            shieldTexture = Content.Load<Texture2D>("Textures//shield");
            trans = Content.Load<Texture2D>("Textures//Transparent");
            sword = new Sprite(swordTexture, new Rectangle(335, 7, 50, 50),Content);
            shield = new Sprite(shieldTexture, new Rectangle(420, 0, 60, 60),Content);
            swordAcquired = new Sprite(correct, new Rectangle(325, 10, 60, 60), Content);
            shieldAcquired = new Sprite(correct, new Rectangle(410, 10, 60, 60),Content);

            soundEffects[0] = Content.Load<SoundEffect>("Audio//Death");
            soundEffects[1] = Content.Load<SoundEffect>("Audio//Food");
            soundEffects[2] = Content.Load<SoundEffect>("Audio//ShieldAcquired");
            soundEffects[3] = Content.Load<SoundEffect>("Audio//SwordAcquired");
            soundEffects[4] = Content.Load<SoundEffect>("Audio//SwordSlash");
            soundEffects[5] = Content.Load<SoundEffect>("Audio//VirusHit");
            soundEffects[6] = Content.Load<SoundEffect>("Audio//VirusSlashed");
            soundEffects[7] = Content.Load<SoundEffect>("Audio//VirusPunched");
            soundEffects[8] = Content.Load<SoundEffect>("Audio//VirusKilled");
            soundEffects[9] = Content.Load<SoundEffect>("Audio//jump");

            items.Add(Content.Load<Texture2D>("Textures//banana"));
            items.Add(Content.Load<Texture2D>("Textures//brocolli"));
            items.Add(Content.Load<Texture2D>("Textures//carrot"));
            items.Add(Content.Load<Texture2D>("Textures//pineapple"));
            items.Add(Content.Load<Texture2D>("Textures//tomato"));
            items.Add(Content.Load<Texture2D>("Textures//strawberry"));
            items.Add(Content.Load<Texture2D>("Textures//hotdog"));
            items.Add(Content.Load<Texture2D>("Textures//pizza"));
            items.Add(Content.Load<Texture2D>("Textures//fries"));
            items.Add(Content.Load<Texture2D>("Textures//donut"));
            items.Add(Content.Load<Texture2D>("Textures//cupcake"));
            items.Add(Content.Load<Texture2D>("Textures//burger"));
            items.Add(Content.Load<Texture2D>("Textures//virus1"));
            items.Add(Content.Load<Texture2D>("Textures//virus2"));
            items.Add(Content.Load<Texture2D>("Textures//virus3"));
            items.Add(shieldTexture);
            items.Add(swordTexture);
            items.Add(Content.Load<Texture2D>("Textures//gym"));
            items.Add(trans);
            items.Add(Content.Load<Texture2D>("Textures//boss"));

            #endregion

            bgLayer1.Initialize(Content, "Background/Layer 1", ScreenManager.GraphicsDevice.Viewport.Width, -1);
            bgLayer2.Initialize(Content, "Background/Layer 2", ScreenManager.GraphicsDevice.Viewport.Width, -2);
            bgLayer3.Initialize(Content, "Background/Layer 3", ScreenManager.GraphicsDevice.Viewport.Width, -4);

            alertTexture = Content.Load<Texture2D>("Textures/alert");

            playerBounds = player.GetBoundingRectangle();
            playerData = player.GetColorData();

            base.LoadContent();

        }

        /// <remarks>
        ///<para>AUTHOR: Khaled Salah, Ahmed Shirin </para>
        ///</remarks>
        public override void Update(GameTime gameTime)
        {
            Content = ScreenManager.Game.Content;
                        
            #region Omar Abdulaal

            bgLayer1.Update();
            bgLayer2.Update();
            bgLayer3.Update();

            player.Update(gameTime);

            if (jumping)
                GetActualBounds(player.GetBoundingRectangle(), player.GetColorData(), out playerBounds, out playerData);
            else
            {
                playerBounds = player.GetBoundingRectangle();
                playerData = player.GetColorData();
            }

            if (player.CheckDeath())
            {
                if (colorDataList.Count == 0)
                {
                    this.Remove();
                    ScreenManager.AddScreen(new LosingScreen(player.Score));
                }
                else
                {
                    this.Remove();
                    ScreenManager.AddScreen(new PhotographsScreen(colorDataList, player.Score));
                }
            }


            if (Constants.isJumping && colorDataList.Count == 0)
            {
                byte[] colorData = ScreenManager.Kinect.GetColorPixels(30);
                if(colorData != null)
                    colorDataList.Add(colorData);
            }

            if (Constants.isRunning && colorDataList.Count == 1)
            {
                byte[] colorData = ScreenManager.Kinect.GetColorPixels(30);
                if (colorData != null)
                    colorDataList.Add(colorData);
            }

            if (player.Immunity < 30)
            {
                alertTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (alertTimer >= 750)
                {
                    displayAlert = !displayAlert;
                    alertTimer = 0;
                }
            }
            else
                displayAlert = false;

            #endregion

            #region khaled's pausescreen and music
           
            if (MediaPlayer.State.Equals(MediaState.Stopped))
            {
                switch (playQueue)
                {
                    case 1:
                        {
                            MediaPlayer.Play(songs[0]);
                            playQueue = 2;
                            break;
                        }
                    case 2:
                        {
                            MediaPlayer.Play(songs[1]);
                            playQueue = 3;
                            break;
                        }
                    //case 3:
                    //    {
                    //        MediaPlayer.Play(songs[2]);
                    //        playQueue = 1;
                    //        break;
                    //    }
                    default: break;
                }
            }
            #endregion

            updateImmunityCounter++;
            if (player.Immunity < 30 && updateImmunityCounter > 0)
            {
                immunityAudio.Play();
                updateImmunityCounter = -300;
            }

            KeyboardState state = Keyboard.GetState();
            if(state.IsKeyDown(Keys.Space))
                Constants.isJumping=true;
            if (state.IsKeyDown(Keys.LeftControl))
                Constants.isBending = true;
            if (state.IsKeyDown(Keys.Enter))
                Constants.isPunching = true;
            if (state.IsKeyDown(Keys.D))
                Constants.isSwappingHand = true;

            if (Constants.isJumping)
                jumpTimer++;        
            if (jumpTimer==1)
                soundEffects[9].Play();
            if (!player.CheckJump())
                jumpTimer = 0;

            if (player.CheckJump()) 
                jumping = true;

            if (jumping)
            {
                time += gameTime.ElapsedGameTime.Milliseconds;
                if (time <= player.GetJumpTime()) jumping = true;
                else { jumping = false; time = 0; }
            }

            if (player.CheckSword())
            {
                swordTimer += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (globalCounter % 600 == 0)
            {
                NewItems();
            }

            globalCounter++;

            if (globalCounter % 60 == 0 && globalCounter >600)
                 spriteCounter++;

            for (int i = 0; i <= spriteCounter - 1; i++)
            {
                currentSprite[i].Update(spriteSpeed);
                HandleCollision(currentSprite[i]);
            }

            for (int i = 0; i <= spriteCounter - 1; i++)
            {
                try
                {
                    Sprite sprite = currentSprite[i];
                    if (sprite.GetName().Contains("boss") && sprite.GetX() == 800)
                    {
                        spriteSpeed = 0;
                        bgLayer1.PauseBackground();
                        bgLayer2.PauseBackground();
                        bgLayer3.PauseBackground();
                        player.MovePlayer();
                    }
                }
                catch (Exception e)
                {
                    spriteCounter--;
                }                
            }
            if (!player.CheckSword())
            {
                swordTimer = 0;
            }
            if (swordUsed)
            {
                player.AcquireSword(false);
                if (swordTimer >= player.GetSwordTime())
                {
                    swordUsed = false;
                    player.ReInitializeRunAnimation();
                    swordTimer = 0;
                }
            }
            RemoveSprites();
            
            bar.SetCurrentValue(player.Immunity);
            score.score = player.Score;
            base.Update(gameTime);
        }

        public Player GetPlayer()
        {
            return this.player;
        }

        private bool IsAbove(Rectangle playerBounds, Rectangle itemBounds)
        {
            return (playerBounds.Y + playerBounds.Height - 30) <= itemBounds.Y + itemBounds.Height;
        }

        /// <summary>
        /// Adds the newly generated items to the list of items to be displayed.
        /// </summary>
        private void NewItems()
        {
            current = generator.GenerateMore();

            Texture2D texture = items[18];

            for (int i = 0; i <= 9; i++)
            {
                int height = 0, length = 50;
                bool transparent = false;

                switch (current[i, 1])
                {
                    case "0": height = 499; break;
                    case "1": height = 399; break;
                    case "2": height = 299; break;
                }

                switch (current[i, 0])
                {
                    case "tomato": texture = items[0]; break;
                    case "carrot": texture = items[1]; break;
                    case "strawberry": texture = items[2]; break;
                    case "orange": texture = items[3]; break;
                    case "pineapple": texture = items[4]; break;
                    case "broccoli": texture = items[5]; break;
                    case "fries": texture = items[6]; break;
                    case "hamburger": texture = items[7]; break;
                    case "pizza": texture = items[8]; break;
                    case "donut": texture = items[9]; break;
                    case "muffin": texture = items[10]; break;
                    case "hotdog": texture = items[11]; break;
                    case "level1": texture = items[12]; break;
                    case "level2": texture = items[13]; break;
                    case "level3": texture = items[14]; break;
                    case "sheild": texture = items[15]; break;
                    case "sword": texture = items[16]; break;
                    case "gym": texture = items[17]; length = 200; height = 349; break;
                    case "Empty": transparent = true; break;
                    default: break;
                }

                if (current[i, 0].Contains("boss"))
                {
                    texture = items[19];
                    length = 200;
                    height = 349;
                }

                if (!transparent)
                {
                    Sprite sprite = new Sprite(texture, new Rectangle(1280, height, length, length), Content);
                    sprite.EnterName(current[i, 0]);
                    currentSprite.Add(sprite);
                }
            }
        }

        /// <summary>
        /// Applies the effect of collision on sprite and player.
        /// </summary>
        /// <param name="name">Sprite name.</param>
        /// <param name="sprite">Instance of the sprite.</param>
        private void ApplyEffect(String name, Sprite sprite)
        {
            if (!sprite.GetCollided())
            {
                switch (name)
                {
                    case "tomato": player.Collided(Constants.healthy1); break;
                    case "carrot": player.Collided(Constants.healthy2); break;
                    case "strawberry": player.Collided(Constants.healthy3); break;
                    case "orange": player.Collided(Constants.healthy4); break;
                    case "pineapple": player.Collided(Constants.healthy5); break;
                    case "broccoli": player.Collided(Constants.healthy6); break;
                    case "fries": player.Collided(Constants.unhealthy1); break;
                    case "hamburger": player.Collided(Constants.unhealthy2); break;
                    case "pizza": player.Collided(Constants.unhealthy3); break;
                    case "donut": player.Collided(Constants.unhealthy4); break;
                    case "muffin": player.Collided(Constants.unhealthy5); break;
                    case "hotdog": player.Collided(Constants.unhealthy6); break;
                    case "level1":
                        if (!sprite.GetKilled())
                        {
                            if (!player.HasShield())
                            {
                                if (!player.HasSword() || !Constants.isSwappingHand)
                                {
                                    if (!Constants.isPunching)
                                        player.Collided(Constants.level1);
                                    else
                                    {
                                        if (sprite.GetY() != 399)
                                            player.Collided(Constants.level1);
                                    }
                                }
                                else
                                {
                                    if (sprite.GetY() != 399)
                                        player.Collided(Constants.level1);
                                }
                            }
                            else
                                player.AcquireShield(false);

                        }
                        break;
                    case "level2":
                        if (!sprite.GetKilled())
                        {
                            if (!player.HasShield())
                            {
                                if (!player.HasSword() || !Constants.isSwappingHand)
                                {
                                    if (!Constants.isPunching)
                                        player.Collided(Constants.level2);
                                    else
                                    {
                                        if (sprite.GetY() == 399)
                                            player.Collided((int)(Constants.level2 / 2));
                                        else
                                            player.Collided(Constants.level2);
                                    }
                                }
                                else
                                {
                                    if (sprite.GetY() != 399)
                                        player.Collided(Constants.level2);
                                }
                            }
                            else
                                player.AcquireShield(false);
                        }
                        break;
                    case "level3":
                        if (!sprite.GetKilled())
                        {
                            if (!player.HasShield())
                            {
                                if (!player.HasSword() || !Constants.isSwappingHand)
                                {
                                    if (!Constants.isPunching)
                                        player.Collided(Constants.level3);
                                    else
                                    {
                                        if (sprite.GetY() == 399)
                                            player.Collided((int)(Constants.level3 / 4));
                                        else
                                            player.Collided(Constants.level3);
                                    }
                                }
                                else
                                {
                                    if (sprite.GetY() == 399)
                                        player.Collided((int)(Constants.level3 / 2));
                                    else
                                        player.Collided(Constants.level3);
                                }
                            }
                            else
                                player.AcquireShield(false);
                        }
                        break;
                    case "sheild": player.AcquireShield(true); break;
                    case "sword": player.AcquireSword(true); break;
                }
            }
        }

        /// <summary>
        /// Collision handling for each sprite on the screen.
        /// </summary>
        /// <param name="sprite">Sprite to check collision with.</param>
        private void HandleCollision(Sprite sprite)
        {
            Rectangle itemBounds = new Rectangle(sprite.GetX(), sprite.GetY(), sprite.GetWidth(), sprite.GetHeight());
            String name = sprite.GetName();
            if (IntersectPixels(playerBounds, playerData, itemBounds, sprite.GetColorData()))
            {
                if (!sprite.GetTransparent())
                {
                    if (name.Equals("level1") || name.Equals("level2") || name.Equals("level3"))
                    {
                        if (IsAbove(playerBounds, itemBounds) && ((player.CheckJump() && sprite.GetY() == 399) || (jumping && sprite.GetY() == 499)))
                            sprite.KillVirus();
                        else
                        {
                            if (Constants.isSwappingHand && player.HasSword() && sprite.GetY() == 399)
                            {
                                swordUsed=true;
                                sprite.SlashVirus();
                            }
                            else
                            {
                                if (!Constants.isPunching)
                                    sprite.HitVirus();
                            }
                        }

                    }

                    ApplyEffect(name, sprite);
                    PlaySoundEffects(name, sprite);

                    if (!name.Equals("gym") && !name.Contains("boss"))
                        sprite.Collide(name);
                    else
                    {
                        if (Constants.isSteppingRight)
                        {
                            screenPaused = true;
                            ScreenManager.AddScreen(new ExcercisesScreen(this));
                            this.FreezeScreen();
                        }
                    }

                    if (name.Contains("boss"))
                    {
                        if (playerBounds.Right >= sprite.GetX() + 150)
                        {
                            //Add Boss Screen
                            screenPaused = true;
                            this.FreezeScreen();
                            ScreenManager.AddScreen(new BossFightScreen(this, Int32.Parse((name.Substring(4)))));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Takes the bounds and color data of an object and returns the bounds ommiting out transparent lines.
        /// </summary>
        /// <param name="rectangle">Bounds of the object.</param>
        /// <param name="data">Color data of the object.</param>
        /// <param name="actualRectangle">Out: New bounds of the object.</param>
        /// <param name="actualColorData">Out: New color data.</param>
        private void GetActualBounds(Rectangle rectangle, Color[] data, out Rectangle actualRectangle, out Color[] actualColorData)
        {
            int ystart, yend;
            ystart = -1;
            yend = -1;
            bool transparent = true;
            for (int y = rectangle.Top; y < rectangle.Bottom; y++)
            {
                transparent = true;
                for (int x = rectangle.Left; x < rectangle.Right; x++)
                {
                    Color colorA = data[(x - rectangle.Left) +
                                         (y - rectangle.Top) * rectangle.Width];
                    transparent &= colorA.A == 0;
                }

                if (!transparent && ystart < 0) ystart = y;
                if (transparent && yend < 0 && ystart > 0)
                {
                    yend = y - 1;
                    break;
                }
                if (y == rectangle.Bottom - 1) yend = y;
            }

            actualRectangle = new Rectangle(rectangle.X, ystart, rectangle.Width, yend - ystart);

            actualColorData = new Color[actualRectangle.Width * actualRectangle.Height];

            int counter = 0;
            for (int y = rectangle.Top; y < rectangle.Bottom; y++)
            {
                for (int x = rectangle.Left; x < rectangle.Right; x++)
                {
                    if (y >= ystart && y < yend)
                    {
                        actualColorData[counter] = data[(x - rectangle.Left) +
                                         (y - rectangle.Top) * rectangle.Width];
                        counter++;
                    }

                }

            }

        }

        /// <summary>
        /// Pixel-by-pixel collision detection.
        /// </summary>
        /// <param name="rectangleA">Bounds of the first object.</param>
        /// <param name="dataA">Color data of the first object.</param>
        /// <param name="rectangleB">Bounds of the second object.</param>
        /// <param name="dataB">Color data of the second object.</param>
        /// <returns></returns>
        private static bool IntersectPixels(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB)
        {
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    if (colorA.A != 0 && colorB.A != 0)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Plays the sound effect when colliding with a sprite.
        /// </summary>
        /// <param name="name">Sprite's name</param>
        /// <param name="sprite">Instance of the sprite</param>
        private void PlaySoundEffects(String name, Sprite sprite)
        {
            if (!sprite.GetCollided())
            {
                switch (name)
                {
                    case "tomato":
                    case "carrot":
                    case "strawberry":
                    case "orange":
                    case "pineapple":
                    case "broccoli":
                    case "fries":
                    case "hamburger":
                    case "pizza":
                    case "donut":
                    case "muffin":
                    case "hotdog": sprite.PlaySoundEffect(soundEffects[1]); break;
                    case "level1":
                    case "level2":
                    case "level3":
                        if (sprite.IsSlashed())
                            sprite.PlaySoundEffect(soundEffects[6]);
                        else
                        {
                            if (sprite.GetKilled())
                                sprite.PlaySoundEffect(soundEffects[8]);
                            else
                            {
                                if (sprite.IsHit())
                                    sprite.PlaySoundEffect(soundEffects[5]);
                                else
                                {
                                    if (Constants.isPunching && sprite.GetY() == 399)
                                        sprite.PlaySoundEffect(soundEffects[7]);
                                    else
                                        sprite.PlaySoundEffect(soundEffects[5]);
                                }
                            }
                        }
                        break;
                    case "sheild": sprite.PlaySoundEffect(soundEffects[2]); break;
                    case "sword": sprite.PlaySoundEffect(soundEffects[3]); break;
                }
            }
        }

        /// <summary>
        /// Removes the sprites that are outside screen bounds from the list.
        /// </summary>
        private void RemoveSprites()
        {
            for (int i = 0; i <= currentSprite.Count - 1; i++)
            {
                if (currentSprite[i].GetX() < -250)
                {
                    currentSprite.Remove(currentSprite[i]);
                    spriteCounter--;
                }
            }
        }

        public override void Draw(GameTime gametime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);
            bgLayer3.Draw(spriteBatch);

            if(displayAlert)
                spriteBatch.Draw(alertTexture, new Vector2(ScreenManager.GraphicsDevice.Viewport.Width/2 - alertTexture.Width/2, 0), Color.White);
            #region Tamer
            bar.Draw(spriteBatch);
            score.Draw(spriteBatch);
            spriteBatch.DrawString(textFont, " Flu ",new Vector2((screenWidth/40)+14,screenHeight/1.2f),Color.Peru);
            spriteBatch.DrawString(textFont, "Pox ",new Vector2(screenWidth/7,screenHeight/1.2f),Color.Peru);
            spriteBatch.DrawString(textFont, "Malaria ",new Vector2((screenWidth/4)-15,screenHeight/1.2f),Color.Peru);
            spriteBatch.DrawString(textFont, "Dead Virus ", new Vector2(screenWidth / 2f, screenHeight / 1.2f), Color.Peru);
            spriteBatch.DrawString(textFont, "Dizzy Virus ", new Vector2(screenWidth / 1.5f, screenHeight / 1.2f), Color.Peru);
            spriteBatch.DrawString(textFont, "Injured Virus ", new Vector2(screenWidth / 1.2f, screenHeight / 1.2f), Color.Peru);
            spriteBatch.Draw(items[12], new Rectangle((int)(screenWidth / 40)+14, (int)(screenHeight / 1.1f),items[12].Width,items[12].Height), Color.White);
            spriteBatch.Draw(items[13], new Rectangle((int)(screenWidth / 7), (int)(screenHeight / 1.1f), items[13].Width, items[13].Height), Color.White);
            spriteBatch.Draw(items[14], new Rectangle((int)(screenWidth / 4), (int)(screenHeight / 1.1f), items[14].Width, items[14].Height), Color.White);
            spriteBatch.Draw(splashDead, new Rectangle((int)(screenWidth / 2)+40, (int)(screenHeight / 1.1f), splashDead.Width, splashDead.Height), Color.White);
            spriteBatch.Draw(splashSemiDead, new Rectangle((int)(screenWidth / 1.5f)+40, (int)(screenHeight / 1.1f), splashSemiDead.Width, splashSemiDead.Height), Color.White);
            spriteBatch.Draw(splashInjured, new Rectangle((int)(screenWidth / 1.2f)+40, (int)(screenHeight / 1.1f), splashInjured.Width, splashInjured.Height), Color.White);
           #endregion

            foreach (Sprite s in currentSprite)
            {
                s.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
                 
            sword.Draw(spriteBatch);
            shield.Draw(spriteBatch);
            if (player.HasShield())
            {
                shieldAcquired.Draw(spriteBatch);
            }
            if (player.HasSword())
            {
                swordAcquired.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gametime);
        }

    }
}