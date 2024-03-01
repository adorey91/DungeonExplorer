using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TextRPG_OOP_
{
    internal class GameManager
    {
        private Player mainPlayer;
        private Enemy slimeEnemy;
        private Enemy livingArmor;
        private Enemy cowardKobald;
        public Map gameMap;
        private void StartUp()
        {
            Debug.WriteLine("Setting Up characters");
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
        private void SetUpGame()
        {
            Debug.WriteLine("Setting up starting map");
            //Intial map draw / setup
            gameMap.DrawMap();
            DrawHUD();
            gameMap.DrawEnemyLegend();
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
        private void EndGame()
        {
            string FormatString = "You had {0} coins, {1} armor, and {2} HP remaining!";
            Debug.WriteLine("EndingGame");
            if(mainPlayer.gameIsOver && mainPlayer.gameWon == true)
            {
                Debug.WriteLine("Player won");
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("You Won!");
                Console.WriteLine();
                Console.WriteLine(string.Format(FormatString,mainPlayer.playerCoins,mainPlayer.healthSystem.armor,mainPlayer.healthSystem.health));
                Console.WriteLine();
                Console.WriteLine("CONGRADULATIONS!");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            if(mainPlayer.gameIsOver && mainPlayer.gameWon != true)
            {
                Debug.WriteLine("Player lost");
                Thread.Sleep(2000); 
                Console.Clear();
                Console.WriteLine("You have lost. Reload the game to try again!");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
        private void DungeonGameLoop()
        {
            Debug.WriteLine("Running GameLoop");
            while(mainPlayer.gameIsOver != true && mainPlayer.gameWon != true)
            {
                Console.CursorVisible = false;
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
                CheckPlayerCondition();
                mainPlayer.GetPlayerInput(gameMap);
                slimeEnemy.MoveEnemy(gameMap);
                livingArmor.MoveEnemy(gameMap);
                cowardKobald.MoveEnemy(gameMap);
                gameMap.DrawMap();
                DrawHUD();
                gameMap.DrawEnemyLegend();
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
            EndGame();
        }
        public void PlayGame()
        {
            Intro();
            Debug.WriteLine("Starting Game");
            StartUp();
            SetUpGame();
            DungeonGameLoop();
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
        private void CheckPlayerCondition()
        {
            Debug.WriteLine("Checking player");
            if(mainPlayer.healthSystem.IsAlive == false)
            {
                mainPlayer.gameIsOver = true;
            }
        }
        void Intro()
        {
            Debug.WriteLine("Into!");
            Console.WriteLine("Welcome to Dungeon Explorer!"); // placeholderTitle
            Console.WriteLine();
            Console.Write("Find your way to the challace. ");
            Console.WriteLine();
            Console.Write("Collect coins ");
            Console.Write("to increase your attack power.");
            Console.WriteLine();
            Console.Write("Collect hearts to heal.");
            Console.WriteLine();
            Console.Write("Collect peices of armor "); 
            Console.Write("to up your defence.");
            Console.WriteLine();
            Console.Write("Avoid or fight the monsters!");
            Console.WriteLine();
            Console.WriteLine("Press any key to get started!");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
