using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal class GameManager
    {
        public Player player;
        public EnemyManager enemyManager;
        public Map gameMap;
        public ItemManager itemManager;
        public UIManager uiManager;
        public QuestManager questManager;


        private void StartUp()
        {
            Console.CursorVisible = false;
            Debug.WriteLine("Setting Up characters");

            Initialize();

        }

        private void EndGame()
        {
            string FormatString = "You had {0} coins, {1} armor, and {2} HP remaining!";
            Debug.WriteLine("EndingGame");
            if (player.GameIsOver && player.GameWon == true)
            {
                Debug.WriteLine("Player won");
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("You Won!");
                Console.WriteLine();
                Console.WriteLine(string.Format(FormatString, player.PlayerCoins, player.healthSystem.armor, player.healthSystem.health));
                Console.WriteLine();
                Console.WriteLine("CONGRADULATIONS!");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            if (player.GameIsOver && player.GameWon != true)
            {
                Debug.WriteLine("Player lost");
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("You have lost. Restarting game!");
                Thread.Sleep(3000);
                PlayGame();
            }
        }

        private void DungeonGameLoop()
        {
            Debug.WriteLine("Running GameLoop");
            gameMap.Draw();
            itemManager.Draw();
            enemyManager.Draw();
            player.Draw();
            uiManager.Draw();

            // Verify that enemies exist
            Debug.WriteLine($"Total Enemies Spawned: {enemyManager.enemies.Count}");

            while (!player.GameIsOver && !player.GameWon)
            {
                Console.CursorVisible = false;
                CheckPlayerCondition();

                Update();
                Draw();
            }

            EndGame();
        }
        private void Initialize()
        {
            gameMap = new Map();  // Game map must be initialized first since all managers rely on it

            // Initialize managers
            uiManager = new UIManager();
            itemManager = new ItemManager();
            enemyManager = new EnemyManager();
            questManager = new QuestManager(this);

            // Initialize player after UIManager and other managers are set up
            player = new Player(this);

            // Initialize components that require references to each other
            player.Initialize();          // Player needs to be initialized first to get the starting position
            uiManager.Initialize(this);   // UI Manager requires reference to GameManager and Player
            itemManager.Initialize(this); // Initialize ItemManager with GameManager
            enemyManager.Initialize(this); // Initialize EnemyManager with GameManager

            // Add enemies and items after everything has been initialized
            enemyManager.AddEnemies();
            itemManager.AddItems();
        }


        private void Update()
        {
            player.Update();
            enemyManager.Update();
            itemManager.Update();
        }

        private void Draw()
        {
            gameMap.Draw();
            enemyManager.Draw();
            itemManager.Draw();
            uiManager.Draw();
            player.Draw();
        }

        public void PlayGame()
        {
            Debug.WriteLine("Starting Game");
            StartUp();
            uiManager.WriteIntro();
            gameMap.Draw();
            DungeonGameLoop();
        }

        private void CheckPlayerCondition()
        {
            //Debug.WriteLine("Checking player");
            //if(player.healthSystem.IsAlive == false)
            //{
            //    player.GameIsOver = true;
            //}
        }


    }
}
