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
        //Bar bar;//Tamer Test
        //Score score; //Tamer Test
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

          //  bar = new Bar(100, 20, 30, 300, 30);  //Tamer Test
          // score = new Score(100, 20, Color.WhiteSmoke); //Tamer Test
        }

        protected override void Initialize()
        {
            //initializations
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager.AddScreen(new MainScreen());
            //screenManager.AddScreen(AyScreen());
          //  bar.LoadContent(Content); //Tamer Test
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
