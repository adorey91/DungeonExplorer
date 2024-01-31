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
            playerDamage = 10; // player starting damage
            PlayerMaxHP = 100; // % health out of 100.
            healthSystem.SetHealth(PlayerMaxHP);
            playerName = "Koal"; // Testing for passing string.
            //Console.Write("Initialized" + playerName);
        }
        public void SetPlayerPosition(int x, int y)
        {
            position.x = x;
            position.y = y;
        }
    }
}
