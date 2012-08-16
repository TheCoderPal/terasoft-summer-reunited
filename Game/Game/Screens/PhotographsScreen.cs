using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Screens
{
    class PhotographsScreen : GameScreen
    {
        List<byte[]> colorDataList;
        Texture2D backgroundImage;
        Button saveButton, nextButton;
        int index, playerScore;
        HandCursor hand;

        public PhotographsScreen(List<byte[]> colorDataList, int score)
        {
            this.colorDataList = colorDataList;
            saveButton = new Button();
            nextButton = new Button();
            hand = new HandCursor();
            playerScore = score;
        }

        public override void Initialize()
        {
            index = 0;
            saveButton.Initialize("Buttons/save", ScreenManager.Kinect, new Vector2(1050, 210));
            nextButton.Initialize("Buttons/next", ScreenManager.Kinect, new Vector2(1050, 360));
            saveButton.Clicked += new Button.ClickedEventHandler(saveButton_Clicked);
            nextButton.Clicked += new Button.ClickedEventHandler(nextButton_Clicked);

            hand.Initialize(ScreenManager.Kinect);
            base.Initialize();
        }

        void nextButton_Clicked(object sender, EventArgs a)
        {
            index++;
            if (index == colorDataList.Count)
            {
                this.Remove();
                ScreenManager.AddScreen(new LosingScreen(playerScore));
            }
            else
                backgroundImage.SetData(colorDataList[index]);
        }

        void saveButton_Clicked(object sender, EventArgs a)
        {
            string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

            string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            string path = Path.Combine(myPhotos, "KinectSnapshot-" + time + ".png");

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    backgroundImage.SaveAsJpeg(fs, ScreenManager.Kinect.GetFrameWidth(), ScreenManager.Kinect.GetFrameHeight());
                }
            }
            catch (IOException)
            {
                Debug.WriteLine("Save not successful");
            }
        }

        public override void LoadContent()
        {
            backgroundImage = new Texture2D(ScreenManager.GraphicsDevice, 
                ScreenManager.Kinect.GetFrameWidth(), ScreenManager.Kinect.GetFrameHeight());
            backgroundImage.SetData(colorDataList[index]);
            saveButton.LoadContent(ScreenManager.Game.Content);
            nextButton.LoadContent(ScreenManager.Game.Content);

            hand.LoadContent(ScreenManager.Game.Content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            saveButton.Update(gameTime);
            nextButton.Update(gameTime);

            hand.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, 
                new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height), Color.White);
            nextButton.Draw(spriteBatch);
            saveButton.Draw(spriteBatch);
            hand.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
