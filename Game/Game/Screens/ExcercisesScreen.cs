using Game.UI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Screens
{
    public class ExcercisesScreen : GameScreen
    {
        Button dumbbell;
        Button treadmill;
        Sprite background;
        SpriteBatch spriteBatch;
        ContentManager Content;
        Game.Kinect.Kinect kinect;
        HandCursor Hand;

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public ExcercisesScreen()
        {

        }

        public override void Initialize()
        {
            showAvatar = true;
            treadmill = new Button();
            dumbbell = new Button();
            Hand = new HandCursor();
            kinect = ScreenManager.Kinect;
            dumbbell.Initialize("Buttons//dumbbell", kinect, new Vector2(140, 290));
            treadmill.Initialize("Buttons//treadmill", kinect, new Vector2(800, 250));
            Hand.Initialize(ScreenManager.Kinect);
            dumbbell.Clicked += new Button.ClickedEventHandler(dumbbell_Clicked);
            treadmill.Clicked += new Button.ClickedEventHandler(treadmill_Clicked);
            base.Initialize();
        }

        void treadmill_Clicked(object sender, System.EventArgs a)
        {
            throw new System.NotImplementedException();
        }

        void dumbbell_Clicked(object sender, System.EventArgs a)
        {
            throw new System.NotImplementedException();
        }

        public override void LoadContent()
        {
            Content = ScreenManager.Game.Content;
            spriteBatch = ScreenManager.SpriteBatch;
            dumbbell.LoadContent(Content);
            treadmill.LoadContent(Content);
            Hand.LoadContent(Content);
            background = new Sprite(Content.Load<Texture2D>("Textures//choosing_frame"), new Rectangle(0, 0, 1280, 720));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Hand.Update(gameTime);
            dumbbell.Update(gameTime);
            treadmill.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            background.Draw(spriteBatch);
            SpriteBatch sprite = spriteBatch;
            sprite.Begin();
            dumbbell.Draw(spriteBatch);
            treadmill.Draw(spriteBatch);
            Hand.Draw(spriteBatch);
            sprite.End();
            base.Draw(gameTime);
        }
    }
}
