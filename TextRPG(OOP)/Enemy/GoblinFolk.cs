﻿using System;
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
        private int fleeRange = 5;

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
            // Calculate the distance to the player
            int rangeX = position.x - player.position.x;
            int rangeY = position.y - player.position.y;

            prevX = position.x;
            prevY = position.y;

            // Check if the player is within the flee range
            if (Math.Abs(rangeX) < fleeRange && Math.Abs(rangeY) < fleeRange)
            {
                MoveAwayFromPlayer(rangeX, rangeY, gameMap);
            }
        }

        private void MoveAwayFromPlayer(int rangeX, int rangeY, Map gameMap)
        {
            // Move horizontally away from the player
            if (rangeX > 0) Move(1, 0, gameMap); // Flee right (player is to the left)
            else if (rangeX < 0) Move(-1, 0, gameMap); // Flee left (player is to the right)

            // Move vertically away from the player
            if (rangeY > 0) Move(0, 1, gameMap); // Flee down (player is above)
            else if (rangeY < 0) Move(0, -1, gameMap); // Flee up (player is below)
        }

        private void Move(int dx, int dy, Map gameMap)
        {
            int targetX = position.x + dx; // New X position
            int targetY = position.y + dy; // New Y position

            // Check if the target position is within the bounds of the map
            if (!gameMap.IsWithinBounds(targetY, targetX))
            {
                //Debug.WriteLine($"Goblin attempted to move out of bounds: ({targetY}, {targetX})");
                return;
            }

            // Check if the target position is occupied by the player
            if (IsTileOccupiedByPlayer(targetY, targetX))
            {
                // Attack player if Goblin tries to move into the player's position
                player.healthSystem.TakeDamage(damage);
                uiManager.AddEventLogMessage($"{player.name} attacked by {enemyType}");
                return; // Do not move into the player's position
            }

            // Check if the target position is occupied by another enemy
            if (IsPositionOccupied(targetY, targetX, this))
            {
                //Debug.WriteLine($"Goblin found another enemy at ({targetX}, {targetY}) and will do nothing.");
                return; // Do nothing if there's another enemy
            }

            // If the target position is walkable, update the position
            if (gameMap.IsWalkable(targetY, targetX))
            {
                position.x = targetX;
                position.y = targetY;
                //Debug.WriteLine($"Goblin moved to ({position.x}, {position.y})");
            }
            else
            {
                //Debug.WriteLine($"Goblin attempted to move to an unwalkable tile: ({targetY}, {targetX})");
            }
        }
    }
}