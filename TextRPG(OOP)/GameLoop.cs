using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TextRPG_OOP_
{
    internal class GameLoop
    {
        static Player mainPlayer;
        static Enemy firstEnemy;
        static Map gameMap;
        static void Main(string[] args)
        {
            StartUp();
            //Console.WriteLine(gameMap.activeMap);
            Console.WriteLine("Welcome to my Text RPG game!");
            //Console.WriteLine("TEST");
            Console.WriteLine();
            Console.WriteLine("Press any key to get started.");
            Console.ReadKey(true);
            Console.Clear();
            gameMap.DrawMap();
            mainPlayer.DrawHUD();
            gameMap.GetPlayerMaxPosition(mainPlayer);
            gameMap.SetPlayerSpawn(mainPlayer);
            mainPlayer.SetMaxPlayerPosition(gameMap);
            firstEnemy.SetEnemyMaxPosition(gameMap);
            gameMap.SetEnemySpawns(firstEnemy, firstEnemy.enemyNumber);
            gameMap.DrawEnemyToMap(firstEnemy);
            gameMap.DrawPlayerToMap(mainPlayer.position.x, mainPlayer.position.y);
            while(mainPlayer.gameIsOver != true && mainPlayer.gameWon != true)
            {
                mainPlayer.CheckPlayerCondition();
                mainPlayer.GetPlayerInput(gameMap);
                firstEnemy.MoveEnemy(gameMap);
                gameMap.DrawMap();
                mainPlayer.DrawHUD();
                gameMap.DrawPlayerToMap(mainPlayer.position.x, mainPlayer.position.y);
                if(firstEnemy.healthSystem.IsAlive)
                {
                    gameMap.DrawEnemyToMap(firstEnemy);
                }
            }
            //Map Test ^
            //Console.ReadKey(true);
        }
        static void StartUp()
        {
            Console.CursorVisible = false;
            gameMap = new Map();
            mainPlayer = new Player();
            firstEnemy = new Enemy();
            gameMap.AddToCharacterList(mainPlayer);
            gameMap.AddToCharacterList(firstEnemy);
            firstEnemy.enemyNumber = 1;
            //gameMap.SetPlayerSpawn(mainPlayer);
            firstEnemy.enemyName = "Slime";
        }  
    }
}
