﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Game.Engine;
using Game.UI;
using Game.Screens.Components;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Game.Screens
{
    public class PlayScreen : GameScreen
    {

        public static readonly int blockSize = 96;
        public bool screenPaused = false;
        private Track track;
        private TrackUI trackUI;
        private ItemsUI itemsUI;
        #region background music attributes
        private Song[] songs= new Song[3];
        private bool songstart = false;
        private int playQueue = 1;
        #endregion

        private Player player;
        private ParallaxingBackground bgLayer1, bgLayer2;

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
        private Rectangle playerBounds;
        private Rectangle[] itemBounds = new Rectangle[20];
        private Color[] avatarData;
        private List<Color[]> itemsData;
        
        public void Initialize()
        {
            player = new Player();
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();
            player.Initialize();

            //Shirin
            generator = new ItemsGenerator();
            current = generator.generateMore();
            currentSprite = new Sprite[20];
            playerBounds = player.GetBoundingRectangle();
            for (int i = 0; i <= 19; i++) 
            {
                itemBounds[i] = new Rectangle(0, 0, 0, 0);
            }
            avatarData = player.GetColorData();
        }
        /// <remarks>
        ///<para>AUTHOR: Khaled Salah, Ahmed Shirin </para>
        ///</remarks>
        public void LoadContent(ContentManager Content)
        {
            songs[0] = Content.Load<Song>("Directory\\songtitle");
            songs[1] = Content.Load<Song>("Directory\\songtitle");
            songs[2] = Content.Load<Song>("Directory\\songtitle");
            MediaPlayer.IsRepeating = true;

            player.LoadContent(Content);

            //Shirin
            sword = new Sprite(Content.Load<Texture2D>("Textures//sword"), new Rectangle(300, 0, 35, 65));
            shield = new Sprite(Content.Load<Texture2D>("Textures//shield"), new Rectangle(390, 0, 60, 60));
            swordAcquired = new Sprite(Content.Load<Texture2D>("Textures//correct"), new Rectangle(290, 10, 60, 60));
            shieldAcquired = new Sprite(Content.Load<Texture2D>("Textures//correct"), new Rectangle(380, 10, 60, 60));

            for (int i = 0; i <= 19; i++)
            {
                currentSprite[i] = new Sprite(Content.Load<Texture2D>("Textures//sword"), new Rectangle(0, 0, 0, 0));
            }
            this.Content = Content;


            //bgLayer1.Initialize(Content, "", graphics.Viewport.Width, -1);
            //bgLayer1.Initialize(Content, "", graphics.Viewport.Width, -1);

        }

        /// <remarks>
        ///<para>AUTHOR: Khaled Salah, Ahmed Shirin </para>
        ///</remarks>
        public void Update(GameTime gameTime)
        {
            if (userAvatar.Avatar[0].Equals(userAvatar.AllAvatars[0]))
            {
                //Freeze Screen, Show pause Screen
               // this.FreezeScreen();
                screenPaused = true;
            }
            else if (userAvatar.Avatar[0].Equals(userAvatar.AllAvatars[2]) && screenPaused == true)
            {
                //exit pause screen, unfreeze screen
                this.UnfreezeScreen();
                screenPaused = false;
            }
                if (MediaPlayer.State.Equals(MediaState.Stopped))
                {
                    switch(playQueue)
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
                        case 3:
                    {
                        MediaPlayer.Play(songs[2]);
                        playQueue = 1;
                        break;
                    }
                    }
                }
                bgLayer1.Update();
                bgLayer2.Update();



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
                        Texture2D texture = Content.Load<Texture2D>("Textures//sword");
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
                            default: length = 0; break;
                        }
                        int height = 0;
                        switch (current[i, 1])
                        {
                            case "0": height = 300; break;
                            case "1": height = 200; break;
                            case "2": height = 100; break;
                        }
                        currentSprite[counter] = new Sprite(texture, new Rectangle(880, height, length, length));
                        counter++;
                    }
                    globalCounter = 0;
                    spriteCounter = 10;
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
                
        }


        /// <summary>
        /// Author: Ahmed Shirin
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);

            player.Draw(spriteBatch);

            //Shirin
            for (int i = 0; i <= currentSprite.Length - 1; i++)
            {
                currentSprite[i].Draw(spriteBatch);
            }
            sword.Draw(spriteBatch);
            shield.Draw(spriteBatch);            
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


    }
}