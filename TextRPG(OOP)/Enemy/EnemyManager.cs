using System.Collections.Generic;
using System;
using TextRPG_OOP_;
using System.Diagnostics;
using TextRPG_OOP_.TextRPG_OOP_;

internal class EnemyManager
{
    public Dictionary<int, List<Enemy>> levelEnemies;
    public Map gameMap;
    private Random random;  // Shared Random instance
    public Player player;
    public ItemManager itemManager;
    public GameManager gameManager;
    public UIManager uiManager;
    public QuestManager questManager;

    public EnemyManager()
    {
        levelEnemies = new Dictionary<int, List<Enemy>>();
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
        if (!levelEnemies.ContainsKey(gameMap.levelNumber))
        {
            levelEnemies[gameMap.levelNumber] = new List<Enemy>();
            AddEnemiesToList("Plasmoid", gameMap.levelNumber, player);
            AddEnemiesToList("GoblinFolk", gameMap.levelNumber, player);
            AddEnemiesToList("Construct", gameMap.levelNumber, player);
        }
    }

    private void AddEnemiesToList(string type, int levelNumber, Player player)
    {
        List<Position> enemyPositions = new List<Position>();

        // Find positions for each type of enemy
        switch (type)
        {
            case "Plasmoid": enemyPositions = gameMap.FindPositionsByChar('!'); break;  // Find all '!' positions for Plasmoid 
            case "GoblinFolk": enemyPositions = gameMap.FindPositionsByChar('&'); break; // Find all '&' positions for GoblinFolk
            case "Construct": enemyPositions = gameMap.FindPositionsByChar('?'); break; // Find all '?' positions for Construct
        }

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
            switch (type)
            {
                case "Plasmoid": enemy = new Plasmoid(RandomConsoleColor(), random, gameManager); break;  // Find all '!' positions for Plasmoid 
                case "GoblinFolk": enemy = new GoblinFolk(RandomConsoleColor(), random, gameManager); break; // Find all '&' positions for GoblinFolk
                case "Construct": enemy = new Construct(RandomConsoleColor(), random, gameManager); break;
            }

            if (enemy != null)
            {
                enemy.position = pos;  // Set enemy's starting position from map
                levelEnemies[levelNumber].Add(enemy);  // Add to enemy list
            }
        }
    }


    public void Update()
    {
        if (gameMap.levelChange)
        {
            AddEnemies();
        }
        else if (!gameMap.inStore)
        {
            foreach (var enemy in levelEnemies[gameMap.levelNumber])
            {
                if (enemy.healthSystem.IsAlive)
                {
                    enemy.Update(gameMap); // Update if alive
                }
                else
                {
                    if (!enemy.beenCounted)
                    {
                        if (enemy.enemyType == Settings.questEnemyType)
                            questManager.UpdateQuestProgress(questManager.questKillEnemyType, 1); // update enemy type kill quest
                        questManager.UpdateQuestProgress(questManager.questKillEnemies, 1); // Update all enemies kill quest
                        uiManager.AddEventLogMessage($"You killed {enemy.enemyType}");
                        enemy.beenCounted = true;
                    }
                }
            }
        }
    }


    public void Draw()
    {
        if (!gameMap.inStore)
        {
            foreach (var enemy in levelEnemies[gameMap.levelNumber])
            {
                if (enemy.healthSystem.IsAlive)
                {
                    // Clear the old position
                    if (enemy.prevX != enemy.position.x || enemy.prevY != enemy.position.y)
                    {
                        ClearPrevious(enemy.prevX, enemy.prevY);
                    }

                    Console.SetCursorPosition(enemy.position.y, enemy.position.x);
                    Console.ForegroundColor = enemy.avatarColor;  // Set enemy's avatar color
                    Console.BackgroundColor = ConsoleColor.Gray;

                    Console.Write(enemy.avatar);  // Draw the enemy avatar
                    Console.ResetColor();  // Reset colors after drawing
                }
            }
        }
    }

    private void ClearPrevious(int prevX, int prevY)
    {
        Console.SetCursorPosition(prevY, prevX); // Correct order: Set cursor to old position
        Console.ForegroundColor = ConsoleColor.Gray; // Assuming this is the color of the floor
        Console.BackgroundColor = ConsoleColor.Gray; // Assuming this is the color of the floor
        Console.Write("-"); // Clear the old position
        Console.ResetColor();
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
        if (levelEnemies.ContainsKey(gameMap.levelNumber))
        {
            // Get the list of items for the current level
            var itemsAtCurrentLevel = levelEnemies[gameMap.levelNumber];
            foreach (var enemy in levelEnemies[gameMap.levelNumber])
            {
                if ((enemy.position.x == x && enemy.position.y == y) && enemy.healthSystem.IsAlive)
                {
                    return enemy; // Return the enemy at the position
                }
            }
        }
        return null; // No enemy at the position
    }
}
