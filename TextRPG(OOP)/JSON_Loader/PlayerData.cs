using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    public class PlayerData
    {
        public Dictionary<string, PlayerAttributes> PlayerAttributes;
    }

    public class PlayerAttributes
    {
        public int BaseHP {  get; set; }
        public int BaseDamage { get; set; }
        public int StartingCoins { get; set; }
        public string Name { get; set; }
    }
}
