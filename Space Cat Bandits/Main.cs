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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D backgroundImage;
        private Rectangle viewportRec;
        //Variables for GameObjects
        private PlayerShip playerShip;
        //Variables For Music
        private Song beautifulDarkness;
        private bool songStart = false;

        //Constructor -------------------------------------------------------------------------------------------
        public Main()
        {
            this.graphics = new GraphicsDeviceManager(this);
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
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            //Load the background Image
            this.backgroundImage = Content.Load<Texture2D>("Textures\\spaceBackground");
            this.viewportRec = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width,
                                                graphics.GraphicsDevice.Viewport.Height);
            //Create the Player's ship image
            this.playerShip = new PlayerShip(Content.Load<Texture2D>("Images\\ship2"));
            //Set the starting position for player's ship
            this.playerShip.setPosition(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                                                    graphics.GraphicsDevice.Viewport.Height - 80));
            //Load the Music
            this.beautifulDarkness = Content.Load<Song>("Audio\\Beautiful_Darkness");
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
            if (!this.songStart)
            {
                MediaPlayer.Play(this.beautifulDarkness);
                MediaPlayer.Volume = 0.7f;
                this.songStart = true;
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Local Variables for 360 controller
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            //Input for moving the player's ship on the xbox360

            this.playerShip.setVelocity(new Vector2(gamePadState.ThumbSticks.Left.X * 0.07f,
                                                    gamePadState.ThumbSticks.Left.Y * 0.07f));
            this.playerShip.upDatePosition();
#if !XBOX
            //Local Variables for Keyboard
            KeyboardState keyboardState = Keyboard.GetState();

            //Input for moving the player's ship on the keyboard
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (this.playerShip.getPosition().X > 0)
                    this.playerShip.setPosition(new Vector2(this.playerShip.getPosition().X - 3,
                                                            this.playerShip.getPosition().Y));
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                if (this.playerShip.getPosition().X + this.playerShip.getSprite().Width
                    < graphics.GraphicsDevice.Viewport.Width)
                    this.playerShip.setPosition(new Vector2(this.playerShip.getPosition().X + 3,
                                                            this.playerShip.getPosition().Y));
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                if (this.playerShip.getPosition().Y > 0)
                    this.playerShip.setPosition(new Vector2(this.playerShip.getPosition().X,
                                                            this.playerShip.getPosition().Y - 3));
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                if (this.playerShip.getPosition().Y + this.playerShip.getSprite().Height <
                    graphics.GraphicsDevice.Viewport.Height)
                    this.playerShip.setPosition(new Vector2(this.playerShip.getPosition().X,
                                                            this.playerShip.getPosition().Y + 3));
            }
#endif


            base.Update(gameTime);
        }


        //Draw Method --------------------------------------------------------------------------------------------
        protected override void Draw(GameTime gameTime)
        {
            //Clear all images
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            //Draw Background image
            this.spriteBatch.Draw(this.backgroundImage, this.viewportRec, Color.White);
            //Draw Player Ship
            this.spriteBatch.Draw(this.playerShip.getSprite(),
                                  this.playerShip.getPosition(),
                                  Color.White);



            this.spriteBatch.End();

            base.Draw(gameTime);
        }



        //Other Methods add here ---------------------------------------------------------------------------------





    }
}
