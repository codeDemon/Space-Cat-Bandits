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
    interface IArtificialIntelligence
    {
        //Set the starting position of the enemy
        Vector2 getStartingPosition();
        //Return the new velocity for the enemy
        Vector2 calculateNewVelocity(Vector2 currentPosition, GameTime gameTime);
        //Return a new speed for the enemy
        float calculateNewSpeed(Vector2 currentPosition, GameTime gameTime);
        //Decide when the enemy should fire a missl
        bool firesMissle(Vector2 currentPosition, GameTime gameTime);
        //The enemy's AI is finish and the enemy is ready to be removed from the game
        bool okToRemove();
    }
}
