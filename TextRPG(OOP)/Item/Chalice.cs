using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal class Chalice : Item
    {
        public Chalice(ItemAttributes attributes)
        {
            avatar = attributes.ItemChar;
            color = ConvertToConsoleColor(attributes.ItemColor);
        }

        public override void Apply(Player player, UIManager uiManager, QuestManager questManager)
        {
            isCollected = true;
        }
    }
}
