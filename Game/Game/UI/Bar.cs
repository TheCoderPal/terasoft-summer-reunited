using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Game.UI
{
    /// <summary>
    /// Tamer Nabil
    /// </summary>
    public class Bar
    {
        private Texture2D activeBar;
        private int maxValue, barPositionX, barPositionY, widthBar, heightBar;
        private int currentValue;
        public Color goodPartColor { get; set; }
        public Color badPartColor { get; set; }

        public int GetCurrentValue()
        {
            return currentValue;
        }
        public void SetCurrentValue(int value)
        {
            if (value > maxValue)
                currentValue = maxValue;
            else
                currentValue = value;
        }
        public Bar(int maxValue, int barPositionX, int barPositionY, int widthBar, int heightBar)
        {
            this.maxValue = maxValue;
            this.barPositionX = barPositionX;
            this.barPositionY = barPositionY;
            this.widthBar = widthBar;
            this.heightBar = heightBar;
            currentValue = 100;
            goodPartColor = Color.SeaGreen;
            badPartColor = Color.Red;
        }

        public void LoadContent(ContentManager Content)
        {
            activeBar = Content.Load<Texture2D>("HealthBar");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Rectangle source = new Rectangle(0, 45, activeBar.Width, 44);
            //Draw the negative space for the health bar
            spriteBatch.Draw(activeBar, new Rectangle(barPositionX, barPositionY, widthBar, heightBar), source, badPartColor);
            //Draw the current health level based on the current Health
            spriteBatch.Draw(activeBar, new Rectangle(barPositionX, barPositionY, (int)(widthBar * ((double)currentValue / maxValue)), heightBar), source, goodPartColor);
            //Draw the box around the health bar
            spriteBatch.Draw(activeBar, new Rectangle(barPositionX, barPositionY, widthBar, heightBar),
                       new Rectangle(0, 0, activeBar.Width, 44), Color.White);
            spriteBatch.End();
        }
    }
}
