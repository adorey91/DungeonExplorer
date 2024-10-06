using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Health : Item
    {
        public Health()
        {
            gainAmount = Settings.healthGain;
            avatar = Settings.healthChar;
            color = Settings.healthColor;
            itemType = "Health";
            cost = Settings.healthCost;
        }
    }
}
