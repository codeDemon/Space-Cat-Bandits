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

namespace Space_Cat_Bandits
{
    //Main Class
    public class Main : Microsoft.Xna.Framework.Game
    {
        //Declare Instance Variables ----------------------------------------------------------------------------
        private GraphicsDeviceManager z_graphics;
        private SpriteBatch z_spriteBatch;
        private ScrollingBackground z_backgroundImage1;
        private ScrollingBackground z_backgroundImage2;
        private Rectangle z_viewportRec;
        //Variables for GameObjects
        private PlayerShip z_playerShip;
        //Variables For Music
        private Song z_beautifulDarkness;
        private bool z_songStart = false;
        
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

            //Create the Player's ship image
            this.z_playerShip = new PlayerShip(Content.Load<Texture2D>("Images\\ship2"));
            //Set the starting position for player's ship
            this.z_playerShip.setPosition(new Vector2(z_graphics.GraphicsDevice.Viewport.Width / 2,
                                                    z_graphics.GraphicsDevice.Viewport.Height - 80));

            //Set the player alive
            this.z_playerShip.setIsAlive(true);

            //Load the Music
            this.z_beautifulDarkness = Content.Load<Song>("Audio\\Beautiful_Darkness");
            MediaPlayer.IsRepeating = true;
        }


        //Unload Content Method ----------------------------------------------------------------------------------
        protected override void UnloadContent()
        {
        }


        //Main Update Method -------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            //Play the Song
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



            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Local Variables for 360 controller
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            //Input for moving the player's ship on the xbox360

            this.z_playerShip.setVelocity(new Vector2(gamePadState.ThumbSticks.Left.X * 0.07f,
                                                    gamePadState.ThumbSticks.Left.Y * 0.07f));
            this.z_playerShip.upDatePosition();
#if !XBOX
            //Local Variables for Keyboard
            KeyboardState keyboardState = Keyboard.GetState();

            //Input for moving the player's ship on the keyboard
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (this.z_playerShip.getPosition().X > 0)
                    this.z_playerShip.setPosition(new Vector2(this.z_playerShip.getPosition().X - 3,
                                                            this.z_playerShip.getPosition().Y));
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                if (this.z_playerShip.getPosition().X + this.z_playerShip.getSprite().Width
                    < z_graphics.GraphicsDevice.Viewport.Width)
                    this.z_playerShip.setPosition(new Vector2(this.z_playerShip.getPosition().X + 3,
                                                            this.z_playerShip.getPosition().Y));
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                if (this.z_playerShip.getPosition().Y > 0)
                    this.z_playerShip.setPosition(new Vector2(this.z_playerShip.getPosition().X,
                                                            this.z_playerShip.getPosition().Y - 3));
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                if (this.z_playerShip.getPosition().Y + this.z_playerShip.getSprite().Height <
                    z_graphics.GraphicsDevice.Viewport.Height)
                    this.z_playerShip.setPosition(new Vector2(this.z_playerShip.getPosition().X,
                                                            this.z_playerShip.getPosition().Y + 3));
            }
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
            this.z_spriteBatch.Draw(this.z_backgroundImage1.getSprite(), this.z_backgroundImage1.getPosition(),null,
                Color.White, 0, new Vector2(0, 0), this.z_backgroundImage1.Scale(this.z_viewportRec)
                ,SpriteEffects.None,1);                       
            
            this.z_spriteBatch.Draw(this.z_backgroundImage2.getSprite(), this.z_backgroundImage2.getPosition(), null,
                Color.White, 0, new Vector2(0, 0), this.z_backgroundImage2.Scale(this.z_viewportRec)
                , SpriteEffects.None, 1);
            
            //Draw Player Ship
            this.z_spriteBatch.Draw(this.z_playerShip.getSprite(),
                                  this.z_playerShip.getPosition(),
                                  Color.White);



            this.z_spriteBatch.End();

            base.Draw(gameTime);
        }



        //Other Methods add here ---------------------------------------------------------------------------------





    }
}
