using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game.UI;
namespace Game
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Bar bar;//Tamer Test
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
          //  bar = new Bar(100, 20, 30, 300, 30);  //Tamer Test
        }

        protected override void Initialize()
        {
            //initializations
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
          //  bar.LoadContent(Content); //Tamer Test

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //updates
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //drawings
           // bar.Draw(spriteBatch); //Tamer Test
            base.Draw(gameTime); 
        }
    }
}
