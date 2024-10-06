using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    /// <summary>
    /// Places and manage all items on each floor. 
    /// </summary>
    internal class ItemManager
    {
        public List<Item> items;
        public List<Item> storeItems;
        public Map gameMap;
        public GameManager GameManager;
        public Player player;
        private Store store;

        public ItemManager()
        {
            items = new List<Item>();
        }

        public void Initialize(GameManager gameManager)
        {
            this.GameManager = gameManager;
            this.gameMap = gameManager.gameMap;
            this.player = gameManager.player;
            this.store = gameManager.store;
        }

        public void AddItems()
        {
            AddItemToList("Armor", gameMap.levelNumber, items);
            AddItemToList("Health", gameMap.levelNumber, items);
            AddItemToList("Coin", gameMap.levelNumber, items);
            AddItemToList("Spike", gameMap.levelNumber, items);
            AddItemToList("Chalice", gameMap.levelNumber, items);
            AddItemToList("Sword", gameMap.levelNumber, items);
        }

        private void AddItemToList(string type, int levelNumber, List<Item> itemList)
        {
            List<Position> itemPositions = new List<Position>();
            if (type == "Armor")
                itemPositions = gameMap.FindPositionsByChar('+'); //  Find all '+' positions for armor
            else if (type == "Health")
                itemPositions = gameMap.FindPositionsByChar('"'); // Find all '"' positions for health
            else if (type == "Coin")
                itemPositions = gameMap.FindPositionsByChar('@'); // Find all '' positions for coins
            else if (type == "Spike")
                itemPositions = gameMap.FindPositionsByChar('*'); // Find all '' positions for coins
            else if (type == "Sword")
                itemPositions = gameMap.FindPositionsByChar('t');
            else if (type == "Chalice")
                itemPositions = gameMap.FindPositionsByChar('$');

            if (itemPositions.Count == 0)
                return;

            foreach (var pos in itemPositions)
            {
                Item item = null;

                switch (type)
                {
                    case "Armor": new Armor(); break;
                    case "Health": new Health(); break;
                    case "Coin": new Coin(); break;
                    case "Spike": new Spike(); break;
                    case "Sword": new Sword(); break;
                    case "Chalice": new Chalice(); break;
                }

                if (item != null)
                {
                    item.position = pos;
                    itemList.Add(item);
                }
            }
        }

        public void Update()
        {
            if (gameMap.levelChange)
            {
                items.Clear();

                AddItems();
            }
            else if (gameMap.inStore)
            {
                AddItemToList("Armor", gameMap.levelNumber, store.itemsToBuy);
                AddItemToList("Health", gameMap.levelNumber, store.itemsToBuy);
                AddItemToList("Sword", gameMap.levelNumber, store.itemsToBuy);

                foreach (var item in store.itemsToBuy)
                {
                    if (store.CanPlayerBuyItem())
                    {

                    }
                }
            }
            else
            {
                List<Item> itemsToRemove = new List<Item>();
                foreach (var item in items)
                {
                    if (item.isCollected)
                    {
                        itemsToRemove.Add(item);
                    }
                }

                foreach (var item in itemsToRemove)
                {
                    items.Remove(item);
                }

            }
        }

        public void Draw()
        {
            if (gameMap.inStore)
            {
                foreach (var item in store.itemsToBuy)
                {
                    Console.SetCursorPosition(item.position.y, item.position.x);
                    Console.ForegroundColor = item.color;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(item.avatar);
                    Console.ResetColor();
                }
            }
           else
            {
                foreach (var item in items)
                {
                    if (!item.isCollected)
                    {
                        Console.SetCursorPosition(item.position.y, item.position.x);
                        Console.ForegroundColor = item.color;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(item.avatar);
                        Console.ResetColor();
                    }
                }
            }
        }

        public Item GetItemAtPosition(int x, int y)
        {
            foreach (var item in items)
            {
                if ((item.position.x == x && item.position.y == y) && !item.isCollected)
                {
                    return item; // Return the item at the position
                }
            }
            return null; // No item at the position
        }
    }
}
