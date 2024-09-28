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
        public static string path1 = @"Floor1Map.txt";
        public static string path2 = @"Floor2Map.txt";
        public static string path3 = @"Floor3Map.txt";
        public int levelNumber = 1;
        public char dungeonWall = ((char)35); // Wall '#'
        public char dungeonFloor = '-'; // Floor '-'

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

        public void LoadMap()
        {
            floorMap = File.ReadAllLines(path);
            activeMap = new char[floorMap.Length, floorMap[0].Length];

            for (int i = 1; i < floorMap.Length; i++) // Initialize from the second row
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

        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < activeMap.GetLength(1) && y >= 0 && y < activeMap.GetLength(0);
        }

        public bool IsWalkable(int x, int y)
        {
            return activeMap[y, x] != dungeonWall;
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

        private void DrawTile(char tile)
        {
            switch (tile)
            {
                case '#': DrawTile(ConsoleColor.DarkGray, ConsoleColor.DarkGray, tile); break;
                case '-': DrawTile(ConsoleColor.Gray, ConsoleColor.Gray, tile); break;
                case '=':
                    foundstart = true;
                    DrawTile(ConsoleColor.Red, ConsoleColor.Gray, tile);
                    break;
                default: DrawTile(ConsoleColor.Gray, ConsoleColor.Gray, tile); break;
            }
        }

        private void DrawTile(ConsoleColor frontColor, ConsoleColor backColor, char tile)
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

        public List<Position> FindPositionsByChar(char c)
        {
            List<Position> positions = new List<Position>();

            for (int row = 0; row < activeMap.GetLength(0); row++)
            {
                for (int col = 0; col < activeMap.GetLength(1); col++)
                {
                    if (activeMap[row, col] == c)
                    {
                        positions.Add(new Position(row, col)); // Add each found position to the list
                    }
                }
            }

            // Do not throw an exception if no positions are found, just return an empty list
            return positions;
        }



        private void SetMapPath(int levelNumber)
        {
            if (levelNumber == 1)
                path = path1;
            else if (levelNumber == 2)
                path = path2;
            else if (levelNumber == 3)
                path = path3;
            else
            {
                Console.WriteLine("Invalid level number. Loading level 1.");
                path = path1;
            }
        }
    }
}