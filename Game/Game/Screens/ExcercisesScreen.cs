using Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Screens
{
    public class ExcercisesScreen : GameScreen
    {
        private Button dumbbell, treadmill;
        private Sprite background;
        private SpriteBatch spriteBatch;
        private ContentManager Content;
        private Game.Kinect.Kinect kinect;
        private HandCursor Hand;
        private PlayScreen playScreen;

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public ExcercisesScreen(PlayScreen playScreen)
        {
            this.playScreen = playScreen;
        }

        public override void Initialize()
        {
            showAvatar = true;
            treadmill = new Button();
            dumbbell = new Button();
            Hand = new HandCursor();
            kinect = ScreenManager.Kinect;
            Hand.Initialize(ScreenManager.Kinect);
            dumbbell.Clicked += new Button.ClickedEventHandler(dumbbell_Clicked);
            treadmill.Clicked += new Button.ClickedEventHandler(treadmill_Clicked);
            base.Initialize();
        }

        void treadmill_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
            ScreenManager.AddScreen(new TreadmillScreen(playScreen));
        }

        void dumbbell_Clicked(object sender, System.EventArgs a)
        {
            this.Remove();
            ScreenManager.AddScreen(new DumbbellScreen(playScreen));
        }

        public override void LoadContent()
        {
            Content = ScreenManager.Game.Content;
            spriteBatch = ScreenManager.SpriteBatch;
            dumbbell.Initialize("Buttons//dumbbell", kinect, new Vector2(140, 290));
            treadmill.Initialize("Buttons//treadmill", kinect, new Vector2(800, 250));
            dumbbell.LoadContent(Content);
            treadmill.LoadContent(Content);
            Hand.LoadContent(Content);
            background = new Sprite(Content.Load<Texture2D>("Textures//choosing_frame"), new Rectangle(0, 0, 1280, 720),Content);
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
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            dumbbell.Draw(spriteBatch);
            treadmill.Draw(spriteBatch);
            Hand.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
