using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Sword: Item
    {
        public Sword()
        {
            gainAmount = Settings.swordGain;
            avatar = Settings.swordChar;
            color = Settings.swordColor;
            itemType = "Sword";
            cost = Settings.swordCost;
        }
    }
}