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
    public class PlayScreen : GameScreen
    {

        public static bool screenPaused = false;


        #region background music attributes
        private Song[] songs = new Song[2];
        private int playQueue = 1;
        #endregion


        private SoundEffect immunityAudio;
        private int updateImmunityCounter;
        private List<byte[]> colorDataList;

        private Player player;
        private ParallaxingBackground bgLayer1, bgLayer2, bgLayer3;
        private Texture2D alertTexture;
        private int alertTimer;
        private bool displayAlert;

        public Bar bar;
        Score score;

        //Shirin
        private ItemsGenerator generator;
        private string[,] current;
        private int globalCounter = 0;
        private int spriteCounter = 0;
        private Sprite[] currentSprite;
        private Sprite sword,shield,swordAcquired,shieldAcquired;
        private ContentManager Content;
        private SoundEffect[] soundEffects = new SoundEffect[6];
        private Color[] playerData;
        private Rectangle playerBounds;
        private Texture2D[] items;

        public override void Initialize()
        {

            colorDataList = new List<byte[]>();
            player = new Player();
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();
            bgLayer3 = new ParallaxingBackground();
            player.Initialize();

            bar = new Bar(100, 20, 15, 270, 30);
            score = new Score(870, 10, Color.Peru);
            updateImmunityCounter = 0;

            alertTimer = 0;
            displayAlert = false;

            #region Shirin
            generator = new ItemsGenerator();
            current = generator.GenerateMore();
            currentSprite = new Sprite[20];
            items = new Texture2D[19];
            #endregion

            Constants.ResetFlags();
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
            player.LoadContent(Content);

            
            immunityAudio = Content.Load<SoundEffect>("Audio\\Immunity2");

            #region Shirin

            sword = new Sprite(Content.Load<Texture2D>("Textures//sword"), new Rectangle(335, 7, 50, 50));
            shield = new Sprite(Content.Load<Texture2D>("Textures//shield"), new Rectangle(420, 0, 60, 60));
            swordAcquired = new Sprite(Content.Load<Texture2D>("Textures//correct"), new Rectangle(325, 10, 60, 60));
            shieldAcquired = new Sprite(Content.Load<Texture2D>("Textures//correct"), new Rectangle(410, 10, 60, 60));

            for (int i = 0; i <= 19; i++)
            {
                currentSprite[i] = new Sprite(Content.Load<Texture2D>("Textures//Transparent"), new Rectangle(0, 0, 0, 0));
                currentSprite[i].EnterName("Empty");
            }

            soundEffects[0] = Content.Load<SoundEffect>("Audio//Death");
            soundEffects[1] = Content.Load<SoundEffect>("Audio//Food");
            soundEffects[2] = Content.Load<SoundEffect>("Audio//ShieldAcquired");
            soundEffects[3] = Content.Load<SoundEffect>("Audio//SwordAcquired");
            soundEffects[4] = Content.Load<SoundEffect>("Audio//SwordSlash");
            soundEffects[5] = Content.Load<SoundEffect>("Audio//VirusHit");

            items[0] = Content.Load<Texture2D>("Textures//healthy1");
            items[1] = Content.Load<Texture2D>("Textures//healthy2");
            items[2] = Content.Load<Texture2D>("Textures//healthy3");
            items[3] = Content.Load<Texture2D>("Textures//healthy4");
            items[4] = Content.Load<Texture2D>("Textures//healthy5");
            items[5] = Content.Load<Texture2D>("Textures//healthy6");
            items[6] = Content.Load<Texture2D>("Textures//unhealthy1");
            items[7] = Content.Load<Texture2D>("Textures//unhealthy2");
            items[8] = Content.Load<Texture2D>("Textures//unhealthy3");
            items[9] = Content.Load<Texture2D>("Textures//unhealthy4");
            items[10] = Content.Load<Texture2D>("Textures//unhealthy5");
            items[11] = Content.Load<Texture2D>("Textures//unhealthy6");
            items[12] = Content.Load<Texture2D>("Textures//virus1");
            items[13] = Content.Load<Texture2D>("Textures//virus2");
            items[14] = Content.Load<Texture2D>("Textures//virus3");
            items[15] = Content.Load<Texture2D>("Textures//shield");
            items[16] = Content.Load<Texture2D>("Textures//sword");
            items[17] = Content.Load<Texture2D>("Textures//gym");
            items[18] = Content.Load<Texture2D>("Textures//Transparent");

            #endregion

            bgLayer1.Initialize(Content, "Background/Layer 1", ScreenManager.GraphicsDevice.Viewport.Width, -1);
            bgLayer2.Initialize(Content, "Background/Layer 2", ScreenManager.GraphicsDevice.Viewport.Width, -2);
            bgLayer3.Initialize(Content, "Background/Layer 3", ScreenManager.GraphicsDevice.Viewport.Width, -4);

            alertTexture = Content.Load<Texture2D>("Textures/alert");

            playerBounds = player.GetBoundingRectangle();
            playerData = player.GetColorData();

            bar.LoadContent(Content);
            score.LoadContent(Content);
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
            playerData = player.GetColorData();
            playerBounds = player.GetBoundingRectangle();

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


            #endregion
            #region khaled's pausescreen and music
            if (userAvatar.Avatar == userAvatar.AllAvatars[0])
            {
                //Freeze Screen, Show pause Screen
                screenPaused = true;
                ScreenManager.AddScreen(new PauseScreen());
                this.FreezeScreen();
            }
            else if (userAvatar.Avatar.Equals(userAvatar.AllAvatars[2]) && screenPaused == true)
            {
                //exit pause screen, unfreeze screen
                this.UnfreezeScreen();
                screenPaused = false;
            }
           
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




            #region Shirin

            if (globalCounter == 500)
            {
                Sprite[] previousSprites = currentSprite;
                int counter = 10;
                for (int i = 0; i <= 9; i++)
                {
                    currentSprite[i] = previousSprites[counter];
                    counter++;
                }
                counter = 10;
                current = generator.GenerateMore();
                for (int i = 0; i <= 9; i++)
                {
                    int height = 0;
                    switch (current[i, 1])
                    {
                        case "0": height = 499; break;
                        case "1": height = 399; break;
                        case "2": height = 299; break;
                    }
                    Texture2D texture = items[18];
                    Boolean transparent = false;
                    int length = 50;
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
                    }
                    currentSprite[counter] = new Sprite(texture, new Rectangle(1280, height, length, length));
                    currentSprite[counter].EnterName(current[i, 0]);
                    if (transparent)
                    {
                        currentSprite[counter].SetTransparent(true);
                    }
                    counter++;
                }
                globalCounter = 0;
                spriteCounter = 10;
            }

            for (int i = 0; i <= 19; i++)
            {
                Rectangle itemBounds = new Rectangle(currentSprite[i].GetX(), currentSprite[i].GetY(), currentSprite[i].GetWidth(), currentSprite[i].GetHeight());
                String name = currentSprite[i].GetName();
                if (IntersectPixels(playerBounds, playerData, itemBounds, currentSprite[i].GetColorData()))
                {
                    if (!currentSprite[i].GetTransparent())
                    {
                        if (name.Equals("level1") || name.Equals("level2") || name.Equals("level3"))
                        {
                            if (IsAbove(playerBounds, itemBounds) && playerBounds.Bottom > 349 && currentSprite[i].GetHeight() > 299)
                            {
                                currentSprite[i].KillVirus();
                            }
                            else
                            {
                                if (Constants.isSwappingHand && player.HasSword() && currentSprite[i].GetY() == 399)
                                {
                                    currentSprite[i].SlashVirus();
                                }
                                else
                                {
                                    if (!Constants.isPunching)
                                    {
                                        currentSprite[i].HitVirus();
                                    }
                                }
                            }
                         
                        }
                        Effects(name, currentSprite[i]);
                        PlaySoundEffects(name, currentSprite[i]);
                        if (!name.Equals("gym"))
                        {
                            currentSprite[i].Collide(Content, name);
                        }
                        else
                        {
                            if (Constants.isSteppingRight)
                            {
                                FreezeScreen();
                                ScreenManager.AddScreen(new ExcercisesScreen(this));
                            }
                        }
                    }
                }
            }

            if (globalCounter % 50 == 0)
            {
                spriteCounter++;
            }

            for (int i = 0; i <= spriteCounter - 1; i++)
            {
                currentSprite[i].Update(4);
            }

            globalCounter++;

            #endregion


            bar.SetCurrentValue(player.Immunity);
            score.score = player.Score;
            base.Update(gameTime);
        }


        /// <summary>
        /// Author: Ahmed Shirin
        /// </summary>
        public override void Draw(GameTime gametime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);
            bgLayer3.Draw(spriteBatch);

            if(displayAlert)
                spriteBatch.Draw(alertTexture, new Vector2(ScreenManager.GraphicsDevice.Viewport.Width/2 - alertTexture.Width/2, 0), Color.White);
            bar.Draw(spriteBatch);
            score.Draw(spriteBatch);
            spriteBatch.End();
            #region Shirin
            foreach (Sprite s in currentSprite)
            {
                s.Draw(spriteBatch);
            }
            SpriteBatch sprite = spriteBatch;
            sprite.Begin();
            player.Draw(sprite);
            sprite.End();            
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
            #endregion
            base.Draw(gametime);
        }


        /// <summary>
        /// Author: Ahmed Shirin
        /// </summary>
        public static bool IntersectPixels(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB)
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
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void PlaySoundEffects(String name, Sprite sprite)
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
                    case "level3": sprite.PlaySoundEffect(soundEffects[5]); break;
                    case "sheild": sprite.PlaySoundEffect(soundEffects[2]); break;
                    case "sword": sprite.PlaySoundEffect(soundEffects[3]); break;
                }
            }
        }

        public void Effects(String name, Sprite sprite)
        {
            if (!sprite.GetCollided())
            {
                switch (name)
                {
                    case "tomato": player.Collided(Constants.healthy1);break;
                    case "carrot": player.Collided(Constants.healthy2);break;
                    case "strawberry": player.Collided(Constants.healthy3); break;
                    case "orange": player.Collided(Constants.healthy4);break;
                    case "pineapple": player.Collided(Constants.healthy5); break;
                    case "broccoli": player.Collided(Constants.healthy6); break;
                    case "fries": player.Collided(Constants.unhealthy1); break;
                    case "hamburger": player.Collided(Constants.unhealthy2); break;
                    case "pizza": player.Collided(Constants.unhealthy3); break;
                    case "donut": player.Collided(Constants.unhealthy4); break;
                    case "muffin": player.Collided(Constants.unhealthy5); break;
                    case "hotdog": player.Collided(Constants.unhealthy6); break;
                    case "level1":
                        if(!sprite.GetKilled()){
                        if (!player.HasShield())
                        {
                            if (!player.HasSword() || !Constants.isSwappingHand)
                            {
                                if (!Constants.isPunching)
                                {
                                    player.Collided(Constants.level1);
                                }
                                else
                                {
                                    if (sprite.GetY() != 399)
                                    {
                                        player.Collided(Constants.level1);
                                    }
                                }
                            }
                            else
                            {
                                if (sprite.GetY() != 399)
                                {
                                    player.Collided(Constants.level1);
                                }
                            }
                        }
                        else
                        {
                            player.AcquireShield(false);
                        }
                        }
                        break;
                    case "level2":
                        if(!sprite.GetKilled()){
                        if (!player.HasShield())
                        {
                            if (!player.HasSword() || !Constants.isSwappingHand)
                            {
                                if (!Constants.isPunching)
                                {
                                    player.Collided(Constants.level2);
                                }
                                else
                                {
                                    if (sprite.GetY() == 399)
                                    {
                                        player.Collided((int)(Constants.level2 / 2));
                                    }
                                    else
                                    {
                                        player.Collided(Constants.level2);
                                    }
                                }
                            }
                            else
                            {
                                if (sprite.GetY() != 399)
                                {
                                    player.Collided(Constants.level2);
                                }
                            }
                        }
                        else
                        {
                            player.AcquireShield(false);
                        }
                        }
                        break;
                    case "level3":
                        if(!sprite.GetKilled()){
                        if (!player.HasShield())
                        {
                            if (!player.HasSword() || !Constants.isSwappingHand)
                            {
                                if (!Constants.isPunching)
                                {
                                    player.Collided(Constants.level3);
                                }
                                else
                                {
                                    if (sprite.GetY() == 399)
                                    {
                                        player.Collided((int)(Constants.level3 / 4));
                                    }
                                    else
                                    {
                                        player.Collided(Constants.level3);
                                    }
                                }
                            }
                            else
                            {
                                if (sprite.GetY() == 399)
                                {
                                    player.Collided((int)(Constants.level3 / 2));
                                }
                                else
                                {
                                    player.Collided(Constants.level3);
                                }
                            }
                        }
                        else
                        {
                            player.AcquireShield(false);
                        } 
                        }
                        break;
                    case "sheild": player.AcquireShield(true); break;
                    case "sword": player.AcquireSword(true); break;
                }
            }
        }

        public Player GetPlayer()
        {
            return this.player;
        }

        public Boolean IsAbove(Rectangle playerBounds, Rectangle itemBounds)
        {
            return (playerBounds.Y + playerBounds.Height - 30) <= itemBounds.Y + itemBounds.Height;
        }

        public void GetActualBounds(Rectangle rectangle, Color[] data, out Rectangle actualRectangle, out Color[] actualColorData)
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
    }
}