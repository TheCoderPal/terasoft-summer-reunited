using Game.UI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Screens
{
    public class MainScreen : GameScreen
    {
        Sprite menu;
        Sprite newGame;
        Sprite instructions;
        Sprite exit;
        Sprite newGameLabel;
        Sprite instructionsLabel;
        Sprite exitLabel;
        GraphicsDeviceManager graphics;

        public MainScreen(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.IsFullScreen = true;
        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager Content)
        {
            menu = new Sprite(Content.Load<Texture2D>("Textures//menu"), new Rectangle(0, 0, 1280, 720));
            newGame = new Sprite(Content.Load<Texture2D>("Textures//new"), new Rectangle(140, 230, 200, 200));
            instructions = new Sprite(Content.Load<Texture2D>("Textures//instructions"), new Rectangle(540, 230, 200, 200));
            exit = new Sprite(Content.Load<Texture2D>("Textures//exit"), new Rectangle(940, 230, 200, 200));
            newGameLabel = new Sprite(Content.Load<Texture2D>("Textures//new_label"), new Rectangle(140, 450, 200, 50));
            instructionsLabel = new Sprite(Content.Load<Texture2D>("Textures//instructions_label"), new Rectangle(545, 450, 200, 50));
            exitLabel = new Sprite(Content.Load<Texture2D>("Textures//exit_label"), new Rectangle(950, 450, 200, 50));

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
            newGame.Draw(spriteBatch);
            instructions.Draw(spriteBatch);
            exit.Draw(spriteBatch);
            newGameLabel.Draw(spriteBatch);
            instructionsLabel.Draw(spriteBatch);
            exitLabel.Draw(spriteBatch);
        }

    }
}
