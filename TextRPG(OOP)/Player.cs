using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{ 
    internal class Player : Character
    {
        public int experience;
        public int playerDamage;
        public int playerCoins;
        public int PlayerMaxHP;
        public string playerName;
        public Player()
        {
            experience = 0;
            playerCoins = 0;
            playerDamage = 10;
            PlayerMaxHP = 100;
            healthSystem.SetHealth(PlayerMaxHP);
            playerName = "Koal"; // Testing for passing string.
        }
    }
}
