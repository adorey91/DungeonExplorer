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
    /// The basic info needed for a plasmoid type enemy
    /// </summary>


    // this will draw the position if they can.
    internal class Plasmoid : Enemy
    {
        public Plasmoid(ConsoleColor color, Random sharedRandom, GameManager gameManager)
            : base(color, sharedRandom, gameManager)
        {
            enemyType = "Plasmoid";
            damage = 1;
            avatar = Settings.PlasmoidChar;
            healthSystem = new HealthSystem();
            healthSystem.SetHealth(Settings.PlasmoidBaseHP);
            healthSystem.IsAlive = true;
        }

        // Method to decide the movement direction
        public override void Update(Map gameMap)
        {
            int moveResult = moveRoll.Next(1, 5);
            //Debug.WriteLine("Plasmoid roll result = " + moveResult);

            switch (moveResult)
            {
                case 1: Move(0, -1, gameMap); break; // Move Up
                case 2: Move(0, 1, gameMap); break;  // Move Down
                case 3: Move(-1, 0, gameMap); break; // Move Left
                case 4: Move(1, 0, gameMap); break;  // Move Right
            }
        }

        // This method will draw the position if they can.
        private void Move(int dx, int dy, Map gameMap)
        {
            // Calculate target position based on the move direction (dx, dy)
            int targetX = position.x + dx; // New X position
            int targetY = position.y + dy; // New Y position

            // Check if the target position is within the bounds of the map && the tile is walkable
            if (gameMap.IsWithinBounds(targetY, targetX) && gameMap.IsWalkable(targetY, targetX))
            {
                // Check if the target position is occupied by the player
                if (IsTileOccupiedByPlayer(targetY, targetX))
                {
                    // If the tile is occupied by the player, attack the player
                    player.healthSystem.TakeDamage(damage);
                    uiManager.AddEventLogMessage($"{player.name} attacked by {enemyType}");
                    //Debug.WriteLine($"{enemyType} attacked player at position ({targetY}, {targetX})");
                }
                // Check if the position is occupied by another enemy or an item
                else if (!IsPositionOccupied(targetY, targetX, this))
                {
                    // If the position is free, update the enemy's position
                    position.x = targetX;
                    position.y = targetY;
                    //Debug.WriteLine($"{enemyType} moved to ({targetY}, {targetX})");
                }
                else
                {
                    // If the position is occupied by an item or another enemy
                    //Debug.WriteLine($"Position ({targetY}, {targetX}) is occupied by an item or another enemy.");
                }
            }
            //else
            //{
            //    Debug.WriteLine($"Plasmoid attempted to move out of bounds: ({targetY}, {targetX})");
            //}
        }


    }
}