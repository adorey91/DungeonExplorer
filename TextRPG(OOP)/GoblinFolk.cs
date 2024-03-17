using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class GoblinFolk : Enemy
    {
        public GoblinFolk(ConsoleColor color)
        {
            avatar = ((char)5);
            avatarColor = color;
        }
        public void SetEnemyStats()
        {
            healthSystem.health = 0 + levelNumber;
            healthSystem.armor = 0;
            enemyDamage = 1;
        }
    }
}
