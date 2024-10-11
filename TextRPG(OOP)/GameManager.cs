using System;
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
        public bool GameOver;
        public bool GameWon;


        private void StartUp()
        {
            Console.CursorVisible = false;
            //Debug.WriteLine("Setting Up characters");
            Initialize();
        }

        private void EndGame()
        {
            string FormatString = "You had {0} coins, {1} armor, and {2} HP remaining!";
            //Debug.WriteLine("EndingGame");
            if (GameOver && GameWon)
            {
                Debug.WriteLine("Player won");
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("You Won!");
                Console.WriteLine();
                Console.WriteLine(string.Format(FormatString, player.PlayerCoins, player.healthSystem.armor, player.healthSystem.health));
                Console.WriteLine();
                Console.WriteLine("CONGRATULATIONS!");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            if (GameOver && !GameWon)
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
            gameMap.Draw();
            itemManager.Draw();
            enemyManager.Draw();
            player.Draw();
            uiManager.Draw();

            // Verify that enemies exist
            Debug.WriteLine($"Total Enemies Spawned: {enemyManager.levelEnemies[gameMap.levelNumber].Count}");

            while (!GameOver && !GameWon)
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
            gameMap.Update();
            enemyManager.Update();
            itemManager.Update();

            if(gameMap.changeLevel)
            {
                player.Initialize();
                Debug.WriteLine("Player should be at position of start");
                gameMap.Draw();
                gameMap.changeLevel = false;
            }
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
            //Debug.WriteLine("Starting Game");
            StartUp();
            uiManager.WriteIntro();
            gameMap.Draw();
            DungeonGameLoop();
        }

        private void CheckPlayerCondition()
        {
            if (player.healthSystem.IsAlive == false)
                GameOver = true;
        }
    }
}
