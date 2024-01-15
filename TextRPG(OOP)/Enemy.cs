using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Enemy
    {
        public HealthSystem EnemyHealth;

        public Enemy()
        {
            EnemyHealth = new HealthSystem();
        }
    }
}
