using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    /// <summary>
    /// The basic info needed for a goblin folk type enemy
    /// </summary>
    internal class GoblinFolk : Enemy
    {
        public GoblinFolk(ConsoleColor color, Random sharedRandom, GameManager gameManager) : base(color, sharedRandom, gameManager)
        {
            enemyType = "GoblinFolk";
            damage = 2;
            avatar = Settings.GoblinFolkChar;  // ASCII character for Goblin
            healthSystem = new HealthSystem();
            healthSystem.SetHealth(Settings.GoblinFolkBaseHP);
            healthSystem.IsAlive = true;
        }

        public override void Update(Map gameMap)
        {
            // Movement logic for GoblinFolk (fleeing behavior)
            //int rangeX = position.x - gameMap.characters[0].position.x;
            //int rangeY = position.Col - gameMap.characters[0].position.Col;

            //if (Math.Abs(rangeX) < 7 && Math.Abs(rangeY) < 5)
            //{
            //    MoveAwayFromPlayer(rangeX, rangeY, gameMap);
            //}
        }

        private void MoveAwayFromPlayer(int rangeX, int rangeY, Map gameMap)
        {
            // Fleeing movement based on player position
            if (rangeX > 0) Move(-1, 0, gameMap); // Flee left
            else if (rangeX < 0) Move(1, 0, gameMap); // Flee right

            if (rangeY > 0) Move(0, -1, gameMap); // Flee up
            else if (rangeY < 0) Move(0, 1, gameMap); // Flee down
        }

        private void Move(int dx, int dy, Map gameMap)
        {
            int targetX = Clamp(position.x + dx, 0, enemyMaxX);
            int targetY = Clamp(position.y + dy, 0, enemyMaxY);

            //if (!gameMap.CheckTile(targetY, targetX)) return;
            //position.x = targetX;
            //position.Col = targetY;
            //Debug.WriteLine($"GoblinFolk moved to ({position.x}, {position.Col})");
        }
    }
}