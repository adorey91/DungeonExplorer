using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Spike : Item
    {
        public Spike()
        {
            gainAmount = Settings.spikeDamage;
            avatar = Settings.spikeChar;
            color = Settings.spikeColor;
            itemType = "Spike";
        }
    }
}
