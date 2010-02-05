#region using
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
#endregion


namespace Space_Cat_Bandits
{
    //Main Class
    public class Main : Microsoft.Xna.Framework.Game
    {
        #region Variables
        //Declare Instance Variables ----------------------------------------------------------------------------
        private GraphicsDeviceManager z_graphics;
        private SpriteBatch z_spriteBatch;
        private ScrollingBackground z_backgroundImage1;
        private ScrollingBackground z_backgroundImage2;
        private Rectangle z_viewportRec;
        private float z_gameTimer;
        private float z_accelTimerX = 0;
        private float z_accelTimerY = 0;
        private float z_interval1 = 15;
        private GameObject z_achivementFail;
        private bool z_achivementFailUnlocked = false;
        private ContentManager z_contentManager;
        private GamePadState z_previousGamePadState = GamePad.GetState(PlayerIndex.One);
        private KeyboardState z_previousKeyboardState = Keyboard.GetState();
        private Vector2 z_startingPosition;
        //The Asteroid Manager
        private AsteroidManager z_asteroidManager;
        //The Missle Manager
        private MissleManager z_missleManager;
        //The Enemy Manager
        private EnemyManager z_enemyManager;
        //Variables for GameObjects
        private PlayerShip z_playerShip;
        //Variables For Music
        private Song z_beautifulDarkness;
        private bool z_songStart = false;
        private SoundEffect z_achievementSound;
        //Variables For Text Fonts
        private SpriteFont z_timerFont;
        private SpriteFont z_livesFont;
        #endregion



        //Constructor -------------------------------------------------------------------------------------------
        public Main()
        {
            this.z_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        //Initialize Method -------------------------------------------------------------------------------------
        protected override void Initialize()
        {
            base.Initialize();
        }


        //Load Content Method -----------------------------------------------------------------------------------
        protected override void LoadContent()
        {
            //Set the contentManger
            this.z_contentManager = new ContentManager(Services);

            // Create a new SpriteBatch, which can be used to draw textures.
            this.z_spriteBatch = new SpriteBatch(GraphicsDevice);

            //Set the viewPortRec
            this.z_viewportRec = new Rectangle(0, 0, z_graphics.GraphicsDevice.Viewport.Width,
                                                z_graphics.GraphicsDevice.Viewport.Height);
            //Load the background Images
            this.z_backgroundImage1 = new ScrollingBackground(Content.Load<Texture2D>("Textures\\spaceBackground"));
            this.z_backgroundImage2 = new ScrollingBackground(Content.Load<Texture2D>("Textures\\spaceBackground"));

            //Set the positions for the background Images
            this.z_backgroundImage1.setPosition(new Vector2(0f, 0f));
            this.z_backgroundImage2.setPosition(new Vector2(0f, 0f - this.z_viewportRec.Height));
            
            //Turn the background Images alive
            this.z_backgroundImage1.setIsAlive(true);
            this.z_backgroundImage2.setIsAlive(true);

            //Set the starting position for player's ship
            this.z_startingPosition = new Vector2(this.z_viewportRec.Center.X,
                                                    z_graphics.GraphicsDevice.Viewport.Height - 80);

            //Create the Player's ship image
            this.z_playerShip = new PlayerShip(Content.Load<Texture2D>("Images\\ship2"), this.z_startingPosition);           

            //Set the player alive
            this.z_playerShip.setIsAlive(true);

            //Load the Music
            this.z_beautifulDarkness = Content.Load<Song>("Audio\\Music\\OutSideMyComfortZone");
            MediaPlayer.IsRepeating = true;

            //Load Fonts
            this.z_timerFont = Content.Load<SpriteFont>("Fonts\\TimerFont");
            this.z_livesFont = Content.Load<SpriteFont>("Fonts\\LivesFont");

            //Load Achivement Stuff
            this.z_achivementFail = new GameObject(Content.Load<Texture2D>("Images\\AchievementFailed"));
            this.z_achivementFail.setPosition(new Vector2((this.z_viewportRec.Width/2)-(this.z_achivementFail.getSprite().Width/2),
                                                            this.z_viewportRec.Height-100));
            this.z_achievementSound = Content.Load<SoundEffect>("Audio\\SoundFX\\AchievementSound");

            //Load the Settings for the asteroidManager
            this.z_asteroidManager = new AsteroidManager(AsteroidManager.AsteroidManagerState.Heavy, this.z_viewportRec,
                                                         this.z_contentManager, this.z_spriteBatch);

            //Load the Settings for the MissleManager
            this.z_missleManager = new MissleManager(this.z_viewportRec, this.z_contentManager,
                                                     Content.Load<SoundEffect>("Audio\\SoundFX\\LaserPellet"));

            //Load the Settings for the EnemyManager
            this.z_enemyManager = new EnemyManager(this.z_contentManager, this.z_spriteBatch, this.z_viewportRec);
            
        }


        //Unload Content Method ----------------------------------------------------------------------------------
        protected override void UnloadContent()
        {
        }


        //Main Update Method -------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            //Update The game Timer
            /*
             * if(game is not paused)
             * //->Then update the gameTime
             * //For changing events will be something like
             * if(z_gameTimer >= some interval)
             * {
             *      //Do Some Stuff
             *      //Unload current level
             *      //Play a talk scene
             *      //Load next level
             *      z_gameTimer = 0.0f;
             * }
             * */

            //Update GameTimer
            this.z_gameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Check Achivements
            if (this.z_gameTimer > this.z_interval1 && !this.z_achivementFailUnlocked)
            {
                this.z_achivementFail.setIsAlive(true);
                this.z_achievementSound.Play();
                this.z_achivementFailUnlocked = true;
            }
            if (this.z_gameTimer > 21)
                this.z_achivementFail.setIsAlive(false);


            //Play Background Music
            if (!this.z_songStart)
            {
                MediaPlayer.Play(this.z_beautifulDarkness);
                MediaPlayer.Volume = 0.7f;
                this.z_songStart = true;
            }

            //Update the Scrolling Background Images
            if (this.z_backgroundImage1.getPosition().Y >= this.z_viewportRec.Height)
            {
                //Then it is off of the stage, reset it back at the top
                this.z_backgroundImage1.setPosition(new Vector2(0, 0.0f - this.z_viewportRec.Height));
                //this.z_backgroundImage1.upDatePosition();
            }
            if (this.z_backgroundImage2.getPosition().Y >= this.z_viewportRec.Height)
            {
                //Then it is off of the stage, reset it back at the top
                this.z_backgroundImage2.setPosition(new Vector2(0, 0.0f - this.z_viewportRec.Height));
                //this.z_backgroundImage2.upDatePosition();
            }
            //The order of the upDatePosition Matters I think
            if (this.z_backgroundImage1.getPosition().Y > this.z_backgroundImage2.getPosition().Y)
            {
                this.z_backgroundImage2.upDatePosition();
                this.z_backgroundImage1.upDatePosition();
            }
            else
            {
                this.z_backgroundImage1.upDatePosition();
                this.z_backgroundImage2.upDatePosition();
            }

            //Update Asteroid Manager
            this.z_asteroidManager.updateAsteroids(this.z_playerShip);

            //########### Input for Controls and Options ########################################


            //For Xbox controller 1
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                //Local Variables for 360 controller
                GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                //Input for moving the player's ship on the xbox360
                //Acceleration needs to be worked on for xbox controller
                this.z_playerShip.setVelocity(new Vector2(gamePadState.ThumbSticks.Left.X * 0.07f,
                                                        gamePadState.ThumbSticks.Left.Y * 0.07f));
                this.z_playerShip.upDatePosition();

                //Update Missle Manager
                this.z_missleManager.MissleManagerUpdateFriendlyGamepad(gamePadState, this.z_previousGamePadState,
                                                                        this.z_playerShip, this.z_spriteBatch);


                //At the end of Xbox Controller Updates
                this.z_previousGamePadState = gamePadState;
            }


