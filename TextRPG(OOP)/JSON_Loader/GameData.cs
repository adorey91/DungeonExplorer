using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    public class GameData
    {
        public Dictionary<string, GameSettings> gameSettings;
    }

    public class GameSettings
    {
        public char Character { get; set; }
        public char NewCharacter { get; set; }
        public string TileColor { get; set; }
    }
}
