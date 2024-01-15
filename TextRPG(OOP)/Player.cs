using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{ 
    internal class Player
    {
        public HealthSystem PlayerHealth;
        public int Score;
        public Player()
        {
            PlayerHealth = new HealthSystem();
            Score = 0;
        }
    }
}
