using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    public class EnemyData
    {
        public Dictionary<string, EnemyAttributes> Enemies { get; set; }
    }

    public class EnemyAttributes
    {
        public string Type { get; set; }
        public int BaseHP { get; set; }
        public int BaseDamage { get; set; }
        public char Character { get; set; }
        public string SpawnColor { get; set; }
        public int FleeRange { get; set; }
        public int ChaseRange { get; set; }
    }
}
