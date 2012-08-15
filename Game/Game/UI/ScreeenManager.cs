using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game.Text;
using Microsoft.Kinect;

namespace Game.UI
{
    public class ScreenManager : DrawableGameComponent
    {
        private List<GameScreen> screens = new List<GameScreen>();
        private List<GameScreen> screensToUpdate = new List<GameScreen>();
        private SpriteBatch spriteBatch;
        public int framesCount;
        public Skeleton newSkeleton;
        public int FramesCount
        {
            get { return framesCount; }
            set { framesCount = value; }
        }

        public Kinect.Kinect Kinect;

        /// <summary>
        /// Returns the sprite batch object.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        /// <summary>
        /// Creates a new instance of ScreenManager.
        /// </summary>
        public ScreenManager(Microsoft.Xna.Framework.Game game, Kinect.Kinect kinect)
            : base(game)
        {
            this.Kinect = kinect;
            base.Initialize();

        }

        /// <summary>
        /// Loads the content of the screens managed by the screenManager.
        /// </summary>
        protected override void LoadContent()
        {
            newSkeleton = Kinect.trackedSkeleton;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (GameScreen screen in screens)
                screen.LoadContent();
        }

        /// <summary>
        /// Unloads the content of the screens managed by the screenManager.
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (GameScreen screen in screens)
                screen.UnloadContent();
        }
        /// <summary>
        /// Updates the screens managed by the screenManager.
        /// </summary>
        /// <param name="gameTime">Represents the time of the game.</param>
        public override void Update(GameTime gameTime)
        {
            screensToUpdate.Clear();
            foreach (GameScreen screen in screens)
            {
                if (!screen.IsFrozen)
                    screensToUpdate.Add(screen);
            }

            if (screensToUpdate.Count == 0)
                foreach (GameScreen screen in screens)
                {
                    screen.UnfreezeScreen();
                    screensToUpdate.Add(screen);
                }
            else
            {
                while (screensToUpdate.Count > 0)
                {
                    GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                    screensToUpdate.RemoveAt(screensToUpdate.Count - 1);
                    screen.Update(gameTime);
                }
            }
            framesCount++;
            newSkeleton = Kinect.trackedSkeleton;
            if (framesCount % 60 == 0 && Kinect.trackedSkeleton != null)
                setSkeletonJoints();
        }
        public void setSkeletonJoints()
        {
            Constants.oldSkeleton = newSkeleton;
            Constants.hipPosX = (int)newSkeleton.Joints[JointType.HipCenter].Position.X;
            Constants.hipPosY = (int)newSkeleton.Joints[JointType.HipCenter].Position.Y;
            Constants.rightHandPosZ = (int)newSkeleton.Joints[JointType.HandRight].Position.Z;
            Constants.rightElbowPosY = (int)newSkeleton.Joints[JointType.ElbowRight].Position.Y;
            Constants.facePosY = (int)newSkeleton.Joints[JointType.Head].Position.Y;
        }
        /// <summary>
        /// Updates the screens managed by the screenManager.
        /// </summary>
        /// <param name="gameTime">Represents the time of the game.</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }
        /// <summary>
        /// Adds a screen to the list of screens managed by the screenManager.
        /// </summary>
        /// <param name="screen">Represents the screen that should be managed by the screenManager.</param>
        public void AddScreen(GameScreen screen)
        {
            screen.ScreenManager = this;
            screen.Initialize();
            screen.LoadContent();
            screens.Add(screen);
        }

        /// <summary>
        /// Removes a screen from the list of screens managed by the screenManager.
        /// </summary>
        /// <param name="screen">Represents the screen that should be removed from the list
        /// of screens managed by the screenManager.</param>
        public void RemoveScreen(GameScreen screen)
        {
            screen.UnloadContent();
            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }

    }
}