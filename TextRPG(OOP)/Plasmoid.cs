using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Plasmoid : Enemy
    {
        public Plasmoid(ConsoleColor color)
        {
            avatar = ((char)6);
            avatarColor = color;
        }
        public void SetEnemyStats()
        {
            healthSystem.health = 3 + levelNumber;
            healthSystem.armor = levelNumber - 1;
            enemyDamage = levelNumber;
        }
    }
}
