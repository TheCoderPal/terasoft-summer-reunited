using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Game.UI
{
    /// <summary>
    /// Tamer Nabil
    /// </summary>
    class Score
    {
        private int positionX;
        private int positionY;
        private Color color;
        private SpriteFont font;
        public Score(int positionX,int positionY,Color color)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.color = color;
            score = 0;
        }

         public int score { get; set; }
         public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("SpriteFont1");
        }
        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "score: " + score,new Vector2(positionX,positionY),color);
            spriteBatch.End();
        }



    }
}
