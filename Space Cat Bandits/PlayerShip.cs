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
    class PlayerShip : GameObject
    {

        enum AccelerationState
        {
            positive,
            negative,
            zero
        }

        //Instance Variables ----------------------------------------------------------------------------
        private int z_health;
        private float z_maxSpeed;
        private float z_acceleration;
        private bool z_IsSlowingDownX;
        private bool z_IsSlowingDownY;
        private float z_accelTimerX;
        private float z_accelTimerY;
        private AccelerationState currentXstate;
        private AccelerationState currentYstate;

        //Constructor -----------------------------------------------------------------------------------     
        public PlayerShip(Texture2D loadedSprite)
            : base(loadedSprite)
        {
            this.z_health = 100;
            this.z_maxSpeed = 3.0f;
            this.z_acceleration = .5f;
            this.z_IsSlowingDownX = false;
            this.z_IsSlowingDownY = false;
            this.z_accelTimerX = 0;
            this.z_accelTimerY = 0;
            this.currentXstate = AccelerationState.zero;
            this.currentYstate = AccelerationState.zero;
        }


        //Accessor Methods ------------------------------------------------------------------------------
        public int getHealth()
        {
            return this.z_health;
        }
        public bool getIsSlowingDownX()
        {
            return this.z_IsSlowingDownX;
        }
        public bool getIsSlowingDownY()
        {
            return this.z_IsSlowingDownY;
        }

        //Mutator Methods -------------------------------------------------------------------------------
        public void setHealth(int newHealth)
        {
            this.z_health = newHealth;
        }
        public void setIsSlowingDownX(bool isIt)
        {
            this.z_IsSlowingDownX = isIt;
        }
        public void setIsSlowingDownY(bool isIt)
        {
            this.z_IsSlowingDownY = isIt;
        }

        //Acceleration Methods ---------------------------------------------------------------------------------
        public void accelerateLeft()
        {
            if (this.getVelocity().X > -1 * this.z_maxSpeed )
            {
                if (this.z_IsSlowingDownX)
                    this.resetXvelocity();
                this.setVelocity(new Vector2(this.getVelocity().X - this.z_acceleration, this.getVelocity().Y));
                this.currentXstate = AccelerationState.negative;            
            }
        }
        public void accelerateRight()
        {
            if (this.getVelocity().X < this.z_maxSpeed )
            {
                if (this.z_IsSlowingDownX)
                    this.resetXvelocity();
                this.setVelocity(new Vector2(this.getVelocity().X + this.z_acceleration, this.getVelocity().Y));
                this.currentXstate = AccelerationState.positive;
            }
        }
        public void accelerateUp()
        {
            if (this.getVelocity().Y > -1 * this.z_maxSpeed )
            {
                if (this.z_IsSlowingDownY)
                    this.resetYvelocity();
                this.setVelocity(new Vector2(this.getVelocity().X, this.getVelocity().Y - this.z_acceleration));
                this.currentYstate = AccelerationState.negative;
            }
        }
        public void accelerateDown()
        {
            if (this.getVelocity().Y < this.z_maxSpeed )
            {
                if (this.z_IsSlowingDownY)
                    this.resetYvelocity();
                this.setVelocity(new Vector2(this.getVelocity().X, this.getVelocity().Y + this.z_acceleration));
                this.currentYstate = AccelerationState.positive;
            }
        }



        //The main Update Method for the Player Ship --------------------------------------------------------
        public void playerShipUpdate(GameTime gameTime, Rectangle viewPort)
        {
            //Ensure that the ship can not leave the viewPort ever
            if((this.getPosition().X <= 1 && this.currentXstate == AccelerationState.negative) || 
                (this.getPosition().X + this.getSprite().Width >= (float)viewPort.Width - 1 && 
                 this.currentXstate == AccelerationState.positive))
                    this.resetXvelocity();
            if((this.getPosition().Y <= 1 && this.currentYstate == AccelerationState.negative) || 
               (this.getPosition().Y + this.getSprite().Height >= (float)viewPort.Height -1 &&
                this.currentYstate == AccelerationState.positive))
                    this.resetYvelocity();

            //Bring ship back to screen if ever necessary
            if (this.getPosition().X < 0)
                    this.setPosition(new Vector2(0, this.getPosition().Y));
            if (this.getPosition().X + this.getSprite().Width > (float)viewPort.Width)
                    this.setPosition(new Vector2(viewPort.Width - this.getSprite().Width, this.getPosition().Y));
            if(this.getPosition().Y <0 )
                    this.setPosition(new Vector2(this.getPosition().X, 0));
            if (this.getPosition().Y + this.getSprite().Height > (float)viewPort.Height)
                    this.setPosition(new Vector2(this.getPosition().X, viewPort.Height - this.getSprite().Height));
                
            //Perform the actual update on the ship Object
            this.upDatePosition();


            //Check to see if the ship is slowing down in the X direction
            if (this.z_IsSlowingDownX)
            {
                if (this.z_accelTimerX < 100)
                    this.z_accelTimerX += (float)gameTime.ElapsedGameTime.Milliseconds;
                else
                {
                    this.z_accelTimerX = 0;
                    //Time to bring the x component of velocity closer to zero
                    //Determine if velocity is greater or less than zero by getting the current State of acceleration
                    switch (this.currentXstate)
                    {
                        case AccelerationState.negative:
                            {
                                //We want to make X velocity more positive, until zero
                                if (this.getVelocity().X < 0)
                                    this.setVelocity(new Vector2(this.getVelocity().X + this.z_acceleration, 
                                                                 this.getVelocity().Y));
                                else
                                    this.resetXvelocity();
                                break;
                            }
                        case AccelerationState.positive:
                            {
                                //We want to make X velocity more negative, until zero
                                if (this.getVelocity().X > 0)
                                    this.setVelocity(new Vector2(this.getVelocity().X - this.z_acceleration, 
                                                                 this.getVelocity().Y));
                                else
                                    this.resetXvelocity();
                                break;
                            }
                        default:
                            {
                                this.resetXvelocity();
                                break;
                            }
                    }
                }
            }

            //Also check to see if the ship is slowing down in the Y direction
            if (this.z_IsSlowingDownY)
            {
                if (this.z_accelTimerY < 100)
                    this.z_accelTimerY += (float)gameTime.ElapsedGameTime.Milliseconds;
                else
                {
                    this.z_accelTimerY = 0;
                    //Time to bring the Y component of velocity closer to zero
                    //Determine if velocity is greater or less than zero by getting the current State of acceleration
                    switch (this.currentYstate)
                    {
                        case AccelerationState.negative:
                            {
                                //We want to make Y velocity more positive, until zero
                                if (this.getVelocity().Y < 0)
                                    this.setVelocity(new Vector2(this.getVelocity().X, 
                                                     this.getVelocity().Y + this.z_acceleration));
                                else
                                    this.resetYvelocity();
                                break;
                            }
                        case AccelerationState.positive:
                            {
                                //We want to make Y velocity more negative, until zero
                                if (this.getVelocity().Y > 0)
                                    this.setVelocity(new Vector2(this.getVelocity().X, 
                                                                 this.getVelocity().Y - this.z_acceleration));
                                else
                                    this.resetYvelocity();
                                break;
                            }
                        default:
                            {
                                this.resetYvelocity();
                                break;
                            }
                    }
                }

            }

            
        }


        //Helper Methods for updating the player's ship
        private void resetXvelocity()
        {
            this.currentXstate = AccelerationState.zero;
            this.setVelocity(new Vector2(0, this.getVelocity().Y));
            this.z_IsSlowingDownX = false;
        }
        private void resetYvelocity()
        {
            this.currentYstate = AccelerationState.zero;
            this.setVelocity(new Vector2(this.getVelocity().X, 0));
            this.z_IsSlowingDownY = false;
        }

    }
}
