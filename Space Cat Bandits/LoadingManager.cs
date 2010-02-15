using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Text;

namespace Space_Cat_Bandits
{
    class LoadingManager
    {
        //The different states a level could be in
        public enum LevelStates
        {
            Running,
            Paused,
            Froze
        }
        //Instance Variables
        private bool z_initialLoadFinished;
        private bool z_isLoading;
        private Texture2D z_loadingScreen;
        private Texture2D z_logo;
        private float z_delay;
        private ContentManager z_content;

        //Screens and Menus
        private TitleScreen z_titleScreen;

        //Constructor
        public LoadingManager(Texture2D loadingScreen,Texture2D logo, ContentManager content)
        {
            this.z_initialLoadFinished = false;
            this.z_isLoading = false;
            this.z_loadingScreen = loadingScreen;
            this.z_logo = logo;
            this.z_delay = 0f;
            this.z_content = content;
        }


        //Accessors
        public bool getInitialLoadFinished()
        {
            return this.z_initialLoadFinished;
        }
        public bool getIsLoading()
        {
            return this.z_isLoading;
        }
        public Texture2D getLoadingScreen()
        {
            return this.z_loadingScreen;
        }
        public Texture2D getLogo()
        {
            return this.z_logo;
        }
        public TitleScreen getTitleScreen()
        {
            return this.z_titleScreen;
        }

        //Mutators
        public void setInitialLoad(bool restartInitialLoad)
        {
            this.z_initialLoadFinished = restartInitialLoad;
        }
        public void setIsLoading(bool isLoading)
        {
            this.z_isLoading = isLoading;
        }
        public void setLoadingScreen(Texture2D newScreen)
        {
            this.z_loadingScreen = newScreen;
        }
        public void setLogo(Texture2D newLogo)
        {
            this.z_logo = newLogo;
        }

        //Perform the first loading for the game
        public void InitialLoad(TitleScreen titleScreen)
        {
            //Load all necessary content for all Screens and Menus
            /*
            this.z_titleScreen = new TitleScreen(this.z_content.Load<Texture2D>("Screens\\LogoScreen"),
                                          this.z_content.Load<Texture2D>("Screens\\TitleOptions"),
                                          this.z_content.Load<Texture2D>("Screens\\ArrowSelection"));
             * */

            
            this.z_initialLoadFinished = true;
        }

        //Load all the content for a level
        public void LoadLevel(ILevel nextLevel)
        {
            this.z_isLoading = true;
            nextLevel.loadAssets();
            this.z_isLoading = false;
        }

        //Unload all the content for a previous level
        public void UnLoadLevel(ILevel lastLevel)
        {
            this.z_isLoading = true;
            lastLevel.unLoadAssets();
            this.z_isLoading = false;
        }

        //Update Method
        public void Update(GameTime gameTime)
        {
            this.z_delay += gameTime.ElapsedGameTime.Milliseconds;
            //Just to pretend like something were loading
            if (this.z_delay > 5000)
                this.z_initialLoadFinished = true;
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.z_logo, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(this.z_loadingScreen, new Vector2(0,300), Color.White);
        }


    }
}
