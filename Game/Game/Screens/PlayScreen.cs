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

        public void Initialize()
        {
            player = new Player();
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();

            player.Initialize();
        }
        /// <remarks>
        ///<para>AUTHOR: Khaled Salah </para>
        ///</remarks>
        public void LoadContent(ContentManager Content)
        {
            songs[0] = Content.Load<Song>("Directory\\songtitle");
            songs[1] = Content.Load<Song>("Directory\\songtitle");
            songs[2] = Content.Load<Song>("Directory\\songtitle");
            MediaPlayer.IsRepeating = true;

            player.LoadContent(Content);
            //bgLayer1.Initialize(Content, "", graphics.Viewport.Width, -1);
            //bgLayer1.Initialize(Content, "", graphics.Viewport.Width, -1);

        }

        /// <remarks>
        ///<para>AUTHOR: Khaled Salah </para>
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);

            player.Draw(spriteBatch);
        }


    }
}