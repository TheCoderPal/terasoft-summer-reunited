using Game.UI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Screens
{
    public class ExcercisesScreen : GameScreen
    {
        Sprite dumbbell;
        Sprite treadmill;
        Sprite background;

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public ExcercisesScreen()
        {

        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager Content)
        {
            background = new Sprite(Content.Load<Texture2D>("Textures//choosing_frame"), new Rectangle(0, 0, 1280, 720));
            dumbbell = new Sprite(Content.Load<Texture2D>("Textures//dumbbell"), new Rectangle(140, 290, 300, 290));
            treadmill = new Sprite(Content.Load<Texture2D>("Textures//treadmill"), new Rectangle(800, 250, 300, 350));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            dumbbell.Draw(spriteBatch);
            treadmill.Draw(spriteBatch);
        }
    }
}