            //For Keyboard
#if !XBOX
            //Local Variables for Keyboard
            KeyboardState keyboardState = Keyboard.GetState();

            #region ShipControls
            //Input for accelerating the ship --------------------------------------------------------
            //Move Left
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    if (z_accelTimerX < 100)
                        z_accelTimerX += (float)gameTime.ElapsedGameTime.Milliseconds;
                    else
                        if (this.z_playerShip.getPosition().X > 1)
                        {
                            this.z_playerShip.accelerateLeft();
                            z_accelTimerX = 0;
                        }
                }

            //Move Right
            if ((keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))) 
            {
                if (z_accelTimerX < 100)
                    z_accelTimerX += (float)gameTime.ElapsedGameTime.Milliseconds;
                else
                    if (this.z_playerShip.getPosition().X + this.z_playerShip.getSprite().Width <
                        this.z_graphics.GraphicsDevice.Viewport.Width - 1)
                    {
                        this.z_playerShip.accelerateRight();
                        z_accelTimerX = 0;
                    }
            }

            //Move Up
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                if (z_accelTimerY < 100)
                    z_accelTimerY += (float)gameTime.ElapsedGameTime.Milliseconds;
                else
                    if (this.z_playerShip.getPosition().Y > 1)
                    {
                        this.z_playerShip.accelerateUp();
                        z_accelTimerY = 0;
                    }
            }
            //Move Down
            else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                if (z_accelTimerY < 100)
                    z_accelTimerY += (float)gameTime.ElapsedGameTime.Milliseconds;
                else
                    if (this.z_playerShip.getPosition().Y + this.z_playerShip.getSprite().Height < 
                        this.z_graphics.GraphicsDevice.Viewport.Height - 1)
                    {
                        this.z_playerShip.accelerateDown();
                        z_accelTimerY = 0;
                    }
            }


            //Don't Move in the X direction when opposite keys are pressed
            if (((keyboardState.IsKeyDown(Keys.Right) && (keyboardState.IsKeyDown(Keys.Left)) ||
                ((keyboardState.IsKeyDown(Keys.Right) && (keyboardState.IsKeyDown(Keys.A)))) ||
                ((keyboardState.IsKeyDown(Keys.Left) && (keyboardState.IsKeyDown(Keys.D)))) ||
                ((keyboardState.IsKeyDown(Keys.A) && (keyboardState.IsKeyDown(Keys.D)))))))
            {
                this.z_playerShip.setVelocity(new Vector2(0, this.z_playerShip.getVelocity().Y));
                this.z_playerShip.setIsSlowingDownX(false);
            }

            //Don't Move in the Y direction when opposite keys are pressed
            if (((keyboardState.IsKeyDown(Keys.Up) && (keyboardState.IsKeyDown(Keys.Down)) ||
                ((keyboardState.IsKeyDown(Keys.Up) && (keyboardState.IsKeyDown(Keys.S)))) ||
                ((keyboardState.IsKeyDown(Keys.Down) && (keyboardState.IsKeyDown(Keys.W)))) ||
                ((keyboardState.IsKeyDown(Keys.W) && (keyboardState.IsKeyDown(Keys.S)))))))
            {
                this.z_playerShip.setVelocity(new Vector2(this.z_playerShip.getVelocity().X, 0));
                this.z_playerShip.setIsSlowingDownY(false);
            }


            //Check if a key was let go for deAccelerating to a stop -------------------------------------
            if ((this.z_previousKeyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyUp(Keys.Left)) ||
                (this.z_previousKeyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.A)) &&
                (!(keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyUp(Keys.A))) &&
                (!(keyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.Left))))
                    this.z_playerShip.setIsSlowingDownX(true);

            else if ((this.z_previousKeyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyUp(Keys.Right)) ||
                (this.z_previousKeyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyUp(Keys.D)) &&
                (!(keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyUp(Keys.D))) &&
                (!(keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyUp(Keys.Right))))
                    this.z_playerShip.setIsSlowingDownX(true);

            if ((this.z_previousKeyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyUp(Keys.Up)) ||
                (this.z_previousKeyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyUp(Keys.W)) &&
                (!(keyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyUp(Keys.W))) &&
                (!(keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyUp(Keys.Up))))
                    this.z_playerShip.setIsSlowingDownY(true);
           
            else if ((this.z_previousKeyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyUp(Keys.Down)) ||
                (this.z_previousKeyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.S)) &&
                (!(keyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyUp(Keys.S))) &&
                (!(keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.Down))))
                    this.z_playerShip.setIsSlowingDownY(true);
            #endregion



            //Perform the Update on The Ship
            this.z_playerShip.playerShipUpdate(gameTime, this.z_viewportRec);


            //Update Missle Manager
            this.z_missleManager.MissleManagerUpdateFriendlyKeyboard(keyboardState, this.z_previousKeyboardState,
                                                                     this.z_playerShip, this.z_spriteBatch);

            //Update the Enemy Manager
            this.z_enemyManager.mainUpdate(gameTime);


            //End of Keyboard Updates
            this.z_previousKeyboardState = keyboardState;
