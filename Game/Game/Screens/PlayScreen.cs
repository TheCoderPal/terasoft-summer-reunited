﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Game.Engine;
using Game.UI;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Game.Text;
using System.Diagnostics;

namespace Game.Screens
{
    public class PlayScreen : GameScreen
    {

        public static bool screenPaused = false;


        #region background music attributes
        private Song[] songs = new Song[2];
        private int playQueue = 1;
        #endregion
        private Player player;
        private ParallaxingBackground bgLayer1, bgLayer2, bgLayer3;
        public Bar bar;
        Score score;

        //Shirin
        private ItemsGenerator generator;
        private string[,] current;
        private int globalCounter = 0;
        private int spriteCounter = 0;
        private Sprite[] currentSprite;
        private Sprite sword;
        private Sprite shield;
        private Sprite swordAcquired;
        private Sprite shieldAcquired;
        private ContentManager Content;
        private SoundEffect[] soundEffects = new SoundEffect[6];
        private Color[] playerData;
        private Rectangle playerBounds;

        public override void Initialize()
        {
            player = new Player();
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();
            bgLayer3 = new ParallaxingBackground();
            player.Initialize();

            bar = new Bar(100, 20, 15, 270, 30); 
            score = new Score(900, 10, Color.Red);

            //Shirin
            generator = new ItemsGenerator();
            current = generator.generateMore();
            currentSprite = new Sprite[20];

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

            //Shirin
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



            bgLayer1.Initialize(Content, "Background/Layer 1", ScreenManager.GraphicsDevice.Viewport.Width, -1);
            bgLayer2.Initialize(Content, "Background/Layer 2", ScreenManager.GraphicsDevice.Viewport.Width, -2);
            bgLayer3.Initialize(Content, "Background/Layer 3", ScreenManager.GraphicsDevice.Viewport.Width, -4);

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
                this.Remove();
                ScreenManager.AddScreen(new LosingScreen(player.Score));
            }
            
            #endregion
            
            if (userAvatar.Avatar[0] == userAvatar.AllAvatars[0])
            {
                //Freeze Screen, Show pause Screen
                screenPaused = true;
                ScreenManager.AddScreen(new PauseScreen());
                this.FreezeScreen();
            }
            else if (userAvatar.Avatar[0].Equals(userAvatar.AllAvatars[2]) && screenPaused == true)
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



            //Shirin

            
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
                current = generator.generateMore();
                for (int i = 0; i <= 9; i++)
                {
                    int height = 0;
                    switch (current[i, 1])
                    {
                        case "0": height = 499; break;
                        case "1": height = 399; break;
                        case "2": height = 299; break;
                    }
                    Texture2D texture = Content.Load<Texture2D>("Textures//Transparent");
                    Boolean transparent = false;
                    int length = 50;
                    switch (current[i, 0])
                    {
                        case "banana": texture = Content.Load<Texture2D>("Textures//healthy1"); break;
                        case "apple": texture = Content.Load<Texture2D>("Textures//healthy2"); break;
                        case "orange": texture = Content.Load<Texture2D>("Textures//healthy3"); break;
                        case "hamburg": texture = Content.Load<Texture2D>("Textures//unhealthy1"); break;
                        case "fries": texture = Content.Load<Texture2D>("Textures//unhealthy2"); break;
                        case "hotdog": texture = Content.Load<Texture2D>("Textures//unhealthy3"); break;
                        case "level1": texture = Content.Load<Texture2D>("Textures//virus1"); break;
                        case "level2": texture = Content.Load<Texture2D>("Textures//virus2"); break;
                        case "level3": texture = Content.Load<Texture2D>("Textures//virus3"); break;
                        case "sheild": texture = Content.Load<Texture2D>("Textures//shield"); break;
                        case "sword": texture = Content.Load<Texture2D>("Textures//sword"); break;
                        case "gym": texture = Content.Load<Texture2D>("Textures//gym"); length = 200; height = 349; break;
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
                if (IntersectPixels(playerBounds, playerData, new Rectangle(currentSprite[i].GetX(), currentSprite[i].GetY(), currentSprite[i].GetWidth(), currentSprite[i].GetHeight()), currentSprite[i].GetColorData()))
                {
                    if (!currentSprite[i].GetTransparent())
                    {
                        Effects(currentSprite[i].GetName(), currentSprite[i]);
                        if (!currentSprite[i].GetName().Equals("gym"))
                        {
                            currentSprite[i].Collide();
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


            bar.setCurrentValue(player.Immunity);
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


            spriteBatch.End();

            bar.Draw(spriteBatch);
            score.Draw(spriteBatch);

            //Shirin
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

        public void Effects(String name, Sprite sprite)
        {
            if (!sprite.GetCollided())
            {
                switch (name)
                {
                    case "banana": player.Collided(3); sprite.PlaySoundEffect(soundEffects[1]); break;
                    case "apple": player.Collided(5); sprite.PlaySoundEffect(soundEffects[1]); break;
                    case "orange": player.Collided(7); sprite.PlaySoundEffect(soundEffects[1]); break;
                    case "hamburg": player.Collided(-8); sprite.PlaySoundEffect(soundEffects[1]); break;
                    case "fries": player.Collided(-6); sprite.PlaySoundEffect(soundEffects[1]); break;
                    case "hotdog": player.Collided(-4); sprite.PlaySoundEffect(soundEffects[1]); break;
                    case "level1": if (!player.HasShield())
                        {
                            if (!player.HasSword()) { player.Collided(-5); }
                            else { player.Collided(-2); player.AcquireSword(true); };
                        }
                        else { player.AcquireShield(false); };
                        sprite.PlaySoundEffect(soundEffects[5]); break;
                    case "level2": if (!player.HasShield())
                        {
                            if (!player.HasSword()) { player.Collided(-8); }
                            else { player.Collided(-4); player.AcquireSword(true); };
                        }
                        else { player.AcquireShield(false); };
                        sprite.PlaySoundEffect(soundEffects[5]); break;
                    case "level3": if (!player.HasShield())
                        {
                            if (!player.HasSword()) { player.Collided(-12); }
                            else { player.Collided(-6); player.AcquireSword(true); };
                        }
                        else { player.AcquireShield(false); };
                        sprite.PlaySoundEffect(soundEffects[5]); break;
                    case "sheild": player.AcquireShield(true); sprite.PlaySoundEffect(soundEffects[2]); break;
                    case "sword": player.AcquireSword(true); sprite.PlaySoundEffect(soundEffects[3]); break;
                   // case "gym": if (Constants.isSteppingRight) { this.FreezeScreen(); new ExcercisesScreen(); }; break;
                }
            }
        }

        public Player GetPlayer()
        {
            return this.player;
        }
    }
}