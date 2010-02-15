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
        /*
         * Logic Note: A lot of these variables should be converted into lists. Because 
         * Each wave will probably need to have it's own counters if we decide to allow
         * multiple waves to spawn at the same time. Dictionary comes to mind. For now, keep it simple
         * */
        //Instance Variables
        private List<IEnemyShip> z_enemyShips;
        private ContentManager z_content;
        private SpriteBatch z_spriteBatch;
        private Rectangle z_viewPort;
        //Create a pool of enemy images already loaded
        private PoolEnemy1 z_poolEnemy1;
        //A counter for the Update Method
        private float z_counter;
        //another counter for spreading out the enemies as they spawn
        private float z_interval;
        //A counter for keeping track how many enemies are spawned
        private int z_EnemiesSpawn;
        //Booleans for activating a type of wave of enemies
        //E1W1 stands for Enemey 1, Wave 1
        private bool z_ActivateE1W1;
        private bool z_ActivateE1W2;
        private bool z_ActivateE1W3;

        //Constructor
        public EnemyManager(ContentManager content, SpriteBatch spriteBatch, Rectangle viewPort)
        {
            this.z_enemyShips = new List<IEnemyShip>();
            this.z_content = content;
            this.z_spriteBatch = spriteBatch;
            this.z_viewPort = viewPort;
            this.populateAnEnemy();
            this.z_ActivateE1W1 = false;
            this.z_ActivateE1W2 = false;
            this.z_ActivateE1W3 = false;
            this.z_counter = 0;
            this.z_interval = 0;
            this.z_EnemiesSpawn = 0;

            //Populate the pool
            this.z_poolEnemy1 = new PoolEnemy1(this.z_content, this.z_viewPort);
        }


        //Accessors
        public bool getE1W1()
        {
            return this.z_ActivateE1W1;
        }
        public bool getE1W2()
        {
            return this.z_ActivateE1W2;
        }
        public bool getE1W3()
        {
            return this.z_ActivateE1W3;
        }

        //Mutators
        public void setE1W1(bool set)
        {
            this.z_ActivateE1W1 = set;
        }
        public void setE1W2(bool set)
        {
            this.z_ActivateE1W2 = set;
        }
        public void setE1W3(bool set)
        {
            this.z_ActivateE1W3 = set;
        }

        //Populate a single enemy1 method
        private void populateAnEnemy()
        {

            Enemy1 enemy = this.z_poolEnemy1.borrowAnEnemy();
            this.z_enemyShips.Add(enemy);
            this.z_enemyShips[0].setIsAlive(true);
        }

        //Populate three enemy1
        private void populateEnemy1Wave1(GameTime gameTime)
        {
            this.z_interval += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (this.z_interval >= 600)
            {
                if (this.z_EnemiesSpawn < 3)
                {
                    this.z_enemyShips.Add(this.z_poolEnemy1.borrowAnEnemy());
                    this.z_enemyShips.Last().setIsAlive(true);
                    this.z_EnemiesSpawn++;
                }
                else
                {
                    this.z_EnemiesSpawn = 0;
                    this.z_ActivateE1W1 = false;
                }
                this.z_interval = 0;
            }
        }

        //Populate five enemy1
        private void populateEnemy1Wave2(GameTime gameTime)
        {
            this.z_interval += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (this.z_interval >= 600)
            {
                if (this.z_EnemiesSpawn < 5)
                {
                    this.z_enemyShips.Add(this.z_poolEnemy1.borrowAnEnemy());
                    this.z_enemyShips.Last().setIsAlive(true);
                    this.z_EnemiesSpawn++;
                }
                else
                {
                    this.z_EnemiesSpawn = 0;
                    this.z_ActivateE1W1 = false;
                }
                this.z_interval = 0;
            }
        }

        //Populate ten enemy1
        private void populateEnemy1Wave3(GameTime gameTime)
        {
            this.z_interval += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (this.z_interval >= 600)
            {
                if (this.z_EnemiesSpawn < 10)
                {
                    this.z_enemyShips.Add(this.z_poolEnemy1.borrowAnEnemy());
                    this.z_enemyShips.Last().setIsAlive(true);
                    this.z_EnemiesSpawn++;
                }
                else
                {
                    this.z_EnemiesSpawn = 0;
                    this.z_ActivateE1W1 = false;
                }
                this.z_interval = 0;
            }
        }

        //Update all Enemies in the list method
        public void mainUpdate(GameTime gameTime)
        {
            this.z_counter += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (this.z_counter > 5000)
            {
                this.z_ActivateE1W1 = true;
                this.z_counter = 0;
            }
            
            for (int i = 0; i < this.z_enemyShips.Count; i++)
            {
                this.z_enemyShips[i].AIUpdate(gameTime);
                if (!this.z_enemyShips[i].getIsAlive())
                {
                    if(this.z_enemyShips[i] is Enemy1)
                        this.z_poolEnemy1.returnAnEnemy((Enemy1)this.z_enemyShips[i]);
                    this.z_enemyShips.RemoveAt(i);
                    i--;
                }
            }

            if (this.z_ActivateE1W1)
                this.populateEnemy1Wave1(gameTime);
            if (this.z_ActivateE1W2)
                this.populateEnemy1Wave2(gameTime);
            if (this.z_ActivateE1W3)
                this.populateEnemy1Wave3(gameTime);
             
        }

        //Draw Method
        public void draw()
        {
            for (int i = 0; i < this.z_enemyShips.Count; i++)
            {
                if(this.z_enemyShips[i].getIsAlive())
                    this.z_spriteBatch.Draw(this.z_enemyShips[i].getSprite(), 
                                            this.z_enemyShips[i].getPosition(), 
                                            Color.White);
            }
        }


    }
}
