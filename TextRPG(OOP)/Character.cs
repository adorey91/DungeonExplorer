using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Character
    {
        private HealthSystem healthSystem;
        public Character()
        {
            healthSystem = new HealthSystem();
        }
        struct Position
        {
            int x;
            int y;
        }
    }
}
