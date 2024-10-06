using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    /// <summary>
    /// Any and all types of pickups
    /// </summary>
    internal class Item
    {
        public int gainAmount;
        public bool isActive;
        public bool isCollected = false;
        public Position position;
        public Char avatar;
        public string itemType;
        public int cost;

        public ConsoleColor color;
        public int index;

        public Item()
        {
            isCollected = false;
        }
    }
}
