﻿using System;
using System.Diagnostics;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal abstract class Enemy : Character
    {

        public int maxHP;
        public int enemyNumber;
        public int enemyMaxX;
        public int enemyMaxY;
        public string enemyType;
        public int levelNumber;
        public ConsoleColor avatarColor;
        public Random moveRoll;  // Now a shared Random instance
        public Player player;
        public EnemyManager enemyManager;
        public ItemManager itemManager;
        public GameManager gameManager;
        public UIManager uiManager;

        public Enemy(ConsoleColor color, Random sharedRandom, GameManager gameManager)
        {
            moveRoll = sharedRandom;  // Use the shared Random instance
            this.avatarColor = color;
            this.gameManager = gameManager;
            this.player = gameManager.player;
            this.enemyManager = gameManager.enemyManager;
            this.itemManager = gameManager.itemManager; 
            this.uiManager = gameManager.uiManager;
        }

        public void SetEnemyMaxPosition(Map map)
        {
            int mapX = map.activeMap.GetLength(1);
            int mapY = map.activeMap.GetLength(0);
            enemyMaxX = mapX - 1;
            enemyMaxY = mapY - 1;
        }

        protected int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        // This will update the enemy position
        public abstract void Update(Map gameMap);



        // Method to check if the tile is occupied by the player
        public bool IsTileOccupiedByPlayer(int y, int x)
        {
            return player.position.x == x && player.position.y == y;
        }

        public bool IsPositionOccupied(int y, int x, Enemy currentEnemy)
        {
            // Check if the enemy is alive and at the target position, and it's not the current enemy trying to move
            foreach (var enemy in enemyManager.enemies)
            {
                if (enemy != currentEnemy && enemy.healthSystem.IsAlive && (enemy.position.x == x && enemy.position.y == y))
                    return true; // Position is occupied by another enemy
            }

            // Check if there's an item in the position
            foreach (var item in itemManager.items)
            {
                if (!item.isCollected && (item.position.x == x && item.position.y == y))
                    return true; // Position is occupied by an item
            }

            return false; // Position is free
        }
    }
}