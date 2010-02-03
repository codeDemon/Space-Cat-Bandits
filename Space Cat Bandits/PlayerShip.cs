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
using System.Text;
#endregion


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


        #region InstanceVariables
        //Instance Variables ----------------------------------------------------------------------------
        private int z_health;
        private int z_lives;
        private float z_maxSpeed;
        private float z_acceleration;
        private bool z_IsSlowingDownX;
        private bool z_IsSlowingDownY;
        private float z_accelTimerX;
        private float z_accelTimerY;
        private AccelerationState currentXstate;
        private AccelerationState currentYstate;
        private bool z_IsInvincible;
        private float z_InvincibleTimer;
        private float z_drawTimer;
        private Vector2 z_startingPosition;
        #endregion



        #region Constructor
        //Constructor -----------------------------------------------------------------------------------     
        public PlayerShip(Texture2D loadedSprite, Vector2 startingPosition)
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
            this.z_lives = 10;
            this.z_IsInvincible = true;
            this.z_InvincibleTimer = 0;
            this.z_drawTimer = 0;
            this.z_startingPosition = startingPosition;
            this.setPosition(z_startingPosition);
        }
        #endregion



        #region Accessors
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
        public int getLives()
        {
            return this.z_lives;
        }
        public bool getIsInvincible()
        {
            return this.z_IsInvincible;
        }
        #endregion



        #region Mutators
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
        public void setLives(int newLives)
        {
            this.z_lives = newLives;
        }
        public void setIsInvincible(bool itIs)
        {
            this.z_IsInvincible = itIs;
        }
        #endregion



        #region AccelerationMethods
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
        #endregion



        #region UpdateMethod
        //The main Update Method for the Player Ship --------------------------------------------------------
        public void playerShipUpdate(GameTime gameTime, Rectangle viewPort)
        {
            //Check to see if the player is alive
            if (!this.getIsAlive())
            {
                //If any lives left, then revive
                if (this.z_lives > 0)
                {
                    this.z_IsInvincible = true;
                    this.setIsAlive(true);
                    this.setPosition(this.z_startingPosition);
                    this.setHealth(100);
                }
                else
                {
                    //Game Over
                    this.setHealth(100);
                    this.setHitRec(new Rectangle(0, 0, 0, 0));
                    return;
                }

            }

            //Check to see if the player has any health
            if (this.z_health <= 0)
            {
                this.setIsAlive(false);
                this.z_lives--;
                return;
            }

            //Update the ships Hit Region
            if (this.z_IsInvincible)
            {
                //While in Invincible/Recovery mode, ship can not collide
                this.setHitRec(new Rectangle(0, 0, 0, 0));
                if (this.z_InvincibleTimer > 3000)
                {
                    this.z_IsInvincible = false;
                    this.z_InvincibleTimer = 0;
                    this.z_drawTimer = 0;
                }
                else
                    this.z_InvincibleTimer += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
                this.setHitRec(new Rectangle((int)this.getPosition().X, (int)this.getPosition().Y,
                                             this.getSprite().Width, this.getSprite().Height));
            

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
        #endregion



        #region HelperMethod
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
        #endregion



        #region DrawMethod
        //Draw Method for PlayerShip
        public void draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!this.getIsAlive())
                return;
            if (!this.z_IsInvincible)
                spriteBatch.Draw(this.getSprite(), this.getPosition(), Color.White);
            else
            {
                if (this.z_drawTimer >= 0 && this.z_drawTimer < 100)
                {
                    spriteBatch.Draw(this.getSprite(), this.getPosition(), new Color(.8f,.8f,.8f,0.40f));
                    this.z_drawTimer += gameTime.ElapsedGameTime.Milliseconds;

                }
                else if (this.z_drawTimer >= 100 && this.z_drawTimer < 200)
                {
                    spriteBatch.Draw(this.getSprite(), this.getPosition(), new Color(.8f,.8f,.8f,0.80f));
                    this.z_drawTimer += gameTime.ElapsedGameTime.Milliseconds;

                }
                else
                    this.z_drawTimer = 0;

            }
        }
        #endregion





    }
}
