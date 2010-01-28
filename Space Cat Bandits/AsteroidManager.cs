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
    class AsteroidManager
    {

        //States
        public enum AsteroidManagerState
        {
            None,
            Lite,
            Moderate,
            Heavy
        }


        //Instance Variables
        private AsteroidManagerState z_currentState;
        private Asteroid[] z_asteroidHolder;
        private float z_minAsteroidSpeed;
        private float z_maxAsteroidSpeed;
        private float z_maxStartingHeight;
        private float z_minStartingHeight;
        private float z_maxStartingWidth;
        private float z_minStartingWidth;
        private float z_maxRotationSpeed;
        private float z_minRotationSpeed;
        private int z_maxImage;
        private int z_minImage;
        private Random z_randomGenerator;
        private Rectangle z_viewPort;
        private ContentManager z_content;
        private SpriteBatch z_spriteBatch;


        //Constructor
        public AsteroidManager(AsteroidManagerState newState, Rectangle viewPort, ContentManager content, SpriteBatch spriteBatch)
        {
            //Define the current State
            this.z_currentState = newState;

            //Decide how many asteroids to put on screen
            this.adjustManagementSettings();

            //Initialize other Variables
            this.z_viewPort = viewPort;
            this.z_minAsteroidSpeed = 0.1f;
            this.z_maxAsteroidSpeed = 1.5f;
            this.z_minStartingHeight = -100;
            this.z_maxStartingHeight = -200;
            this.z_minStartingWidth = 0;
            this.z_maxStartingWidth = (float)this.z_viewPort.Width;
            this.z_minRotationSpeed = 0.01f;
            this.z_maxRotationSpeed = 0.05f;
            this.z_minImage = 1;
            this.z_maxImage = 5;
            this.z_randomGenerator = new Random();
            this.z_content = content;
            this.z_content.RootDirectory = "Content";
            this.z_spriteBatch = spriteBatch;

            //Start Populating Asteroids after all variables have been initialized
            this.populateAsteroids();
        }

        //Helper Method for determining the maximum number of Asteroids to put on the screen
        private void adjustManagementSettings()
        {
            //Based on the current State, Determine how many max Asteroids to draw
            //Must use a switch statement with enum types, (if does not work).
            switch (this.z_currentState)
            {
                case AsteroidManagerState.None:
                    {
                        this.z_asteroidHolder = new Asteroid[0];
                        break;
                    }
                case AsteroidManagerState.Lite:
                    {
                        this.z_asteroidHolder = new Asteroid[3];
                        break;
                    }
                case AsteroidManagerState.Moderate:
                    {
                        this.z_asteroidHolder = new Asteroid[5];
                        break;
                    }
                case AsteroidManagerState.Heavy:
                    {
                        this.z_asteroidHolder = new Asteroid[7];
                        break;
                    }
                default:
                    {
                        this.z_asteroidHolder = new Asteroid[0];
                        break;
                    }
            }
        }


        //Accessor Methods
        public AsteroidManagerState getState()
        {
            return this.z_currentState;
        }


        //Mutator Methods
        public void setState(AsteroidManagerState newState)
        {
            this.z_currentState = newState;
            this.adjustManagementSettings();
            this.populateAsteroids();
        }
        
            
         //Populate Asteroid Method
        private void populateAsteroids()
        {
            for (int i = 0; i < this.z_asteroidHolder.Length; i++)
            {
                this.z_asteroidHolder[i] = new Asteroid(z_content.Load<Texture2D>
                                                        ("Images\\Asteroids\\Asteroid" + this.getRandomImage()));
                this.z_asteroidHolder[i].setPosition(new Vector2(this.getRandomWidth(), this.getRandomHeight()));
                this.z_asteroidHolder[i].setSpeed(this.getRandomSpeed());
                this.z_asteroidHolder[i].setRotationSpeed(this.getRandomRotationSpeed());
            }
        }


        //Method for Drawing the asteroids on the screen
        public void drawAsteroids()
        {
            foreach (Asteroid asteroid in this.z_asteroidHolder)
                this.z_spriteBatch.Draw(asteroid.getSprite(), asteroid.getPosition(), null,
                                        Color.White, asteroid.getRotation(), asteroid.getCenter(), 1.0f,
                                        SpriteEffects.None, 0);
        }


        //Method for Updating the asteroids
        public void updateAsteroids()
        {
            foreach (Asteroid asteroid in this.z_asteroidHolder)
            {
                //If the asteroid has gone off the screen, reset it back to the top
                if (asteroid.getPosition().Y > this.z_viewPort.Height+(asteroid.getSprite().Height*1.5))
                {
                    //Then reset it and rerandomize it's variables
                    //Not sure if reloading a new image for each asteroid is a good idea
                    //Might cause game lag**
                    asteroid.setSprite(z_content.Load<Texture2D>
                                       ("Images\\Asteroids\\Asteroid" + this.getRandomImage()));
                    asteroid.setPosition(new Vector2(this.getRandomWidth(), this.getRandomHeight()));
                    asteroid.setSpeed(this.getRandomSpeed());
                    //Because The rotation is perfectly centered, asteroids that move slow look really
                    //Off balance. So instead I'm temporarily scaling rotation speed with the asteroids traveling speed
                    //asteroid.setRotationSpeed(this.getRandomRotationSpeed());
                    asteroid.setRotationSpeed((asteroid.getSpeed() / 50)*this.getRandomRotationDirection());
                    asteroid.setIsAlive(true);
                }

                //Otherwise Update it's new position
                asteroid.AstroUpdate();

            }
        }



        //Randomizing Helper Methods --------------------------------------------------------------------------------------
        private float getRandomSpeed()
        {
            return MathHelper.Lerp(this.z_minAsteroidSpeed, 
                                    this.z_maxAsteroidSpeed, 
                                    (float)this.z_randomGenerator.NextDouble());
        }
        private float getRandomHeight()
        {
            return MathHelper.Lerp(this.z_minStartingHeight,
                                    this.z_maxStartingHeight,
                                    (float)this.z_randomGenerator.NextDouble());
        }
        private float getRandomWidth()
        {
            return MathHelper.Lerp(this.z_minStartingWidth,
                                    this.z_maxStartingWidth,
                                    (float)this.z_randomGenerator.NextDouble());
        }
        private float getRandomRotationSpeed()
        {
            return MathHelper.Lerp(this.z_minRotationSpeed,
                                    this.z_maxRotationSpeed,
                                    (float)this.z_randomGenerator.NextDouble());
        }
        private int getRandomImage()
        {
            return this.z_randomGenerator.Next(this.z_minImage, this.z_maxImage+1);
        }
        private int getRandomRotationDirection()
        {
            if (this.z_randomGenerator.Next(0,2) == 0)
                return 1;
            return -1;
        }
    }
}
