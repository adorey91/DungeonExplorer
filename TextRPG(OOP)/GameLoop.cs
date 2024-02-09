using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TextRPG_OOP_
{
    internal class GameLoop
    {
        static Player mainPlayer;
        static Enemy slimeEnemy;
        static Enemy livingArmor;
        static Enemy cowardKobald;
        static Map gameMap;
        static void Main(string[] args)
        {
            StartUp();
            Console.WriteLine("Welcome to my Text RPG game!");
            Console.WriteLine("Find your way to the challace.");
            Console.WriteLine("Collect coins to increase your attack power.");
            Console.WriteLine("Collect hearts to heal.");
            Console.WriteLine("Collect peices of armor to up your defence.");
            Console.WriteLine("Avoid monsters!");
            Console.WriteLine("Press any key to get started!");
            Console.ReadKey(true);
            Console.Clear();
            SetUpMap();
            while(mainPlayer.gameIsOver != true && mainPlayer.gameWon != true)
            {
                if(!slimeEnemy.healthSystem.IsAlive)
                {
                    slimeEnemy.SetEnemyStats();
                }
                if(!livingArmor.healthSystem.IsAlive)
                {
                    livingArmor.SetEnemyStats();
                }
                if(!cowardKobald.healthSystem.IsAlive)
                {
                    cowardKobald.SetEnemyStats();
                }
                mainPlayer.CheckPlayerCondition();
                mainPlayer.GetPlayerInput(gameMap);
                slimeEnemy.MoveEnemy(gameMap);
                livingArmor.MoveEnemy(gameMap);
                cowardKobald.MoveEnemy(gameMap);
                gameMap.DrawMap();
                mainPlayer.DrawHUD();
                gameMap.DrawPlayerToMap(mainPlayer.position.x, mainPlayer.position.y);
                if(slimeEnemy.healthSystem.IsAlive)
                {
                    gameMap.DrawEnemyToMap(slimeEnemy);
                }
                if(livingArmor.healthSystem.IsAlive)
                {
                    gameMap.DrawEnemyToMap(livingArmor);
                }
                if(cowardKobald.healthSystem.IsAlive)
                {
                    gameMap.DrawEnemyToMap(cowardKobald);
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
            slimeEnemy = new Enemy();
            livingArmor = new Enemy();
            cowardKobald = new Enemy();
            gameMap.AddToCharacterList(mainPlayer);
            gameMap.AddToCharacterList(slimeEnemy);
            gameMap.AddToCharacterList(livingArmor);
            gameMap.AddToCharacterList(cowardKobald);
            slimeEnemy.enemyNumber = 1;
            slimeEnemy.enemyType = 1;
            livingArmor.enemyNumber = 2;
            livingArmor.enemyType = 3;
            cowardKobald.enemyNumber = 3;
            cowardKobald.enemyType = 2;
            slimeEnemy.name = "Slime";
            livingArmor.name = "Living Armor";
            cowardKobald.name = "Jim the coward";
        }  
        static void SetUpMap()
        {
            //Intial map draw / setup
            gameMap.DrawMap();
            mainPlayer.DrawHUD();
            gameMap.GetPlayerMaxPosition(mainPlayer);
            gameMap.SetPlayerSpawn(mainPlayer);
            mainPlayer.SetMaxPlayerPosition(gameMap);
            slimeEnemy.SetEnemyMaxPosition(gameMap);
            livingArmor.SetEnemyMaxPosition(gameMap);
            cowardKobald.SetEnemyMaxPosition(gameMap);
            gameMap.SetEnemySpawns(livingArmor, livingArmor.enemyNumber);
            gameMap.SetEnemySpawns(slimeEnemy, slimeEnemy.enemyNumber);
            gameMap.SetEnemySpawns(cowardKobald, cowardKobald.enemyNumber);
            gameMap.DrawEnemyToMap(slimeEnemy);
            slimeEnemy.SetLevelNumber(gameMap.levelNumber);
            slimeEnemy.SetEnemyStats();
            gameMap.DrawEnemyToMap(livingArmor);
            livingArmor.SetLevelNumber(gameMap.levelNumber);
            livingArmor.SetEnemyStats();
            gameMap.DrawEnemyToMap(cowardKobald);
            cowardKobald.SetLevelNumber(gameMap.levelNumber);
            cowardKobald.SetEnemyStats();
            gameMap.DrawPlayerToMap(mainPlayer.position.x, mainPlayer.position.y);
        }
    }
}
