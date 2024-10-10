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
    /// The basic info needed for a contruct type enemy
    /// </summary>
    internal class Construct : Enemy
    {
        private int ChaseRange = 7;

        public Construct(ConsoleColor color, Random sharedRandom, GameManager gameManager) : base(color, sharedRandom, gameManager)
        {
            enemyType = "Construct";
            maxHP = 5;
            damage = Settings.ConstructBaseDamage;
            avatar = Settings.ConstructChar ;  // ASCII character for Construct
            healthSystem = new HealthSystem();
            healthSystem.SetHealth(Settings.ConstructBaseHP);
            healthSystem.IsAlive = true;
        }

        public override void Update(Map gameMap)
        {
            // Calculate the distance to the player
            int rangeX = position.x - player.position.x;
            int rangeY = position.y - player.position.y;

            prevX = position.x;
            prevY = position.y;

            // Check if the player is within the chase range
            if (Math.Abs(rangeX) < ChaseRange && Math.Abs(rangeY) < ChaseRange)
            {
                MoveTowardPlayer(rangeX, rangeY, gameMap);
            }
        }

        private void MoveTowardPlayer(int rangeX, int rangeY, Map gameMap)
        {
            // Move horizontally towards the player
            if (rangeX > 0) Move(-1, 0, gameMap); // Chase left
            else if (rangeX < 0) Move(1, 0, gameMap); // Chase right

            // Move vertically towards the player
            if (rangeY > 0) Move(0, -1, gameMap); // Chase up
            else if (rangeY < 0) Move(0, 1, gameMap); // Chase down
        }

        private void Move(int dx, int dy, Map gameMap)
        {
            int targetX = position.x + dx; // New X position
            int targetY = position.y + dy; // New Y position

            // Check if the target position is within the bounds of the map
            if (!gameMap.IsWithinBounds(targetY, targetX))
            {
                //Debug.WriteLine($"Construct attempted to move out of bounds: ({targetY}, {targetX})");
                return;
            }

            // Check if the target position is occupied by the player
            if (IsTileOccupiedByPlayer(targetY, targetX))
            {
                //Debug.WriteLine($"Construct attacked player at: ({targetY}, {targetX})");
                player.healthSystem.TakeDamage(damage);
                uiManager.AddEventLogMessage($"{player.name} attacked by {enemyType}");
                return; // Do not move into the player's position
            }

            // Check if the target position is occupied by another enemy
            if (IsPositionOccupied(targetY, targetX, this))
            {
                //Debug.WriteLine($"Construct found an enemy at ({targetX}, {targetY}) and will do nothing.");
                return; // Do nothing if there's another enemy
            }

            // If the target position is walkable, update the position
            if (gameMap.IsWalkable(targetY, targetX))
            {
                position.x = targetX;
                position.y = targetY;
                //Debug.WriteLine($"Construct moved to ({position.x}, {position.y})");
            }
            else
            {
                //Debug.WriteLine($"Construct attempted to move to an unwalkable tile: ({targetY}, {targetX})");
            }
        }
    }
}