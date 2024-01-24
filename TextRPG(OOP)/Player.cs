using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{ 
    internal class Player : Character
    {
        public int experience;
        static int playerDamage;
        static int playerCoins;
        public Player()
        {
            experience = 0;
            playerCoins = 0;
            playerDamage = 1;
        }
    }
}
