using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Enemy : Character
    {
        //public HealthSystem EnemyHealth; old
        public int enemyDamage;
        public int expDrop;
        public Enemy()
        {
           //EnemyHealth = new HealthSystem(); old
        }
    }
}
