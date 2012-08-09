 using Game.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using System.Text;
using Game.Text;
namespace Game.Screens
{
    public class HighScoresScreen : GameScreen
    {
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphics;
        private int screenWidth,screenHeight;
        private float maxStringLength;
        private ContentManager content;
        private SpriteFont font;
        private string[] names;
        private string drawnScores,drawnNames,readedText;
        private List<int> listscore;
        private List<string> list;
        private Texture2D backgroundImage,buttonImage;
        Button OkButton;
        HandCursor hand;
        public HighScoresScreen()
        {
            OkButton = new Button();
            hand = new HandCursor();
            listscore = new List<int>();
            list = new List<string>();
          
            OkButton.Clicked += new Button.ClickedEventHandler(menu_Clicked);
        }
        public override void Initialize()
        {
            content = ScreenManager.Game.Content;
            graphics = ScreenManager.GraphicsDevice;
            spriteBatch = ScreenManager.SpriteBatch;
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
            OkButton.Initialize("Buttons//exit", ScreenManager.Kinect, new Vector2(screenWidth/1.5f,screenHeight/3.5f), 200, 200);
            hand.Initialize(ScreenManager.Kinect);            
            base.Initialize();
        }
        void menu_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
            ScreenManager.AddScreen(new MainScreen());
        }
        public override void LoadContent()
        {
            backgroundImage = content.Load<Texture2D>("Textures/highScoresScreen");
            font = content.Load<SpriteFont>("SpriteFont1");
           // buttonImage = content.Load<Texture2D>("Buttons/X");
            //readedText = System.IO.File.ReadAllText("Text/HighScores.txt");
            names = System.IO.File.ReadAllLines("Text/HighScores.txt");
            OkButton.LoadContent(content);
            hand.LoadContent(content);
            split();
            base.LoadContent();
        }

        private void split()
        {
            //names = readedText.Split('\n');
            maxStringLength = 0;
            for (int i = 0; i < names.Length; i++)
            {
                string[] temp = names[i].Split(',');
                list.Add(temp[0]);
                listscore.Add(Int32.Parse(temp[1]));

                if (font.MeasureString(temp[0]).X > maxStringLength)
                    maxStringLength = font.MeasureString(temp[0]).X;
            }
            foreach (int i in listscore)
            {
                drawnScores += i + "\n \n";
            }
            foreach (string i in list)
            {
                drawnNames += i + "\n \n";
            }

        }
        public override void Update(GameTime gameTime)
        {
            OkButton.Update(gameTime);
            hand.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, graphics.Viewport.Bounds, Color.White);
            spriteBatch.DrawString(font,drawnScores, new Vector2(screenWidth/2f,screenHeight/6f),Color.White);
            spriteBatch.DrawString(font, drawnNames, new Vector2(screenWidth /2.5f,screenHeight/6f), Color.White);
            OkButton.Draw(spriteBatch);
            hand.Draw(spriteBatch);
          //Test  spriteBatch.DrawString(font,Constants.isSwappingHand+"", new Vector2(10,10), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Remove()
        {
            base.Remove();
        }



    }
}
