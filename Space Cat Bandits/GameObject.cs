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
    class GameObject
    {
        //Declare Instance Variables ----------------------------------------------------------------------------
        private Texture2D z_sprite;
        private Vector2 z_position;
        private Vector2 z_velocity;
        private float z_speed;
        private bool z_isAlive;
        //For Hit Detection Purposes
        private bool z_isKillerObject;
        private bool z_isPickUp;
        private Rectangle z_hitRec;


        //Constructor -------------------------------------------------------------------------------------------
        public GameObject(Texture2D loadedTexture)
        {
            //Initialize all instance variables
            this.z_sprite = loadedTexture;
            this.z_position = Vector2.Zero;
            this.z_velocity = Vector2.Zero;
            this.z_speed = 1.0f;
            this.z_isAlive = false;
            this.z_hitRec = new Rectangle(0, 0, 0, 0);
            this.z_isKillerObject = false;
            this.z_isPickUp = false;
        }


        //Access Methods ----------------------------------------------------------------------------------------
        public Texture2D getSprite()
        {
            return this.z_sprite;
        }
        public Vector2 getPosition()
        {
            return this.z_position;
        }
        public Vector2 getVelocity()
        {
            return this.z_velocity;
        }
        public bool getIsAlive()
        {
            return this.z_isAlive;
        }
        public float getSpeed()
        {
            return this.z_speed;
        }
        public bool getIsKillerObject()
        {
            return this.z_isKillerObject;
        }
        public bool getIsPickUp()
        {
            return this.z_isPickUp;
        }
        public Rectangle getHitRec()
        {
            return this.z_hitRec;
        }


        //Mutator Methods ---------------------------------------------------------------------------------------
        public void setSprite(Texture2D newSprite)
        {
            this.z_sprite = newSprite;
        }
        public void setPosition(Vector2 newPosition)
        {
            this.z_position = newPosition;
        }
        public void setVelocity(Vector2 newVelocity)
        {
            this.z_velocity = newVelocity;
        }
        public void setIsAlive(bool isAlive)
        {
            this.z_isAlive = isAlive;
        }
        public void setSpeed(float newSpeed)
        {
            this.z_speed = newSpeed;
        }
        public void setIsKillerObject(bool isKiller)
        {
            if (isKiller == true && this.z_isPickUp)
                this.z_isPickUp = false;
            this.z_isKillerObject = isKiller;
        }
        public void setIsPickUp(bool isPickup)
        {
            if (this.z_isKillerObject && isPickup == true)
                this.z_isKillerObject = false;
            this.z_isPickUp = isPickup;
        }
        public void setHitRec(Rectangle newHitRec)
        {
            this.z_hitRec = newHitRec;
        }


        //Other Methods -----------------------------------------------------------------------------------------
        public void upDatePosition()
        {
            this.z_position += this.z_velocity;
        }

        //Use this method for updating position if a speed is set
        public void upDatePositionWithSpeed()
        {
            this.z_position += new Vector2(this.z_velocity.X * this.z_speed,
                                           this.z_velocity.Y * this.z_speed);
        }




    }
}
