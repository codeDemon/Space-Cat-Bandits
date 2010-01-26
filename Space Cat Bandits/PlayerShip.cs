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
        //Instance Variables ----------------------------------------------------------------------------
        private int z_health;

        //Constructor -----------------------------------------------------------------------------------     
        public PlayerShip(Texture2D loadedSprite)
            : base(loadedSprite)
        {
            this.z_health = 100;
        }


        //Accessor Methods ------------------------------------------------------------------------------
        public int getHealth()
        {
            return this.z_health;
        }

        //Mutator Methods -------------------------------------------------------------------------------
        public void setHealth(int newHealth)
        {
            this.z_health = newHealth;
        }

        //Other Methods ---------------------------------------------------------------------------------





    }
}
