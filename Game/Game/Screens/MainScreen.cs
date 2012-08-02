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
        Sprite highscores;
        Sprite exit;
        Sprite newGameLabel;
        Sprite instructionsLabel;
        Sprite scoreslabel;
        Sprite exitLabel;

        public MainScreen()
        {

        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager Content)
        {
            menu = new Sprite(Content.Load<Texture2D>("Textures//menu"), new Rectangle(0, 0, 1280, 720));
            newGame = new Sprite(Content.Load<Texture2D>("Textures//new"), new Rectangle(85, 210, 200, 200));
            instructions = new Sprite(Content.Load<Texture2D>("Textures//instructions"), new Rectangle(385, 210, 200, 200));
            highscores = new Sprite(Content.Load<Texture2D>("Textures//highscores"), new Rectangle(685, 210, 200, 200));
            exit = new Sprite(Content.Load<Texture2D>("Textures//exit"), new Rectangle(975, 210, 200, 200));
            newGameLabel = new Sprite(Content.Load<Texture2D>("Textures//new_label"), new Rectangle(70, 430, 240, 50));
            instructionsLabel = new Sprite(Content.Load<Texture2D>("Textures//instructions_label"), new Rectangle(375, 430, 240, 50));
            scoreslabel = new Sprite(Content.Load<Texture2D>("Textures//scores_label"), new Rectangle(665, 430, 240, 50));
            exitLabel = new Sprite(Content.Load<Texture2D>("Textures//exit_label"), new Rectangle(960, 430, 240, 50));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
            newGame.Draw(spriteBatch);
            instructions.Draw(spriteBatch);
            highscores.Draw(spriteBatch);
            exit.Draw(spriteBatch);
            newGameLabel.Draw(spriteBatch);
            instructionsLabel.Draw(spriteBatch);
            scoreslabel.Draw(spriteBatch);
            exitLabel.Draw(spriteBatch);
        }

    }
}
