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
    class EnemyManager
    {
        //Instance Variables
        private List<IEnemyShip> z_enemyShips;
        private ContentManager z_content;
        private SpriteBatch z_spriteBatch;
        private Rectangle z_viewPort;

        //Constructor
        public EnemyManager(ContentManager content, SpriteBatch spriteBatch, Rectangle viewPort)
        {
            this.z_enemyShips = new List<IEnemyShip>();
            this.z_content = content;
            this.z_spriteBatch = spriteBatch;
            this.z_viewPort = viewPort;
            this.populateAnEnemy();
        }


        //Accessors


        //Mutators


        //Populate a single enemy1 method
        private void populateAnEnemy()
        {
            this.z_enemyShips.Add(new Enemy1(this.z_content.Load<Texture2D>("Images\\EnemyShips\\EnemyShip1"),this.z_viewPort));
            this.z_enemyShips[0].setIsAlive(true);
        }

        //Update all Enemies in the list method
        public void mainUpdate(GameTime gameTime)
        {
            
            for (int i = 0; i < this.z_enemyShips.Count; i++)
            {
                this.z_enemyShips[i].AIUpdate(gameTime);
                if (!this.z_enemyShips[i].getIsAlive())
                {
                    this.z_enemyShips.RemoveAt(i);
                    i--;
                }
            }
             
        }

        //Draw Method
        public void draw()
        {
            for (int i = 0; i < this.z_enemyShips.Count; i++)
            {
                this.z_spriteBatch.Draw(this.z_enemyShips[i].getSprite(), 
                                        this.z_enemyShips[i].getPosition(), 
                                        Color.White);
            }
        }


    }
}
