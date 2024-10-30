using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    public class QuestData
    {
        public Dictionary<string, QuestAttributes> Quest {  get; set; }
    }

    public class QuestAttributes
    {
        public int TargetAmount { get; set; }
        public string TargetItem { get; set; }
        public string TargetEnemy { get; set; }
        public string Description { get; set; }
    }
}
