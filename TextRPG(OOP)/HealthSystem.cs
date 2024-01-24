using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class HealthSystem
    {
        public int health;
        public int armor;

        public HealthSystem() //Constructor
        {

        }
        public void Heal(int HpGain, int maxHeath) //Health gain and health max needed to not over heal. 
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
        public void SetHealth(int maxHP)
        {
            health = maxHP;
        }
    }
}
