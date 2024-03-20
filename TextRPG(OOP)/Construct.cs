using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Construct : Enemy
    {
        public int BaseHP;
        public int BaseDamage;
        public Construct(ConsoleColor color, Settings settings)
        {
            avatar = ((char)4);
            avatarColor = color;
            BaseHP = settings.ConstructBaseHP;
            BaseDamage = settings.ConstructBaseDamage;
        }
        public void SetEnemyStats()
        {
            healthSystem.health = BaseHP + levelNumber;
            healthSystem.armor = levelNumber;
            enemyDamage = BaseDamage + levelNumber;
        }
    }
}
