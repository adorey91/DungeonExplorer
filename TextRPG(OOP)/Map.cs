using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Map
    {
        public string path;
        string path1 = @"Floor1Map.txt";
        string path2 = @"Floor2Map.txt";
        string path3 = @"Floor3Map.txt";
        string[] floorMap;
        public char[,] dungeonMap;
        int levelNumber;
        bool levelChanged;
    }
}
