﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Game.Engine;
using Game.UI;
using Game.Screens.Components;
using Microsoft.Xna.Framework.Media;

namespace Game.Screens
{
    public class PlayScreen : GameScreen
    {

        public static readonly int blockSize = 96;

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

        private ItemsGenerator generator;
        private string[,] current;
        private int globalCounter = 0;
        private int spriteCounter = 0;
        private Sprite[] currentSprite;
        private ContentManager Content;

        public void Initialize()
        {
            player = new Player();
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();
            player.Initialize();

            generator = new ItemsGenerator();
            current = generator.generateMore();
            currentSprite = new Sprite[10];            
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

            for (int i = 0; i <= 9; i++)
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


                if (globalCounter == 1000)
                {
                    current = generator.generateMore();
                    for (int i = 0; i <= 9; i++)
                    {
                        Texture2D texture = Content.Load<Texture2D>("Textures//sword");
                        int length = 40;
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
                            case "shield": texture = Content.Load<Texture2D>("Textures//shield"); break;
                            default: length = 0; break;
                        }
                        int height = 0;
                        switch (current[i, 1])
                        {
                            case "0": height = 400; break;
                            case "1": height = 300; break;
                            case "2": height = 100; break;
                        }
                        currentSprite[i] = new Sprite(texture, new Rectangle(880, height, length, length));
                    }
                    globalCounter = 0;
                    spriteCounter = 0;
                }

                if (globalCounter % 100 == 0)
                {
                    spriteCounter++;
                }

                for (int i = 0; i <= spriteCounter - 1; i++)
                {
                    currentSprite[i].Update(6);
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

            for (int i = 0; i <= 9; i++)
            {
                currentSprite[i].Draw(spriteBatch);
            }
        }


    }
}