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
    class PoolEnemy1
    {
        //Instance Variables
        private List<Enemy1> z_EnemyPool;
        private int z_pointer;
        private int z_counter;
        private ContentManager z_content;

        //Constructor
        public PoolEnemy1(ContentManager content, Rectangle viewPort)
        {
            this.z_pointer = 0;
            this.z_counter = 0;
            this.z_EnemyPool = new List<Enemy1>();
            this.z_content = content;
            //Add 500 enemies to the pool
            for (; this.z_counter < 500; this.z_counter++)
            {
                this.z_EnemyPool.Add(new Enemy1(z_content.Load<Texture2D>("Images\\EnemyShips\\EnemyShip1"), viewPort));
                this.z_EnemyPool[this.z_counter].setPointer(this.z_counter);
            }
        }

        //Accessors
        public Enemy1 borrowAnEnemy()
        {
            Console.WriteLine(this.z_EnemyPool[this.z_pointer]);
            if (this.z_pointer > this.z_counter)
                throw new Exception("Out of Enemy1");

            return this.z_EnemyPool.ElementAt<Enemy1>(this.z_pointer++);
        }

        //Mutators
        public void returnAnEnemy(Enemy1 enemy)
        {
            enemy.reset();
            this.z_EnemyPool.Add(enemy);
            this.z_EnemyPool.Last<Enemy1>().setPointer(++this.z_counter);
        }




    }
}
