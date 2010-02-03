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
    class Enemy1 : IEnemyShip
    {
        //Instance Variables
        private AI_ZigZag z_AI;

        //Constructor
        public Enemy1(Texture2D loadedSprite, Rectangle viewPort)
            : base(loadedSprite)
        {
            z_AI = new AI_ZigZag(viewPort);
            this.setPosition(this.z_AI.getStartingPosition());
        }

        //Accessors

        //Mutators











        public override void AIUpdate(GameTime gameTime)
        {
            if (this.z_AI.okToRemove())
            {
                this.setIsAlive(false);
                return;
            }

            this.setVelocity(this.z_AI.calculateNewVelocity(this.getPosition(), gameTime));

            this.upDatePosition();
        }

       
    }
}
