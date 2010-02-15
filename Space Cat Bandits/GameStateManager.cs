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
    class GameStateManager
    {
        /*
         * Welcome to the GameStateManager.
         * This class will control how the game is played
         * by managing all the different states the game could be in.
         * 
         * 
         * */

        //Enum for all the different states
        public enum GameState
        {
            LoadingScreen,
            TitleMenu,
            OptionsMenu,
            ProfileScreen,
            MainMenu,
            MissionMenu,
            PlayingGame,
            GameOverScreen
            //Maybe more
        }


        //Instance Variables
        private GameState z_currentGameState;
        private GameState z_previousGameState; //For the back button
        private TitleScreen z_titleScreen;


        //Constructor
        public GameStateManager( TitleScreen titleScreen)
        {
            this.z_currentGameState = GameState.LoadingScreen;
            this.z_previousGameState = this.z_currentGameState;
            this.z_titleScreen = titleScreen;
            
        }


        //Accessors
        public GameState getCurrentGameState()
        {
            return this.z_currentGameState;
        }
        public GameState getPreviousGameState()
        {
            return this.z_previousGameState;
        }
        public TitleScreen getTitleScreen()
        {
            return this.z_titleScreen;
        }

        //Mutators
        public void setCurrentGameState(GameState newState)
        {
            this.z_currentGameState = newState;
        }
        public void setPreviousGameState(GameState newState)
        {
            this.z_previousGameState = newState;
        }
        public void setTitleScreen(TitleScreen newScreen)
        {
            this.z_titleScreen = newScreen;
        }


        //Some Important Methods for the states
        public void Update(KeyboardState currentKeyState, KeyboardState previousKeyState)
        {
            //States will change into other states based on input
            this.z_titleScreen.update(currentKeyState, previousKeyState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //based on the current state, draw the currect screen/menu
            //since currently only one
            this.z_titleScreen.Draw(spriteBatch);
        }


    }
}
