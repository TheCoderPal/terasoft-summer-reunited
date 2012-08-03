using Microsoft.Xna.Framework;

namespace Game.UI
{
    #region ScreenState
    /// <summary>
    /// Represents the screen states.
    /// </summary>
    public enum ScreenState
    {
        Active,
        Frozen,
        Hidden
    }
    #endregion

    /// <summary>
    /// This class represents a screen.
    /// </summary>
    public abstract class GameScreen
    {
        public static int frameNumber;
        public UserAvatar userAvatar;
        public bool IsFrozen
        {
            get { 
                return screenState == ScreenState.Frozen;
            }
        }

        private ScreenState screenState;
        public ScreenState ScreenState
        {
            get { return screenState; }
            set { screenState = value; }
        }
        private ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            set { screenManager = value; }
        }
        
        public bool IsActive
        {
            get
            {
                return screenState == ScreenState.Active;
            }
        }
        public bool showAvatar=true;

        /// <summary>
        /// LoadContent will be called only once before drawing and it's the place to load
        /// all of your content.
        /// </summary>
        public virtual void LoadContent() {
            if (showAvatar)
            {
             //   userAvatar = new UserAvatar(new Game.Kinect.Kinect(), ScreenManager.Game.Content, ScreenManager.GraphicsDevice, ScreenManager.SpriteBatch);
                userAvatar.LoadContent();
            }
  
        }
        /// <summary>
        /// Initializes the GameScreen.
        /// </summary
        public virtual void Initialize() {}

        /// <summary>
        /// Unloads the content of GameScreen.
        /// </summary>
        public virtual void UnloadContent() { }

        public virtual void Update(GameTime gameTime, bool covered)
        {
            if (IsFrozen)
                return;
        }
        /// <summary>
        /// Allows the game screen to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime) {
            if (showAvatar)
            {
                    userAvatar.Update(gameTime);  
            }
        }
        
        /// <summary>
        /// Removes the current screen.
        /// </summary>
        public virtual void Remove()
        {
            screenManager.RemoveScreen(this);
        }    

        /// <summary>
        /// This is called when the game screen should draw itself.
        /// </summary>
        public virtual void Draw(GameTime gameTime)
        {
          if(showAvatar)
           userAvatar.Draw(gameTime);
        }
        
        public void FreezeScreen()
        {
            screenState = ScreenState.Frozen;
        }

        public void UnfreezeScreen()
        {
            screenState = ScreenState.Active;
        }
        
    
    }
}