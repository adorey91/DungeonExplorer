using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace TextRPG_OOP_
{
    /// <summary>
    /// Build and manages the game map
    /// </summary>
    internal class Map
    {
        public char[,] activeMap;
        public static string[] floorMap;
        public static string path;
        private string[] mapTextFiles = new string[] { "Maps\\\\Floor1Map.txt", "Maps\\\\Floor2Map.txt", "Maps\\\\Floor3Map.txt", "Maps\\StoreMap.txt" };
        public int levelNumber = 0;
        private int previousLevel;
        public int storeLevel = 3;

        public bool changeLevel = false;
        public bool inStore = false;

        public bool foundstart;

        public Map()
        {
            Initialize();
        }

        public void Initialize()
        {
            SetMapPath(levelNumber);
            LoadMap();
        }

        public void Update()
        {
            if (changeLevel)
            {
                ChangeLevel();
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);

            // Print out the dimensions of the map before drawing
            //Console.WriteLine($"Active Map Size: {activeMap.GetLength(0)} x {activeMap.GetLength(1)}");

            for (int i = 0; i < activeMap.GetLength(0); i++)
            {
                for (int j = 0; j < activeMap.GetLength(1); j++)
                {
                    if (j < activeMap.GetLength(1))
                    {
                        if (activeMap[i, j] == '\0') // Default value check
                            continue;

                        // Draw the tile
                        if (i == 0 && j < 9)
                            Console.Write(activeMap[i, j]); // Directly write the tile without color
                        else
                        {
                            DrawTile(activeMap[i, j]); // Apply color normally for other tiles

                            if (foundstart)
                            {
                                foundstart = false;
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public void LoadMap()
        {
            floorMap = File.ReadAllLines(path);
            activeMap = new char[floorMap.Length, floorMap[0].Length];

            for (int i = 0; i < floorMap.Length; i++)
            {
                if (floorMap[i].Length != floorMap[0].Length)
                {
                    throw new Exception($"Inconsistent row length at row {i + 1}. Expected length: {floorMap[0].Length}, Actual length: {floorMap[i].Length}");
                }
            }

            for (int i = 0; i < activeMap.GetLength(0); i++)
            {
                for (int j = 0; j < activeMap.GetLength(1); j++)
                {
                    activeMap[i, j] = floorMap[i][j];
                }
            }
        }

        /// <summary>
        /// Draws individual tile based on the character.
        /// </summary>
        /// <param name="tile"></param>
        private void DrawTile(char tile)
        {
            switch (tile)
            {
                case Settings.dungeonWall: DrawColorTile(ConsoleColor.DarkGray, ConsoleColor.DarkGray, tile); break;
                case Settings.dungeonFloor: DrawColorTile(ConsoleColor.Gray, ConsoleColor.Gray, tile); break;
                case Settings.storeFloor: DrawColorTile(ConsoleColor.Gray, ConsoleColor.Gray, tile); break;
                case Settings.startingPosition: foundstart = true; DrawColorTile(ConsoleColor.Gray, ConsoleColor.Gray, tile); break;
                case Settings.stairs: DrawColorTile(ConsoleColor.DarkGray, ConsoleColor.Gray, Settings.newStairsChar); break;
                case Settings.storeChar: DrawColorTile(ConsoleColor.Red, ConsoleColor.DarkGray, tile); break;
                default: DrawColorTile(ConsoleColor.Gray, ConsoleColor.Gray, tile); break;
            }
        }

        private void DrawColorTile(ConsoleColor frontColor, ConsoleColor backColor, char tile)
        {
            Console.ForegroundColor = frontColor;
            Console.BackgroundColor = backColor;
            Console.Write(tile);
            Console.ResetColor();
        }

        public Position FindPlayerStartPosition()
        {
            for (int row = 0; row < activeMap.GetLength(0); row++)
            {
                for (int col = 0; col < activeMap.GetLength(1); col++)
                {
                    if (activeMap[row, col] == '=')
                    {
                        return new Position(row, col); // Return a Position object
                    }
                }
            }
            throw new Exception("Start position not found on the map.");
        }

        /// <summary>
        /// Sets the map path based on the level number
        /// </summary>
        /// <param name="level"></param>
        private void SetMapPath(int level)
        {
            inStore = false;
            path = mapTextFiles[level];
        }

        public void ChangeLevel()
        {
            levelNumber++;
            SetMapPath(levelNumber);
            LoadMap();
        }

       #region ChecksForMap
        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < activeMap.GetLength(1) && y >= 0 && y < activeMap.GetLength(0);
        }

        public bool IsWalkable(int x, int y)
        {
            return activeMap[y, x] != Settings.dungeonWall;
        }

        public bool isInStore(int x, int y)
        {
            return activeMap[y,x] == Settings.storeFloor;
        }

        public bool IsStairs(int x, int y)
        {
            return activeMap[y, x] == Settings.stairs;
        }

        

        /// <summary>
        /// Finds starting positions for items, enemies & map
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public List<Position> FindPositionsByChar(char character)
        {
            List<Position> positions = new List<Position>();

            for (int row = 0; row < activeMap.GetLength(0); row++)
            {
                for (int col = 0; col < activeMap.GetLength(1); col++)
                {
                    if (activeMap[row, col] == character)
                        positions.Add(new Position(row, col)); // Add each found position to the list
                }
            }
            // Do not throw an exception if no positions are found, just return an empty list
            return positions;
        }
        #endregion
    }
}