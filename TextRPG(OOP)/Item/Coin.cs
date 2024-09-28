using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Coin : Item
    {
        public Coin() 
        {
            gainAmount = Settings.coinGain;
            avatar = Settings.coinChar;
            color = Settings.coinColor;
            itemType = "Coin";
        }
    }
}
