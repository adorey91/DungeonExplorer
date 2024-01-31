using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextRPG_OOP_
{
    internal class Map
    {
        public string path;
        public string path1 = @"Map.txt";
        public string path2 = @"Floor2Map.txt";
        public string path3 = @"Floor3Map.txt";
        public string[] floorMap;
        public char[,] activeMap;
        int levelNumber;
        bool levelChanged;
        public char dungeonFloor = ((char)18);
        public char dungeonWall = ((char)35);
        public char spikeTrap = ((char)23);
        public char player = ((char)2);
        public char stairsDown = ((char)30);
        public char stairsUp = ((char)31);
        public char finalLoot = ((char)165);
        public char coin = ((char)164);
        public char healthPickup = ((char)3);
        public int mapX;
        public int mapY;
        public Map() //Constructor
        {
            Initialization();
        }
        public void Initialization()
        {
            //path = path1;
            floorMap = File.ReadAllLines(path1);
            activeMap = new char[floorMap.Length, floorMap[0].Length];
            MakeDungeonMap();
        }
        
        public void MakeDungeonMap()
        {
            for (int i = 0; i < floorMap.Length; i++)
            {
                for (int j = 0; j < floorMap[i].Length; j++)
                {
                    activeMap[i, j] = floorMap[i][j];
                }
            } 
        }
        void DrawFloor()
        {
            // used to draw a floor tile
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(dungeonFloor);
            SetColorDefault();
        }
        void DrawWall()
        {
            // used to draw a wall tile
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(dungeonWall);
            SetColorDefault();
        }
        void DrawSpikes()
        {
            // used to draw a spikes tile
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(spikeTrap);
            SetColorDefault();
        }
        void DrawFinalLoot()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(finalLoot);
            SetColorDefault();
        }
        void DrawCoin()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(coin);
            SetColorDefault();
        }
        void SetColorDefault()
        {
            // sets console color back to default. 
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        void DrawStairsDown()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(stairsDown);
            SetColorDefault();
        }
        void DrawHealthPickup()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(healthPickup);
            SetColorDefault();
        }
        void DrawPlayer(int PosX, int PosY)
        {
            // used to draw the player
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(player);
            SetColorDefault();
        }
        public void DrawTile(Char tile)
        {
            // draws the correct tile based on the floorMap
            if(tile == '-')
            {
                DrawFloor();
                return;
            }
            if(tile == '#')
            {
                DrawWall();
                return;
            }
            if(tile == '*')
            {
                DrawSpikes();
                return;
            }
            if(tile == '~')
            {
                DrawStairsDown();
                return;
            }
            if(tile == '=')
            {
                DrawWall();
                return;
            }
            if(tile == '$')
            {
                DrawFinalLoot();
                return;
            }
            if(tile == '@')
            {
                DrawCoin();
                return;
            }
            if(tile == '"')
            {
                DrawHealthPickup();
                return;
            }
            if(tile == '!')
            {
                DrawFloor();
                return;
            }
            if(tile == '?')
            {
                DrawFloor();
                return;
            }
            if(tile == '&')
            {
                DrawFloor();
                return;
            }
            if(tile == '^')
            {
                DrawFloor();
                return;
            }
            else
            {
                Console.Write(tile);
            }
        }
    }

}
