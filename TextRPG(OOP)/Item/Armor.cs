using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Armor : Item
    {
        public Armor()
        {
            gainAmount = Settings.armorGain;
            avatar = Settings.armorChar;
            color = Settings.armorColor;
            itemType = "Armor";
        }
    }
}
