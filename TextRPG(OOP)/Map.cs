using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace TextRPG_OOP_
{
    internal class Map
    {
        public static string path;
        public static string path1 = @"Map.txt";
        public static string path2 = @"Floor2Map.txt";
        public static string path3 = @"Floor3Map.txt";
        public static string[] floorMap;
        public char[,] activeMap;
        int levelNumber;
        bool levelChanged;
        public static char dungeonFloor = ((char)18);
        public static char dungeonWall = ((char)35);
        public static char spikeTrap = ((char)23);
        public static char player = ((char)2);
        public static char stairsDown = ((char)30);
        public static char startPos = ((char)31);
        public static char finalLoot = ((char)165);
        public static char coin = ((char)164);
        public static char healthPickup = ((char)3);
        static char enemy1 = ((char)4);
        static char enemy2 = ((char)6);
        static char enemy3 = ((char)5);
        static char enemy4 = ((char)127);
        public static int mapX;
        public static int mapY;
        public int playerX;
        public int playerY;
        static int enemy1X;
        static int enemy1Y;
        static int enemy2X;
        static int enemy2Y;
        static int enemy3X;
        static int enemy3Y;
        static int enemy4X;
        static int enemy4Y;
        public Map() //Constructor
        {
            Initialization();
            //Console.Write("Initialized Map");
        }
        public void Initialization()
        {
            //Console.Write("Initializing Map");
            //Console.ReadKey();
            //path = path1;
            floorMap = File.ReadAllLines(path1);
            activeMap = new char[floorMap.Length, floorMap[0].Length];
            mapX = activeMap.GetLength(1);
            mapY = activeMap.GetLength(0);
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
        public void DrawMap()
        {
            //Draws the map of the current level
            Console.SetCursorPosition(0,0);
            for(int y = 0; y < mapY; y++)
            {
                for(int x = 0; x < mapX; x++)
                {
                    char tile = activeMap[y,x];
                    if(tile == '=' && levelChanged == false)
                    {
                        playerX = x;
                        playerY = y-1;
                        levelChanged = true;
                        activeMap[y,x] = '#';
                    }
                    if(tile == '!' && levelChanged == false || tile == '?' && levelChanged == false
                    || tile == '&' && levelChanged == false || tile == '^' && levelChanged == false)
                    {
                        if(tile == '?')
                        {
                            enemy1X = x;
                            enemy1Y = y;
                        }
                        if(tile == '!')
                        {
                            enemy2X = x;
                            enemy2Y = y;
                        }
                        if(tile == '&')
                        {
                            enemy3X = x;
                            enemy3Y = y; 
                        }
                        if(tile == '^')
                        {
                            enemy4X = x;
                            enemy4Y = y;
                        }
                    }
                    //Console.Write("test");
                    DrawTile(tile);
                }
                Console.Write("\n");
            }
        }
        // Sets player spawn point
        public void SetPlayerSpawn(Player player)
        {
            player.position.x = playerX;
            player.position.y = playerY;
        }
        // Draws the player at current position.
        public void DrawPlayerToMap(int x , int y) // X = players position.x Y = players position.Y
        {
            Console.SetCursorPosition(x,y);
            DrawPlayer();
        }
        public void SetEnemySpawns(Enemy enemy1) /*Enemy enemy2,Enemy enemy3,Enemy enemy4*/
        {
            enemy1.position.x = enemy1X;
            enemy1.position.y = enemy1Y;
            // enemy2.position.x = enemy2X;
            // enemy2.position.y = enemy2Y;
            // enemy3.position.x = enemy3X;
            // enemy3.position.y = enemy3Y;
            // enemy4.position.x = enemy4X;
            // enemy4.position.y = enemy4Y;
        }
        public void DrawEnemyToMap(Enemy enemy)
        {

            Console.SetCursorPosition(enemy.position.x,enemy.position.y);
            DrawEnemy(enemy.enemyNumber);
        }
        static void DrawEnemy(int enemyNumber)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            if(enemyNumber > 4 || enemyNumber < 1)
            {
                enemyNumber = 1;
            }
            if(enemyNumber == 1)
            {
                Console.Write(enemy1);
            }
            if(enemyNumber == 2)
            {
                Console.Write(enemy2);
            }
            if(enemyNumber == 3)
            {
                Console.Write(enemy3);
            }
            if(enemyNumber == 4)
            {
                Console.Write(enemy4);
            }
            SetColorDefault();
        }
        //Code for tiles color and ascii
        static void DrawFloor()
        {
            // used to draw a floor tile
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(dungeonFloor);
            SetColorDefault();
        }
        static void DrawWall()
        {
            // used to draw a wall tile
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(dungeonWall);
            SetColorDefault();
        }
        static void DrawSpikes()
        {
            // used to draw a spikes tile
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(spikeTrap);
            SetColorDefault();
        }
        static void DrawFinalLoot()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(finalLoot);
            SetColorDefault();
        }
        static void DrawCoin()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(coin);
            SetColorDefault();
        }
        static void SetColorDefault()
        {
            // sets console color back to default. 
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void DrawStairsDown()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(stairsDown);
            SetColorDefault();
        }
        static void DrawHealthPickup()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(healthPickup);
            SetColorDefault();
        }
        static void DrawPlayer()
        {
            // used to draw the player
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(player);
            SetColorDefault();
        }
        //Determines what tile is what during print.
        public static void DrawTile(Char tile)
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
