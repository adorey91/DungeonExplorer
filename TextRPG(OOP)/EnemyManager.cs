using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TextRPG_OOP_
{
    internal class EnemyManager
    {
        public List<Enemy> enemiesList; 
        public Map gameMap;
        public bool isFirstKobald;
        public Settings enemySettings;
        public EnemyManager(Map map, Settings settings)
        {
            isFirstKobald = true;
            gameMap = map;
            enemiesList = new List<Enemy>();
            enemySettings = settings;
        }
        public void AddEnemiesToList(string type, int levelNumber)
        {
            if(type == "Plasmoid")
            {
                Plasmoid plasmoid = new Plasmoid(RandomConsoleColor(),enemySettings);
                plasmoid.enemyType = type;
                plasmoid.name = "Slime " + EnemyTypeCount(type);
                plasmoid.SetLevelNumber(levelNumber);
                plasmoid.SetEnemyMaxPosition(gameMap);
                plasmoid.SetEnemyStats();
                enemiesList.Add(plasmoid);
            }
            if(type == "Construct")
            {
                Construct construct = new Construct(RandomConsoleColor(),enemySettings);
                construct.enemyType = type;
                construct.name = "Living Armor " + EnemyTypeCount(type);
                construct.SetLevelNumber(levelNumber);
                construct.SetEnemyMaxPosition(gameMap);
                construct.SetEnemyStats();
                enemiesList.Add(construct);
            }
            if(type == "GoblinFolk")
            {
                GoblinFolk goblinFolk = new GoblinFolk(RandomConsoleColor(),enemySettings);
                goblinFolk.enemyType = type;
                if(isFirstKobald)
                {
                    goblinFolk.name = "Jim the Coward";
                    isFirstKobald = false;
                }
                else
                {
                    goblinFolk.name = "Goblin " + EnemyTypeCount(type);
                }
                goblinFolk.SetLevelNumber(levelNumber);
                goblinFolk.SetEnemyMaxPosition(gameMap);
                goblinFolk.SetEnemyStats();
                enemiesList.Add(goblinFolk);
            }
        }
        public void Update()
        {
            for(int i = 0; i < enemiesList.Count(); i++)
            {
                Debug.WriteLine("Moving " + enemiesList[i].name);
                enemiesList[i].MoveEnemy(gameMap);
            }
        }
        public void Draw()
        {
            gameMap.DrawEnemiesToMap(enemiesList);
        }
        public ConsoleColor RandomConsoleColor()
        {
            Random colorRoll = new Random();
            int rollResult = colorRoll.Next(1,7);
            ConsoleColor RandomColor = new ConsoleColor();
            if(rollResult == 1)
            {
                RandomColor = ConsoleColor.DarkBlue;
            }
            if(rollResult == 2)
            {
                RandomColor = ConsoleColor.DarkGreen;
            }
            if(rollResult == 3)
            {
                RandomColor = ConsoleColor.DarkMagenta;
            }
            if(rollResult == 4)
            {
                RandomColor = ConsoleColor.DarkRed;
            }
            if(rollResult == 5)
            {
                RandomColor = ConsoleColor.DarkYellow;
            }
            if(rollResult == 6)
            {
                RandomColor = ConsoleColor.Green;
            }
            return RandomColor;
        }
        int EnemyTypeCount(string type)
        {
            int enemyCount = 0;
            for(int i = 0; i < enemiesList.Count(); i++)
            {
                if(enemiesList[i].enemyType == type)
                {
                    enemyCount += 1;
                }
            }
            if(type == "GoblinFolk")
            {
                enemyCount -= 1;
            }
            return enemyCount;
        }
    }
}
