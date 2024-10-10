using System;
using System.Diagnostics;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal abstract class Enemy : Character
    {

        public int maxHP;
        public int enemyMaxX;
        public int enemyMaxY;
        public int prevX;
        public int prevY;
        public bool beenCounted;

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
            Enemy enemyAtPosition = enemyManager.GetEnemyAtPosition(x, y);
            if(enemyAtPosition != null && enemyAtPosition.healthSystem.IsAlive && enemyAtPosition != currentEnemy)
                return true;

            // Check if there's an item in the position using GetItemAtPosition
            Item itemAtPosition = itemManager.GetItemAtPosition(x, y); // Call to GetItemAtPosition
            if (itemAtPosition != null && !itemAtPosition.isCollected)
                return true; // Position is occupied by an item
            
            return false; // Position is free
        }
    }
}