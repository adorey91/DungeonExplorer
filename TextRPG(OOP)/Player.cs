using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{ 
    internal class Player
    {
        static int playerMaxX;
        static int playerMaxY;
        public HealthSystem PlayerHealth;
        public int Score;
        static int playerDamage;
        static int playerCoins;
        static int playerX;
        static int playerY;
        public Player()
        {
            PlayerHealth = new HealthSystem();
            Score = 0;
        }
    }
}
