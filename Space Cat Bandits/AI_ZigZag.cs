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
    class AI_ZigZag : IArtificialIntelligence
    {

        //Instance Variables
        private bool z_removeEnemy;
        private Rectangle z_viewPort;
        private Vector2 z_startPosition;
        private bool z_reachedPoint1;
        private bool z_reachedPoint2;
        private bool z_reachedPoint3;
        private bool z_reachedPoint4;
        private bool z_reachedPoint5;

        //Constructor
        public AI_ZigZag(Rectangle viewPort)
        {
            this.z_viewPort = viewPort;
            this.z_removeEnemy = false;
            this.z_startPosition = new Vector2(-150, 25);
            this.z_reachedPoint1 = false;
            this.z_reachedPoint2 = false;
            this.z_reachedPoint3 = false;
            this.z_reachedPoint4 = false;
            this.z_reachedPoint5 = false;
        }

        //Accessor Methods
        

        //Mutators
        











        #region IArtificialIntelligence Members

        public Microsoft.Xna.Framework.Vector2 getStartingPosition()
        {
            return this.z_startPosition;
        }

        public Microsoft.Xna.Framework.Vector2 calculateNewVelocity(Microsoft.Xna.Framework.Vector2 currentPosition, Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Go right until point 1 is reached.
            //Go down until point 2 is reached.
            //Go left until point 3 is reached.
            //Go down until point 4 is reached.
            //Go Right until point 5 is reached.
            //Remove enemy from game.
            if (!this.z_reachedPoint1)
            {
                if (currentPosition.X < this.z_viewPort.Width + 200)
                    return new Vector2(3, 0);
                else
                {
                    this.z_reachedPoint1 = true;
                    return new Vector2(0, 0);
                }
            }
            else if (!this.z_reachedPoint2)
            {
                if (currentPosition.Y < this.z_startPosition.Y + 100)
                    return new Vector2(0, 2);
                else
                {
                    this.z_reachedPoint2 = true;
                    return new Vector2(0, 0);
                }
            }
            else if (!this.z_reachedPoint3)
            {
                if (currentPosition.X > this.z_startPosition.X)
                    return new Vector2(-3, 0);
                else
                {
                    this.z_reachedPoint3 = true;
                    return new Vector2(0, 0);
                }
            }
            else if (!this.z_reachedPoint4)
            {
                if (currentPosition.Y < this.z_startPosition.Y + 200)
                    return new Vector2(0, 2);
                else
                {
                    this.z_reachedPoint4 = true;
                    return new Vector2(0, 0);
                }
            }
            else if (!this.z_reachedPoint5)
            {
                if (currentPosition.X < this.z_viewPort.Width + 200)
                    return new Vector2(3, 0);
                else
                {
                    this.z_reachedPoint1 = true;
                    return new Vector2(0, 0);
                }
            }
            else
            {
                this.z_removeEnemy = true;
                return new Vector2(0, 0);
            }
        }

        public float calculateNewSpeed(Microsoft.Xna.Framework.Vector2 currentPosition, Microsoft.Xna.Framework.GameTime gameTime)
        {
            //keep it simple for now, constant speed
            return 1.0f;
        }

        public bool firesMissle(Microsoft.Xna.Framework.Vector2 currentPosition, Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Simple for now
            return false;
        }

        public bool okToRemove()
        {
            return this.z_removeEnemy;
        }

        #endregion
    }
}
