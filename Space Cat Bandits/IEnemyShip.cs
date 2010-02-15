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
    abstract class IEnemyShip : GameObject
    {
        public IEnemyShip(Texture2D loadedSprite)
            : base(loadedSprite)
        {

        }
        //This method should be calaulated using some sort of AI
        abstract public void AIUpdate(GameTime gameTime);
        
        //These methods are used for the enemy Pool
        abstract public void setPointer(int pointer);
        abstract public int getPointer();

        //Method for reseting an enemy status
        abstract public void reset();
    }
}
