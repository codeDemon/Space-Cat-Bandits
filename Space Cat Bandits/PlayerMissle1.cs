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
    class PlayerMissle1 : MissleObject
    {
        //Instance Variables ---------------------------------------------------------


        //Constructor ----------------------------------------------------------------
        public PlayerMissle1(Texture2D MissleSprite, Vector2 playersLocation, SpriteBatch spriteBatch)
            : base(MissleSprite, playersLocation, spriteBatch)
        {
            this.setVelocity(new Vector2(0, -1));
            this.setSpeed(8);
            this.setIsAlive(true);
        }

        //Accessor Methods -----------------------------------------------------------


        //Mutator Methods ------------------------------------------------------------


        //Other Methods --------------------------------------------------------------











    }
}
