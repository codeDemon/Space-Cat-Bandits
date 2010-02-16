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
        private int z_pointer;

        //Constructor
        public Enemy1(Texture2D loadedSprite, Rectangle viewPort)
            : base(loadedSprite)
        {
            z_AI = new AI_ZigZag(viewPort);
            this.setPosition(this.z_AI.getStartingPosition());
            z_pointer = 0;
        }

        //Accessors
        public override void setPointer(int pointer)
        {
            this.z_pointer = pointer;
        }

        //Mutators
        public override int getPointer()
        {
            return this.z_pointer;
        }


        public override void reset()
        {
            this.setPosition(Vector2.Zero);
            this.setVelocity(Vector2.Zero);
            this.setSpeed(1.0f);
            this.setIsAlive(false);
            this.setHitRec(new Rectangle(0, 0, 0, 0));
            this.setIsKillerObject(false);
            this.setIsPickUp(false);
        }







        public override void AIUpdate(GameTime gameTime)
        {
            if (this.z_AI.okToRemove())
            {
                this.setIsAlive(false);
                return;
            }

            this.setVelocity(this.z_AI.calculateNewVelocity(this.getPosition(), gameTime));

            this.upDatePosition();
            this.setHitRec(new Rectangle((int)this.getPosition().X, (int)this.getPosition().Y, 
                          (int)this.getSprite().Width, (int)this.getSprite().Height));
        }

       
    }
}
