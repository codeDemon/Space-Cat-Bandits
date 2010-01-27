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
    class Asteroid : GameObject
    {
        //Instance Variables
        private Vector2 z_center;
        private float z_rotation;
        private float z_rotationSpeed;


        //Constructor
        public Asteroid(Texture2D loadedSprite)
            : base(loadedSprite)
        {
            this.z_rotation = 0.0f;
            this.z_center = new Vector2(this.getSprite().Width / 2, this.getSprite().Height / 2);
            this.setVelocity(new Vector2(0 , 2));
            this.z_rotationSpeed = 0f;
        }


        //Accessor Methods
        public Vector2 getCenter()
        {
            return this.z_center;
        }
        public float getRotation()
        {
            return this.z_rotation;
        }
        public float getRotationSpeed()
        {
            return this.z_rotationSpeed;
        }

        //Mutator Methods
        public void setCenter(Vector2 newCenter)
        {
            this.z_center = newCenter;
        }
        public void setRotation(float newRotation)
        {
            this.z_rotation = newRotation;
        }
        public void setRotationSpeed(float newRotSpeed)
        {
            this.z_rotationSpeed = newRotSpeed;
        }

        //Asteroid Update Method
        public void AstroUpdate()
        {
            this.z_rotation += this.z_rotationSpeed;
            this.upDatePositionWithSpeed();
        }

    }
}
