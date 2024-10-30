using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    public class ItemData
    {
        public Dictionary<string, ItemAttributes> Items { get; set; }
    }

    public class ItemAttributes
    {
        public string Type { get; set; }
        public int ItemGain { get; set; }
        public string ItemColor { get; set; }
        public char ItemChar { get; set; }
        public int ItemCost { get; set; }
        public int ItemDamage { get; set; }
    }
}
