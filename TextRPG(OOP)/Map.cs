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
    internal class Map
    {
        public static string path;
        public static string path1 = @"Floor1Map.txt";
        public static string path2 = @"Floor2Map.txt";
        public static string path3 = @"Floor3Map.txt";
        public static string[] floorMap;
        public char[,] activeMap;
        public int levelNumber;
        public bool levelChanged;
        public char dungeonFloor = ((char)18); // ↕
        public char dungeonWall = ((char)35); // #
        public char spikeTrap = ((char)23); // ↨
        public char player = ((char)2);  // ☻
        public char stairsDown = ((char)30); // ▲
        public char startPos = ((char)31); // ▼
        public char finalLoot = ((char)165); 
        public char coin = ((char)164); 
        public char healthPickup = ((char)3); // ♥
        public char armorPickup = ((char)21); // §
        static char enemy1 = ((char)4);
        static char enemy2 = ((char)6);
        static char enemy3 = ((char)5);
        static char enemy4 = ((char)127);
        public static int mapX;
        public static int mapY;
        public int playerX;
        public int playerY;
        public int playerMaxX;
        public int playerMaxY;
        public List<Character> characters;
        public int enemyCount;
        public int index;
        public EnemyManager enemyManager;
        public Player mainPlayer;
        public ItemManager itemManager;
        public Map(ItemManager IM) //Constructor
        {
            Initialization();
            itemManager = IM;
            //Console.Write("Initialized Map");
        }
        public void Initialization()
        {
            //Console.Write("Initializing Map");
            //Console.ReadKey();
            enemyCount = 0;
            levelNumber = 1;
            path = path1;
            characters = new List<Character>();
            floorMap = File.ReadAllLines(path);
            activeMap = new char[floorMap.Length, floorMap[0].Length];
            mapX = activeMap.GetLength(1);
            mapY = activeMap.GetLength(0);
            MakeDungeonMap();
        }
        public void Start(Player player, EnemyManager em)
        {
            enemyManager = em;
            mainPlayer = player;
            AddToCharacterList(mainPlayer);
            SetPlayerSpawn(mainPlayer);
            GetPlayerMaxPosition(mainPlayer);
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
        public void Update()
        {
            
        }
        public void Draw()
        {
            DrawMap();
            DrawHUD();
            DrawEnemyLegend();
            DrawPlayerToMap(characters[0].position.x, characters[0].position.y);
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
                        mainPlayer.SetPlayerPosition(playerX,playerY);
                    }
                    if(tile == '!' && levelChanged == false || tile == '?' && levelChanged == false
                    || tile == '&' && levelChanged == false || tile == '^' && levelChanged == false)
                    {
                        if(tile == '!')
                        {
                            enemyManager.AddEnemiesToList("Slime");
                            enemyManager.enemiesList[enemyCount].position.x = x;
                            enemyManager.enemiesList[enemyCount].position.y = y;
                            AddToCharacterList(enemyManager.enemiesList[enemyCount]);
                            activeMap[y, x] = '-';
                            enemyCount += 1;
                        }
                        if(tile == '?')
                        {
                            enemyManager.AddEnemiesToList("Living Armor");
                            enemyManager.enemiesList[enemyCount].position.x = x;
                            enemyManager.enemiesList[enemyCount].position.y = y;
                            AddToCharacterList(enemyManager.enemiesList[enemyCount]);
                            activeMap[y, x] = '-';
                            enemyCount += 1;
                        }
                        if(tile == '&')
                        {
                            enemyManager.AddEnemiesToList("Kobald");
                            enemyManager.enemiesList[enemyCount].position.x = x;
                            enemyManager.enemiesList[enemyCount].position.y = y;
                            AddToCharacterList(enemyManager.enemiesList[enemyCount]);
                            activeMap[y, x] = '-';
                            enemyCount += 1;
                        }
                    }
                    //Console.Write("test");
                    DrawTile(tile);
                }
                Console.Write("\n");
            }
        }
        // Sets player spawn point
        public void SetPlayerSpawn(Character player)
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
        public void GetPlayerMaxPosition(Player player)
        {
            playerMaxX = player.position.maxX;
            playerMaxY = player.position.maxY;
        }
        public void DrawEnemiesToMap(List<Enemy> eList)
        {
            for(int i = 0; i < eList.Count(); i++)
            {
                Console.SetCursorPosition(eList[i].position.x,eList[i].position.y);
                DrawEnemy(eList[i].enemyType);
            }
        }
        public void DrawEnemy(string enemyType)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            if(enemyType == "Slime")
            {
               Console.Write(enemy1);
            }
            if(enemyType == "Living Armor")
            {
               Console.Write(enemy2);
            }
            if(enemyType == "Kobald")
            {
               Console.Write(enemy3);
            }
            SetColorDefault();
        }
        //Code for tiles color and ascii
        public void DrawFloor()
        {
            // used to draw a floor tile
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(dungeonFloor);
            SetColorDefault();
        }
        public void DrawWall()
        {
            // used to draw a wall tile
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(dungeonWall);
            SetColorDefault();
        }
        public void DrawSpikes()
        {
            // used to draw a spikes tile
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(spikeTrap);
            SetColorDefault();
        }
        public void DrawFinalLoot()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(finalLoot);
            SetColorDefault();
        }
        public void DrawCoin()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(coin);
            SetColorDefault();
        }
        public void SetColorDefault()
        {
            // sets console color back to default. 
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void DrawArmor()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(armorPickup);
            SetColorDefault();
        }
        public void DrawStairsDown()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(stairsDown);
            SetColorDefault();
        }
        public void DrawHealthPickup()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(healthPickup);
            SetColorDefault();
        }
        public void DrawPlayer()
        {
            // used to draw the player
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(player);
            SetColorDefault();
        }
        //Determines what tile is what during print.
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
            if(tile == '+')
            {
                DrawArmor();
                return;
            }
            else
            {
                Console.Write(tile);
            }
        }
        public void ChangeLevels()
        {
            levelChanged = false;
            // used to change maps
            if(levelNumber == 1)
            {
                path = path1;
                floorMap = File.ReadAllLines(path);
            }
            if(levelNumber == 2)
            {
                levelNumber = 2;
                path = path2;
                floorMap = File.ReadAllLines(path);
            }
            if(levelNumber == 3)
            {
                levelNumber = 3;
                path = path3;
                floorMap = File.ReadAllLines(path);
            }
            if(levelNumber > 3 || levelNumber <= 0)
            {
                Console.Clear();
                Console.WriteLine("Level Out of range, Loading level 1");
                path = path1;
                floorMap = File.ReadAllLines(path);
            }
            MakeDungeonMap();
            DrawMap();
            RestCharacters();
        }
        public bool CretureInTarget(int y, int x)
        {
            bool IsTarget = false;
            for(index = 0; index < characters.Count(); index++)
            {
                if(x == characters[index].position.x && y == characters[index].position.y)
                {
                    IsTarget = true;
                    return IsTarget;
                }
                if(index > characters.Count() || index < 0)
                {
                    index = 0;
                }
            }
            return IsTarget;
        }
        public void AddToCharacterList(Character character)
        {
            characters.Add(character);
        }
        public bool CheckTile(int y, int x)
        {
            bool CanMove = false;
            if(activeMap[y,x] == dungeonWall)
            {
                CanMove = false;
            }
            else
            {
                CanMove = true;
            }
            return CanMove;
        }
        public void RestCharacters()
        {
            for(int i = 0; i < characters.Count(); i++)
            {
                characters[i].healthSystem.IsAlive = true;
                if(i == 0)
                {
                    SetPlayerSpawn(characters[0]);
                }
                if(i > 0)
                {

                }
            }
        }
        public void DrawEnemyLegend()
        {
            Console.WriteLine();
            DrawEnemy("Slime");
            Console.WriteLine(" = Slime");
            DrawEnemy("Living Armor");
            Console.WriteLine(" = Living Armor");
            DrawEnemy("Kobald");
            Console.WriteLine(" = Kobald");
        }
        void DrawHUD() //Add to a UIManager Class
        {
            Debug.WriteLine("Drawing HUD");
            string enemyHUDString = "{0} has Hp: {1} Armor: {2}     ";
            string FormatString = "HP: {0}  Damage: {1}  Coins: {2}  Armor: {3}    ";
            Console.WriteLine(string.Format(FormatString, mainPlayer.healthSystem.health, mainPlayer.playerDamage, mainPlayer.playerCoins, mainPlayer.healthSystem.armor));
            if(mainPlayer.enemyHitName == "")
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(string.Format(enemyHUDString,mainPlayer.enemyHitName,mainPlayer.enemyHitHealth,mainPlayer.enemyHitArmor));
            }
        }
        
    }
}