#endif


            base.Update(gameTime);
        }


        //Draw Method --------------------------------------------------------------------------------------------
        protected override void Draw(GameTime gameTime)
        {
            //Clear all images
            GraphicsDevice.Clear(Color.Black);
            this.z_spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            //Draw Background images
            if(this.z_backgroundImage1.getIsAlive())
                this.z_spriteBatch.Draw(this.z_backgroundImage1.getSprite(), this.z_backgroundImage1.getPosition(),null,
                    Color.White, 0, new Vector2(0, 0), this.z_backgroundImage1.Scale(this.z_viewportRec)
                    ,SpriteEffects.None,1);                       
            if(this.z_backgroundImage2.getIsAlive())
                this.z_spriteBatch.Draw(this.z_backgroundImage2.getSprite(), this.z_backgroundImage2.getPosition(), null,
                    Color.White, 0, new Vector2(0, 0), this.z_backgroundImage2.Scale(this.z_viewportRec)
                    ,SpriteEffects.None, 1);

            //Draw Enemies from EnemyManager
            this.z_enemyManager.draw();

            //Draw any asteroids from AsteroidManager
            this.z_asteroidManager.drawAsteroids();
            
            //Draw Player Ship
            this.z_playerShip.draw(this.z_spriteBatch, gameTime);

            //Draw Fonts
            this.z_spriteBatch.DrawString(this.z_timerFont, "Time: " + Math.Round(z_gameTimer,2),
                                          new Vector2(.01f * this.z_viewportRec.Width, .01f * this.z_viewportRec.Height),
                                          Color.Yellow);
            this.z_spriteBatch.DrawString(this.z_livesFont, "Lives: " + this.z_playerShip.getLives(),
                                          new Vector2(.01f * this.z_viewportRec.Width, .05f * this.z_viewportRec.Height),
                                          Color.White);

            //Draw any achivements
            if (this.z_achivementFail.getIsAlive())
                this.z_spriteBatch.Draw(this.z_achivementFail.getSprite(), this.z_achivementFail.getPosition(), Color.White);

            //Draw Missles
            this.z_missleManager.MissleManagerDrawAllMissles();

            



            //Close Sprite Batch
            this.z_spriteBatch.End();

            base.Draw(gameTime);
        }



        //Other Methods add here ---------------------------------------------------------------------------------





    }
}
