using System;
using System.Collections.Generic;
using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Game.Screens
{
    public class HighScoresScreen : GameScreen
    {
        #region Tamer InstanceVariables + Constructor
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphics;
        private int screenWidth,screenHeight;
        private float maxStringLength;
        private ContentManager content;
        private SpriteFont font;
        private string[] names;
        private string drawnScores,drawnNames;
        private List<int> listScore;
        private List<string> list;
        private Texture2D backgroundImage;
        private Button okButton;
        private HandCursor hand;
        public HighScoresScreen()
        {
            okButton = new Button();
            hand = new HandCursor();
            listScore = new List<int>();
            list = new List<string>();
          
            okButton.Clicked += new Button.ClickedEventHandler(menu_Clicked);
        }
        #endregion
        #region Initialize
        public override void Initialize()
        {
            content = ScreenManager.Game.Content;
            graphics = ScreenManager.GraphicsDevice;
            spriteBatch = ScreenManager.SpriteBatch;
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
            okButton.Initialize("Buttons//exit", ScreenManager.Kinect, new Vector2(screenWidth/1.5f,screenHeight/3.5f), 200, 200);
            hand.Initialize(ScreenManager.Kinect);            
            base.Initialize();
        }
        #endregion
        #region button event + LoadContent
        void menu_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
            ScreenManager.AddScreen(new MainScreen());
        }
        public override void LoadContent()
        {
            backgroundImage = content.Load<Texture2D>("Textures/highScoresScreen");
            font = content.Load<SpriteFont>("newFont");
            names = System.IO.File.ReadAllLines("Text/HighScores.txt");
            okButton.LoadContent(content);
            hand.LoadContent(content);
            Split();
            base.LoadContent();
        }
        #endregion
        #region Split Method
        private void Split()
        {
            maxStringLength = 0;
            for (int i = 0; i < names.Length; i++)
            {
                string[] temp = names[i].Split(',');
                list.Add(temp[0]);
                listScore.Add(Int32.Parse(temp[1]));

                if (font.MeasureString(temp[0]).X > maxStringLength)
                    maxStringLength = font.MeasureString(temp[0]).X;
            }
            foreach (int i in listScore)
            {
                drawnScores += i + "\n \n";
            }
            foreach (string i in list)
            {
                drawnNames += i + "\n \n";
            }

        }
  #endregion
        #region update + draw methods
        public override void Update(GameTime gameTime)
        {
            okButton.Update(gameTime);
            hand.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, graphics.Viewport.Bounds, Color.White);
            spriteBatch.DrawString(font,drawnScores, new Vector2(screenWidth/1.8f,screenHeight/6f),Color.White);
            spriteBatch.DrawString(font, drawnNames, new Vector2(screenWidth /2.7f,screenHeight/6f), Color.White);
            okButton.Draw(spriteBatch);
            hand.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
        public override void Remove()
        {
            base.Remove();
        }



    }
}
