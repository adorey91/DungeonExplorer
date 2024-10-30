using System.Collections.Generic;
using System;
using TextRPG_OOP_;
using System.Diagnostics;
using TextRPG_OOP_.TextRPG_OOP_;
using System.IO;
using System.Text.Json;

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
    private EnemyData enemyData;

    public EnemyManager()
    {
        levelEnemies = new Dictionary<int, List<Enemy>>();
        random = new Random();  // Instantiate Random once
        LoadEnemyData();
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
            if (enemyData.Enemies.TryGetValue(type, out EnemyAttributes attributes))
            {
                Enemy enemy = CreateEnemy(type, attributes);

                if (enemy != null)
                {
                    enemy.position = pos;
                    levelEnemies[levelNumber].Add(enemy);
                }
            }
        }
    }


    public void Update()
    {
        if (gameMap.changeLevel)
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
                        //if (enemy.enemyType == Settings.questEnemyType)
                        //    questManager.UpdateQuestProgress(questManager.questKillEnemyType, 1); // update enemy type kill quest
                        //if (enemy.enemyType == Settings.questEnemyType2)
                        //    questManager.UpdateQuestProgress(questManager.questKillEnemyType2, 1); // update enemy type kill quest 2
                        //questManager.UpdateQuestProgress(questManager.questKillEnemies, 1); // Update all enemies kill quest
                        //uiManager.AddEventLogMessage($"You killed {enemy.enemyType}");
                        //enemy.beenCounted = true;
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

    private void LoadEnemyData()
    {
        string jsonFilePath = "JSON\\enemies.json";
        string jsonString = File.ReadAllText(jsonFilePath);
        enemyData = JsonSerializer.Deserialize<EnemyData>(jsonString);
    }

    private ConsoleColor ConvertToConsoleColor(string color)
    {
        return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
    }

    private Enemy CreateEnemy(string type, EnemyAttributes attributes)
    {
        switch (type)
        {
            case "GoblinFolk":
                return new GoblinFolk(
                    ConvertToConsoleColor(attributes.SpawnColor), // Convert color string to ConsoleColor
                    random,
                    gameManager,
                    attributes // Pass the attributes to the GoblinFolk constructor
                );
            case "Plasmoid":
                return new Plasmoid(
                    ConvertToConsoleColor(attributes.SpawnColor),
                    random,
                    gameManager,
                    attributes // Pass attributes to the Plasmoid constructor
                );
            case "Construct":
                return new Construct(
                    ConvertToConsoleColor(attributes.SpawnColor),
                    random,
                    gameManager,
                    attributes // Pass attributes to the Construct constructor
                );
            default:
                return null;  // Unknown enemy type
        }
    }
}