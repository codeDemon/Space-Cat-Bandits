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
        private Texture2D sprite;
        private Vector2 position;
        private Vector2 velocity;
        private float speed;
        private bool isAlive;


        //Constructor -------------------------------------------------------------------------------------------
        public GameObject(Texture2D loadedTexture)
        {
            //Initialize all instance variables
            this.sprite = loadedTexture;
            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
            this.speed = 0.0f;
            this.isAlive = false;
        }


        //Access Methods ----------------------------------------------------------------------------------------
        public Texture2D getSprite()
        {
            return this.sprite;
        }
        public Vector2 getPosition()
        {
            return this.position;
        }
        public Vector2 getVelocity()
        {
            return this.velocity;
        }
        public bool getIsAlive()
        {
            return this.isAlive;
        }
        public float getSpeed()
        {
            return this.speed;
        }


        //Mutator Methods ---------------------------------------------------------------------------------------
        public void setSprite(Texture2D newSprite)
        {
            this.sprite = newSprite;
        }
        public void setPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }
        public void setVelocity(Vector2 newVelocity)
        {
            this.velocity = newVelocity;
        }
        public void setIsAlive(bool isAlive)
        {
            this.isAlive = isAlive;
        }
        public void setSpeed(float newSpeed)
        {
            this.speed = newSpeed;
        }

        //Other Methods -----------------------------------------------------------------------------------------
        public void upDatePosition()
        {
            this.position += this.velocity;
        }




    }
}
