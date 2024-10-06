using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Chalice : Item
    {
        public Chalice()
        {
            avatar = Settings.finalLootChar;
            color = Settings.finalLootColor;
        }
    }
}
