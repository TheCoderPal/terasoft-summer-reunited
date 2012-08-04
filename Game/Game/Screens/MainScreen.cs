using Game.UI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Screens
{
    public class MainScreen : GameScreen
    {
        Sprite menu;
        Button newGame;
        Button instructions;
        Button highscores;
        Button exit;
        Sprite newGameLabel;
        Sprite instructionsLabel;
        Sprite scoreslabel;
        Sprite exitLabel;
        SpriteBatch spriteBatch;
        ContentManager Content;
        Game.Kinect.Kinect kinect;

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>

        public MainScreen()
        {

        }

        public override void Initialize()
        {
            newGame = new Button();
            instructions = new Button();
            highscores = new Button();
            exit = new Button();
            newGame.Initialize("Textures//new", kinect, new Vector2(85, 210), 200, 200);
            instructions.Initialize("Textures//instructions", kinect, new Vector2(385, 210), 200, 200);
            highscores.Initialize("Textures//highscores", kinect, new Vector2(685, 210), 200, 200);
            exit.Initialize("Textures//exit", kinect, new Vector2(975, 210), 200, 200);
            newGame.Clicked += new Button.ClickedEventHandler(newGame_Clicked);
            instructions.Clicked += new Button.ClickedEventHandler(instructions_Clicked);
            highscores.Clicked += new Button.ClickedEventHandler(highscores_Clicked);
            exit.Clicked += new Button.ClickedEventHandler(exit_Clicked);
        }

        void exit_Clicked(object sender, System.EventArgs a)
        {
           
        }

        void highscores_Clicked(object sender, System.EventArgs a)
        {
            ScreenManager.AddScreen(new HighScoresScreen());
        }

        void instructions_Clicked(object sender, System.EventArgs a)
        {
            ScreenManager.AddScreen(new InstructionsScreen());
        }

        void newGame_Clicked(object sender, System.EventArgs a)
        {
            ScreenManager.AddScreen(new PlayScreen());
        }

        public override void LoadContent()
        {
            Content = ScreenManager.Game.Content;
            spriteBatch = ScreenManager.SpriteBatch;
            newGame.LoadContent(Content);
            instructions.LoadContent(Content);
            highscores.LoadContent(Content);
            exit.LoadContent(Content);
            menu = new Sprite(Content.Load<Texture2D>("Textures//menu"), new Rectangle(0, 0, 1280, 720));           
            newGameLabel = new Sprite(Content.Load<Texture2D>("Textures//new_label"), new Rectangle(70, 430, 240, 50));
            instructionsLabel = new Sprite(Content.Load<Texture2D>("Textures//instructions_label"), new Rectangle(375, 430, 240, 50));
            scoreslabel = new Sprite(Content.Load<Texture2D>("Textures//scores_label"), new Rectangle(665, 430, 240, 50));
            exitLabel = new Sprite(Content.Load<Texture2D>("Textures//exit_label"), new Rectangle(960, 430, 240, 50));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            menu.Draw(spriteBatch);
            SpriteBatch sprite = spriteBatch;
            sprite.Begin();
            newGame.Draw(spriteBatch);
            instructions.Draw(spriteBatch);
            highscores.Draw(spriteBatch);
            exit.Draw(spriteBatch);
            sprite.End();            
            newGameLabel.Draw(spriteBatch);
            instructionsLabel.Draw(spriteBatch);
            scoreslabel.Draw(spriteBatch);
            exitLabel.Draw(spriteBatch);
        }

    }
}
