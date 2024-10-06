using System.Collections.Generic;
using System;
using TextRPG_OOP_;
using System.Diagnostics;
using TextRPG_OOP_.TextRPG_OOP_;

internal class EnemyManager
{
    public List<Enemy> enemies;
    public Map gameMap;
    private Random random;  // Shared Random instance
    public Player player;
    public ItemManager itemManager;
    public GameManager gameManager;
    public UIManager uiManager;
    public QuestManager questManager;

    public EnemyManager()
    {
        enemies = new List<Enemy>();
        random = new Random();  // Instantiate Random once
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
        this.gameMap = gameManager.gameMap;
        this.player = gameManager.player;
        this.itemManager = gameManager.itemManager;
        this.uiManager = gameManager.uiManager;
        this.questManager = gameManager.questManager;
    }

    public void AddEnemies()
    {
        AddEnemiesToList("Plasmoid", gameMap.levelNumber, player);
        AddEnemiesToList("GoblinFolk", gameMap.levelNumber, player);
        AddEnemiesToList("Construct", gameMap.levelNumber, player);
    }

    private void AddEnemiesToList(string type, int levelNumber, Player player)
    {
        List<Position> enemyPositions = new List<Position>();

        // Find positions for each type of enemy
        if (type == "Plasmoid")
            enemyPositions = gameMap.FindPositionsByChar('!');  // Find all '!' positions for Plasmoid
        else if (type == "GoblinFolk")
            enemyPositions = gameMap.FindPositionsByChar('&');  // Find all '&' positions for GoblinFolk
        else if (type == "Construct")
            enemyPositions = gameMap.FindPositionsByChar('?');  // Find all '?' positions for Construct

        // If no positions are found, skip spawning that enemy type
        if (enemyPositions.Count == 0)
        {
            //Debug.WriteLine($"No {type} enemies found on the map for level {levelNumber}. Skipping spawn.");
            return;
        }

        // Spawn enemies at the found positions
        foreach (var pos in enemyPositions)
        {
            Enemy enemy = null;

            if (type == "Plasmoid")
                enemy = new Plasmoid(RandomConsoleColor(), random, gameManager);
            else if (type == "GoblinFolk")
                enemy = new GoblinFolk(RandomConsoleColor(), random, gameManager);
            else if (type == "Construct")
                enemy = new Construct(RandomConsoleColor(), random, gameManager);

            if (enemy != null)
            {
                enemy.position = pos;  // Set enemy's starting position from map
                enemies.Add(enemy);  // Add to enemy list
                                     //Debug.WriteLine($"{type} enemy added at ({pos.x}, {pos.y}) with name {enemy.name}");
            }
        }
    }


    public void Update()
    {
        if (gameMap.levelChange)
        {
            enemies.Clear();

            AddEnemies();
        }
        else if (!gameMap.inStore)
        {
            List<Enemy> enemiesToRemove = new List<Enemy>(); // List to track enemies to be removed

            foreach (var enemy in enemies)
            {
                if (enemy.healthSystem.IsAlive)
                {
                    enemy.Update(gameMap); // Update if alive
                }
                else
                {
                    if (enemy.enemyType == Settings.questEnemyType)
                        questManager.UpdateQuestProgress(questManager.questKillEnemyType, 1); // update enemy type kill quest
                    questManager.UpdateQuestProgress(questManager.questKillEnemies, 1); // Update all enemies kill quest
                    uiManager.AddEventLogMessage($"You killed {enemy.enemyType}");
                    enemiesToRemove.Add(enemy); // Add to removal list instead of directly removing
                }
            }

            // Now remove all enemies that need to be removed after the loop
            foreach (var enemy in enemiesToRemove)
            {
                enemies.Remove(enemy);
            }
        }
    }


    public void Draw()
    {
        if (!gameMap.inStore)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.healthSystem.IsAlive)
                {
                    Console.SetCursorPosition(enemy.position.y, enemy.position.x);
                    Console.ForegroundColor = enemy.avatarColor;  // Set enemy's avatar color
                    Console.BackgroundColor = ConsoleColor.Gray;

                    Console.Write(enemy.avatar);  // Draw the enemy avatar
                    Console.ResetColor();  // Reset colors after drawing
                }
            }
        }
    }

    public ConsoleColor RandomConsoleColor()
    {
        int rollResult = random.Next(1, 7);
        ConsoleColor selectedColor;

        switch (rollResult)
        {
            case 1: selectedColor = ConsoleColor.DarkBlue; break;
            case 2: selectedColor = ConsoleColor.DarkGreen; break;
            case 3: selectedColor = ConsoleColor.DarkMagenta; break;
            case 4: selectedColor = ConsoleColor.DarkRed; break;
            case 5: selectedColor = ConsoleColor.Blue; break;
            case 6: selectedColor = ConsoleColor.Green; break;
            default: selectedColor = ConsoleColor.White; break;
        }
        return selectedColor;
    }


    // Method to get the enemy at a specific position
    public Enemy GetEnemyAtPosition(int x, int y)
    {
        foreach (var enemy in enemies)
        {
            if ((enemy.position.x == x && enemy.position.y == y) && enemy.healthSystem.IsAlive)
            {
                return enemy; // Return the enemy at the position
            }
        }
        return null; // No enemy at the position
    }
}
