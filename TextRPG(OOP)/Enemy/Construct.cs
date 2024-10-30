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
        private int chaseRange;

        public Construct(ConsoleColor color, Random sharedRandom, GameManager gameManager, EnemyAttributes attributes) : base(color, sharedRandom, gameManager)
        {
            enemyType = "Construct";
            damage = attributes.BaseDamage;
            avatar = attributes.Character;  // ASCII character for Construct
            healthSystem = new HealthSystem();
            healthSystem.SetHealth(attributes.BaseHP);
            healthSystem.IsAlive = true;
            chaseRange = attributes.ChaseRange;
        }

        public override void Update(Map gameMap)
        {
            // Calculate the distance to the player
            int rangeX = position.x - player.position.x;
            int rangeY = position.y - player.position.y;

            // Check if the player is within the chase range
            if (Math.Abs(rangeX) < chaseRange && Math.Abs(rangeY) < chaseRange)
                MoveTowardPlayer(rangeX, rangeY, gameMap);
        }

        private void MoveTowardPlayer(int rangeX, int rangeY, Map gameMap)
        {
            if (rangeX > 0) Move(-1, 0, gameMap); // Chase left
            else if (rangeX < 0) Move(1, 0, gameMap); // Chase right

            if (rangeY > 0) Move(0, -1, gameMap); // Chase up
            else if (rangeY < 0) Move(0, 1, gameMap); // Chase down
        }

        private void Move(int dx, int dy, Map gameMap)
        {
            int targetX = position.x + dx; // New X position
            int targetY = position.y + dy; // New Y position

            // Check if the target position is within the bounds of the map
            if (!gameMap.IsWithinBounds(targetY, targetX))
                return;

            // Check if the target position is occupied by the player
            if (IsTileOccupiedByPlayer(targetY, targetX))
            {
                //Debug.WriteLine($"Construct attacked player at: ({targetY}, {targetX})");
                player.healthSystem.TakeDamage(damage);
                if (player.healthSystem.wasHurt)
                {
                    uiManager.AddEventLogMessage($"{enemyType} attacked {player.name}");
                    player.healthSystem.wasHurt = false;
                }
                else
                    uiManager.AddEventLogMessage($"{player.name}'s armor is too strong for {enemyType}");
                return; // Do not move into the player's position
            }

            // Check if the target position is occupied by another enemy
            if (IsPositionOccupied(targetY, targetX, this))
                return; // Do nothing if there's another enemy

            else if (!IsPositionOccupied(targetY, targetX, this) && !gameMap.isInStore(targetY, targetX) && gameMap.IsWalkable(targetY, targetX))
            {
                // If the position is free, update the enemy's position
                position.x = targetX;
                position.y = targetY;
            }
        }
    }
}