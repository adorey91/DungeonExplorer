using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;

namespace TextRPG_OOP_
{
    internal class HealthSystem
    {
        public int health;
        public int armor;
        public bool IsAlive;
        public HealthSystem() //Constructor
        {
            IsAlive = true;
            armor = 0;
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
            health -= Damage - armor;
            if(health <= 0 )
            {
                health = 0;
                IsAlive = false;
            }
        }
        public void SetHealth(int maxHP)
        {
            health = maxHP;
        }
        public int GetHealth()
        {
            return health;
        }
        public void IncreseArmor(int armorUp)
        {
            armor += armorUp;
        }
    }
}
