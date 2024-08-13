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
        public char stairsDown = ((char)30); // ▲
        public char startPos = ((char)31); // ▼
        public char finalLoot = ((char)165); 
        public char coin = ((char)164); 
        public char healthPickup = ((char)3); // ♥
        public char armorPickup = ((char)21); // §
        static char enemy1 = ((char)4);
        static char enemy2 = ((char)6);
        static char enemy3 = ((char)5);
        public static int mapX;
        public static int mapY;
        public int playerX;
        public int playerY;
        public int playerMaxX;
        public int playerMaxY;
        public List<Character> characters;
        public int enemyCount;
        public int itemCount;
        public int index;
        public int itemIndex;
        public EnemyManager enemyManager;
        public Player mainPlayer;
        public ItemManager itemManager;
        public Map(ItemManager IM) //Constructor
        {
            Initialization();
            //Sets item manager from call in GameManager
            itemManager = IM;
            //Console.Write("Initialized Map");
        }
        public void Initialization()
        {
            //starts map building process.
            enemyCount = 0;
            levelNumber = 1;
            path = path1;
            characters = new List<Character>();
            floorMap = File.ReadAllLines(path);
            activeMap = new char[floorMap.Length, floorMap[0].Length];
            mapX = activeMap.GetLength(1);
            mapY = activeMap.GetLength(0);
            MakeDungeonMap();
            index = 0;
            itemIndex = 0;
        }
        public void Start(Player player, EnemyManager em)
        {
            //Sets player and Enemy manager from call in gameManager.
            enemyManager = em;
            mainPlayer = player;
            AddToCharacterList(mainPlayer);
            SetPlayerSpawn(mainPlayer);
            GetPlayerMaxPosition(mainPlayer);
        }
        public void MakeDungeonMap()
        {
            //Creates map based on active floor.
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
            //Updares values needed for map.
            index = 0;
            itemIndex = 0;
        }
        public void Draw()
        {
            //Call function to draw map/Hud/Legend
            DrawMap();
            DrawHUD();
            DrawEnemyLegend();
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
                            enemyManager.AddEnemiesToList("Plasmoid", levelNumber);
                            enemyManager.enemiesList[enemyCount].position.x = x;
                            enemyManager.enemiesList[enemyCount].position.y = y;
                            AddToCharacterList(enemyManager.enemiesList[enemyCount]);
                            activeMap[y, x] = '-';
                            enemyCount += 1;
                        }
                        if(tile == '?')
                        {
                            enemyManager.AddEnemiesToList("Construct", levelNumber);
                            enemyManager.enemiesList[enemyCount].position.x = x;
                            enemyManager.enemiesList[enemyCount].position.y = y;
                            AddToCharacterList(enemyManager.enemiesList[enemyCount]);
                            activeMap[y, x] = '-';
                            enemyCount += 1;
                        }
                        if(tile == '&')
                        {
                            enemyManager.AddEnemiesToList("GoblinFolk", levelNumber);
                            enemyManager.enemiesList[enemyCount].position.x = x;
                            enemyManager.enemiesList[enemyCount].position.y = y;
                            AddToCharacterList(enemyManager.enemiesList[enemyCount]);
                            activeMap[y, x] = '-';
                            enemyCount += 1;
                        }
                    }
                    if(tile == '@' && levelChanged == false)
                    {
                        itemManager.AddItemToList("Coin",x,y);
                        itemManager.items[itemCount].index = itemCount;
                        itemCount += 1;
                    }
                    if(tile == '"' && levelChanged == false)
                    {
                        itemManager.AddItemToList("Health Pickup",x,y);
                        itemManager.items[itemCount].index = itemCount;
                        itemCount += 1;
                    }
                    if(tile == '+' && levelChanged == false)
                    {
                        itemManager.AddItemToList("Armor Pickup",x,y);
                        itemManager.items[itemCount].index = itemCount;
                        itemCount += 1;
                    }
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
        public void GetPlayerMaxPosition(Player player)
        {
            playerMaxX = player.position.maxX;
            playerMaxY = player.position.maxY;
        }
        public void DrawEnemiesToMap(List<Enemy> eList)
        {
            //Draws enemys to map based on enemy position.
            for(int i = 0; i < eList.Count(); i++)
            {
                if(eList[i].healthSystem.IsAlive == true)
                {
                    Console.SetCursorPosition(eList[i].position.x,eList[i].position.y);
                    DrawEnemy(eList[i]);
                }
            }
        }
        public void DrawEnemy(Enemy enemy)
        {
            //Makes sure enemy matches avatar and color. 
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = enemy.avatarColor;
            Console.Write(enemy.avatar);
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
            //Draws armor tile to map
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(armorPickup);
            SetColorDefault();
        }
        public void DrawStairsDown()
        {
            //Draws stairs to next level
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(stairsDown);
            SetColorDefault();
        }
        public void DrawHealthPickup()
        {
            //Draws health pickup
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(healthPickup);
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
                DrawFloor();
                return;
            }
            if(tile == '"')
            {
                DrawFloor();
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
                DrawFloor();
                return;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
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
            RestCharacters();
            ResetItems();
            MakeDungeonMap();
            DrawMap();
        }
        public bool CretureInTarget(int y, int x)
        {
            //Used to check if a creture is in target location. 
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
        public bool IsPlayerInTarget(int y, int x)
        {
            //Checks if player is in target loctaion
            bool IsTarget;
            if(mainPlayer.position.y == y && mainPlayer.position.x == x)
            {
                IsTarget = true;
            }
            else
            {
                IsTarget = false;
            }
            return IsTarget;
        }
        public bool ItemInTarget(int y, int x)
        {
            //Checks for Item in target location
            bool IsTarget = false;
            for(itemIndex = 0; itemIndex < itemManager.items.Count(); itemIndex++)
            {
                if(x == itemManager.items[itemIndex].position.x && y == itemManager.items[itemIndex].position.y)
                {
                    IsTarget = true;
                    return IsTarget;
                }
                if(itemIndex > itemManager.items.Count() || itemIndex < 0)
                {
                    index = 0;
                }
            }
            return IsTarget;
        }
        public void AddToCharacterList(Character character)
        {
            //Adds to list for character used for the floor. 
            characters.Add(character);
        }
        public bool CheckTile(int y, int x)
        {
            //Makes sure base tile is walkable. 
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
            //Resets map for moving floors.
            enemyManager.enemiesList.Clear();
            characters.Clear();
            enemyCount = 0;
            characters.Add(mainPlayer);
        }
        public void ResetItems()
        {
            //resets items for moving floors.
            itemManager.items.Clear();
            itemCount = 0;
        }
        public void DrawEnemyLegend()
        {
            //Draws legend for enemies.
            Console.SetCursorPosition(mapX + 1, 3);
            Console.Write(enemy2);
            Console.Write(" = Slime");
            Console.SetCursorPosition(mapX + 1, 4);
            Console.Write(enemy1);
            Console.Write(" = Construct");
            Console.SetCursorPosition(mapX + 1, 5);
            Console.Write(enemy3);
            Console.Write(" = Goblin Folk");
        }
        void DrawHUD() //Add to a UIManager Class
        {
            //Draws HUD.
            Console.SetCursorPosition(0,mapY + 1);
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
