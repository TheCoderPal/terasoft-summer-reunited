using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.UI;
using Microsoft.Xna.Framework.Input;
using Game.Screens;
namespace Game
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;
        //Score score; //Tamer Test

        public Kinect.Kinect Kinect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            Kinect = new Kinect.Kinect(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            screenManager = new ScreenManager(this, Kinect);
            Components.Add(screenManager);

        }

        protected override void Initialize()
        {
            //initializations
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
           // screenManager.AddScreen(new MainScreen());
            screenManager.AddScreen(new MainScreen());
          //  score.LoadContent(Content);//Tamer Test
            
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //drawings
           // bar.Draw(spriteBatch); //Tamer Test
           // score.Draw(spriteBatch); //Tamer Test
            base.Draw(gameTime); 
        }
    }
}
