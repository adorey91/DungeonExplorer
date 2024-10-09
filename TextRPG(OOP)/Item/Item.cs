using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    /// <summary>
    /// Any and all types of pickups
    /// </summary>
    internal abstract class Item
    {
        public int gainAmount;
        public bool isActive;
        public bool isCollected = false;
        public Position position;
        public Char avatar;
        public string itemType;
        public int cost;
        public bool isForStore;

        public ConsoleColor color;

        public Item()
        {
            isCollected = false;
        }

        public abstract void Apply(Player player, UIManager uiManager, QuestManager questManager);
    }
}
