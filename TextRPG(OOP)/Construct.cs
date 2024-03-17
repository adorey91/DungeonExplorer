using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Construct : Enemy
    {
        public Construct(ConsoleColor color)
        {
            avatar = ((char)4);
            avatarColor = color;
        }
        public void SetEnemyStats()
        {
            healthSystem.health = 5 + levelNumber;
            healthSystem.armor = levelNumber;
            enemyDamage = 2 + levelNumber;
        }
    }
}
