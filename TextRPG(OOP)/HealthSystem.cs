using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class HealthSystem
    {
        public int maxHeath;
        public int health;
        public int armor;

        public HealthSystem() //Constructor
        {
            maxHeath = 100;
            health = maxHeath;
        }
        public void Heal(int HpGain)
        {
            health += HpGain;
            if(health > maxHeath)
            {
                health = maxHeath;
            }
        }
        public void TakeDamage(int Damage)
        {
            health -= Damage;
            if(health <= 0 )
            {
                health = 0;
            }
        }
    }
}
