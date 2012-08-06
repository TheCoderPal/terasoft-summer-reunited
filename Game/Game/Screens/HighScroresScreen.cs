using Game.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using System.Text;
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
        private List<int> listscore = new List<int>();
        private List<string> list = new List<string>();
        public override void LoadContent()
        {
            content = ScreenManager.Game.Content;
            graphics = ScreenManager.GraphicsDevice;
            spriteBatch = ScreenManager.SpriteBatch;
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
            font = content.Load<SpriteFont>("SpriteFont1");
            readedText = System.IO.File.ReadAllText("Text/HighScores.txt");
            names = readedText.Split('\n');
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
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font,drawnScores, new Vector2(screenWidth/2f,screenHeight/6f),Color.White);
            spriteBatch.DrawString(font, drawnNames, new Vector2(screenWidth /2.5f,screenHeight/6f), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Remove()
        {
            base.Remove();
        }



    }
}
