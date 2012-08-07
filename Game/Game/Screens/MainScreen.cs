using Game.UI;
using Game;
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
        HandCursor Hand;

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>

        public MainScreen()
        {

        }

        public override void Initialize()
        {
            showAvatar = true;
            newGame = new Button();
            instructions = new Button();
            highscores = new Button();
            exit = new Button();
            Hand = new HandCursor();
            kinect = ScreenManager.Kinect;
            newGame.Initialize("Buttons//new", kinect, new Vector2(85, 210), 200, 200);
            instructions.Initialize("Buttons//instructions", kinect, new Vector2(385, 210), 200, 200);
            highscores.Initialize("Buttons//highscores", kinect, new Vector2(685, 210), 200, 200);
            exit.Initialize("Buttons//exit", kinect, new Vector2(975, 210), 200, 200);
            Hand.Initialize(ScreenManager.Kinect);
            newGame.Clicked += new Button.ClickedEventHandler(newGame_Clicked);
            instructions.Clicked += new Button.ClickedEventHandler(instructions_Clicked);
            highscores.Clicked += new Button.ClickedEventHandler(highscores_Clicked);
            exit.Clicked += new Button.ClickedEventHandler(exit_Clicked);

            base.Initialize();
        }

        void exit_Clicked(object sender, System.EventArgs a)
        {
            this.ScreenManager.Game.Exit();
           
        }

        void highscores_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
            ScreenManager.AddScreen(new HighScoresScreen());
        }

        void instructions_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
            InstructionsScreen IScreen = new InstructionsScreen();
            IScreen.SetText("   The goal of this game is to get as far as you can aswell as getting the highest score possible. Healthy foods such as Strawberries, Tomatoes and Carrots increase your immunity and your score. " +
                "Avoid Unhealthy items like Hamburgers, Fries, Pizza and Viruses by jumping over them or bending to slide beneath them since they decrease your immunity and score. You can step to the right when you pass by a Gym building to exercise and restore your immunity. There are 2 exercises in the Gym, Dumbbell exercise and a running exercise. " +
                "The bar on the top left represents your immunity, and the avatar on the top right of the screen represents your distance from the Kinect. Green is the optimum distance. " +
                "\n                                                         Have fun ! :)");

            ScreenManager.AddScreen(IScreen);
        }

        void newGame_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
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
            Hand.LoadContent(Content);
            menu = new Sprite(Content.Load<Texture2D>("Textures//menu"), new Rectangle(0, 0, 1280, 720));           
            newGameLabel = new Sprite(Content.Load<Texture2D>("Textures//new_label"), new Rectangle(70, 430, 240, 50));
            instructionsLabel = new Sprite(Content.Load<Texture2D>("Textures//instructions_label"), new Rectangle(375, 430, 240, 50));
            scoreslabel = new Sprite(Content.Load<Texture2D>("Textures//scores_label"), new Rectangle(665, 430, 240, 50));
            exitLabel = new Sprite(Content.Load<Texture2D>("Textures//exit_label"), new Rectangle(960, 430, 240, 50));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Hand.Update(gameTime);
            newGame.Update(gameTime);
            instructions.Update(gameTime);
            highscores.Update(gameTime);
            exit.Update(gameTime);
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
            Hand.Draw(sprite);
            sprite.End();            
            newGameLabel.Draw(spriteBatch);
            instructionsLabel.Draw(spriteBatch);
            scoreslabel.Draw(spriteBatch);
            exitLabel.Draw(spriteBatch);

            base.Draw(gameTime);
        }

    }
}
