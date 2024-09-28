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
        public Map gameMap;
        public GameManager GameManager;
        public UIManager uiManager;
        public Player player;

        public ItemManager()
        {
            items = new List<Item>();
        }

        public void Initialize(GameManager gameManager)
        {
            this.GameManager = gameManager;
            this.gameMap = gameManager.gameMap;
            this.uiManager = gameManager.uiManager;
            this.player = gameManager.player;
        }

        public void AddItems()
        {
            AddItemToList("Armor", gameMap.levelNumber);
            AddItemToList("Health", gameMap.levelNumber);
            AddItemToList("Coin", gameMap.levelNumber);
            AddItemToList("Spike", gameMap.levelNumber);
        }

        private void AddItemToList(string type, int levelNumber)
        {
            List<Position> itemPositions = new List<Position>();
            if (type == "Armor")
                itemPositions = gameMap.FindPositionsByChar('+'); //  Find all '+' positions for armor
            else if(type == "Health")
                itemPositions = gameMap.FindPositionsByChar('"'); // Find all '"' positions for health
            else if(type == "Coin")
                itemPositions = gameMap.FindPositionsByChar('@'); // Find all '' positions for coins
            else if (type == "Spike")
                itemPositions = gameMap.FindPositionsByChar('*'); // Find all '' positions for coins

            if (itemPositions.Count == 0)
                return;

            foreach(var pos  in itemPositions)
            {
                Item item = null;
                if (type == "Armor") item = new Armor();
                else if (type == "Health") item = new Health();
                else if (type == "Coin") item = new Coin();
                else if (type == "Spike") item = new Spike();

                if(item != null)
                {
                    item.position = pos;
                    items.Add(item);
                }
            }
        }

        public void Update()
        {
            List<Item> itemsToRemove = new List<Item>();
            foreach (var item in items)
            {
                if (item.isCollected)
                {
                    if (item.itemType != "Spike")
                        uiManager.AddEventLogMessage($"{item.itemType} has been collected");

                    itemsToRemove.Add(item);
                }
            }

            foreach (var item in itemsToRemove)
            {
                items.Remove(item);
            }
        }

        public void Draw()
        {
            foreach (var item in items)
            {
                if(!item.isCollected)
                {
                    Console.SetCursorPosition(item.position.y, item.position.x);
                    Console.ForegroundColor = item.color;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(item.avatar);
                    Console.ResetColor();
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
