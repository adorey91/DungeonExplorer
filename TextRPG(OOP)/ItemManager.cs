using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class ItemManager
    {
        public List<Item> items;
        public Map gameMap;
        public ItemManager()
        {
            items = new List<Item>();
        }
        public void AddItemToList(string type, int x, int y)
        {
            Item item = new Item(type,x,y,gameMap);
            items.Add(item);
        }
        public void Start(Map map)
        {
            gameMap = map;
        }
        public void ClearItemList()
        {
            items.Clear();
        }
        public void Draw()
        {
            DrawItemsToMap();
        }
        public void DrawItemsToMap()
        {
            for(int i = 0; i < items.Count(); i++)
            {
                if(items[i].isActive == true)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = items[i].color;
                    Console.SetCursorPosition(items[i].position.x, items[i].position.y);
                    Console.Write(items[i].item);
                }
            }
        }
        public void Update(Player player)
        {
            ChckItemPositions(player);
        }
        public void ChckItemPositions(Player player)
        {
            for(int i = 0; i < items.Count(); i++)
            {
                if(items[i].position.x == player.position.x && items[i].position.y == player.position.y)
                {
                    if(items[i].itemType == "Coin")
                    {
                        player.playerCoins += items[i].gainAmount;
                        items[i].isActive = false;    
                    }
                    if(items[i].itemType == "Health Pickup" && player.healthSystem.health != player.PlayerMaxHP)
                    {
                        player.healthSystem.health += items[i].gainAmount;
                        items[i].isActive = false;  
                    }
                    if(items[i].itemType == "Armor Pickup")
                    {
                        player.healthSystem.armor += items[i].gainAmount;
                        items[i].isActive = false;  
                    }
                }
            }
        }
    }
}
