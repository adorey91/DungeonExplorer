using System;
using System.Collections.Generic;
using System.Diagnostics;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    /// <summary>
    /// Places and manage all items on each floor. 
    /// </summary>
    internal class ItemManager
    {
        Dictionary<int, List<Item>> levelItems;
        public Map gameMap;
        public GameManager GameManager;
        public Player player;

        public ItemManager()
        {
            levelItems = new Dictionary<int, List<Item>>();
        }

        public void Initialize(GameManager gameManager)
        {
            this.GameManager = gameManager;
            this.gameMap = gameManager.gameMap;
            this.player = gameManager.player;
        }

        public void AddItems()
        {
            if (!levelItems.ContainsKey(gameMap.levelNumber))
            {
                levelItems[gameMap.levelNumber] = new List<Item>();
                AddItemToList("Armor", gameMap.levelNumber);
                AddItemToList("Health", gameMap.levelNumber);
                AddItemToList("Coin", gameMap.levelNumber);
                AddItemToList("Spike", gameMap.levelNumber);
                AddItemToList("Chalice", gameMap.levelNumber);
                AddItemToList("Sword", gameMap.levelNumber);
            }
        }

        private void AddItemToList(string type, int levelNumber)
        {
            List<Position> itemPositions = new List<Position>();
            switch (type)
            {
                case "Armor": itemPositions = gameMap.FindPositionsByChar('+'); break;
                case "Health": itemPositions = gameMap.FindPositionsByChar('"'); break;
                case "Coin": itemPositions = gameMap.FindPositionsByChar('@'); break;
                case "Spike": itemPositions = gameMap.FindPositionsByChar('*'); break;
                case "Sword": itemPositions = gameMap.FindPositionsByChar('t'); break;
                case "Chalice": itemPositions = gameMap.FindPositionsByChar('$'); break;
            }

            if (itemPositions.Count == 0)
                return;

            foreach (var pos in itemPositions)
            {
                Item item = null;

                switch (type)
                {
                    case "Armor": item = new Armor(); break;
                    case "Health": item = new Health(); break;
                    case "Coin": item = new Coin(); break;
                    case "Spike": item = new Spike(); break;
                    case "Sword": item = new Sword(); break;
                    case "Chalice": item = new Chalice(); break;
                }

                if (item != null)
                {
                    item.position = pos;
                    // Add the item to the list of items for this level in the dictionary
                    levelItems[levelNumber].Add(item);
                }
            }
        }

        public void Update()
        {
            if (gameMap.levelChange)
            {
                AddItems();
            }
            else if (gameMap.inStore)
            {
                AddItems();
            }
        }

        public void Draw()
        {
            if (levelItems.ContainsKey(gameMap.levelNumber))
            {
                foreach (var item in levelItems[gameMap.levelNumber])
                {
                    if (!item.isCollected) // Skip collected items
                    {
                        Console.SetCursorPosition(item.position.y, item.position.x);
                        Console.ForegroundColor = item.color;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(item.avatar);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.SetCursorPosition(item.position.y, item.position.x);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(item.avatar);
                        Console.ResetColor();
                    }
                }
            }
        }

        

        public Item GetItemAtPosition(int x, int y)
        {
            // Check if the current level exists in the dictionary
            if (levelItems.ContainsKey(gameMap.levelNumber))
            {
                // Get the list of items for the current level
                var itemsAtCurrentLevel = levelItems[gameMap.levelNumber];

                foreach (var item in itemsAtCurrentLevel)
                {
                    // Check if the item's position matches and it is not collected
                    if ((item.position.x == x && item.position.y == y) && !item.isCollected)
                    {
                        return item; // Return the item at the position
                    }
                }
            }

            return null; // No item at the position
        }

    }
}