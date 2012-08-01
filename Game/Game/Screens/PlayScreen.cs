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

        private SpriteAnimation animation;
        private Track track;
        private TrackUI trackUI;
        private ItemsUI itemsUI;
        #region background music attributes
        private Song[] songs= new Song[3];
        private bool songstart = false;
        private int playQueue = 1;
        #endregion

        public void Initialize()
        {

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
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }


    }
}