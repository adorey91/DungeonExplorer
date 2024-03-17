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
        private EnemyManager enemyManager;
        public Map gameMap;
        public ItemManager itemManager;
        private void StartUp()
        {
            Console.CursorVisible = false;
            Debug.WriteLine("Setting Up characters");
            itemManager = new ItemManager();
            gameMap = new Map(itemManager);
            enemyManager = new EnemyManager(gameMap);
            mainPlayer = new Player(gameMap,itemManager);
            gameMap.AddToCharacterList(mainPlayer);
        }  
        private void SetUpGame()
        {
            Debug.WriteLine("Setting up starting map");
            itemManager.Start(gameMap);
            gameMap.Start(mainPlayer, enemyManager);
            mainPlayer.Start();
            gameMap.Draw();
            enemyManager.Draw();
            itemManager.Draw();
            mainPlayer.Draw();
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
                CheckPlayerCondition();
                mainPlayer.Update();
                enemyManager.Update();
                itemManager.Update(mainPlayer);
                gameMap.Draw();
                enemyManager.Draw();
                itemManager.Draw();
                mainPlayer.Draw();
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
