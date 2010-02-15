using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Cat_Bandits
{
    interface ILevel
    {
        //An Interface that every level is required to use

        

        //Methods that a level is required to provide
        //LevelStates getCurrentState();
        //LevelStates setCurrentState();


        void loadAssets();


        void unLoadAssets();
        



    }
}
