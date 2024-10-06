using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TextRPG_OOP_
{
    /// <summary>
    /// Is the player, Handles all player movment and interactions.
    /// </summary>
    using System;
    using System.Configuration;

    namespace TextRPG_OOP_
    {
        /// <summary>
        /// Represents the player character, handling all player movement and interactions.
        /// </summary>
        internal class Player : Character
        {
            int dirX;
            int dirY;
            public int PlayerCoins;
            public ConsoleKeyInfo PlayerInput;
            public bool GameIsOver = false;
            public bool GameWon = false;
            public Map map;
            public EnemyManager enemyManager; // Reference to EnemyManager
            public ItemManager itemManager; // references to itemManager
            public UIManager uiManager;
            public GameManager GameManager;
            public QuestManager questManager;

            public Player(GameManager gameManager)
            {
                this.GameManager = gameManager;

                avatar = (char)2; // Sets player to smiley face
                GameIsOver = false;
                GameWon = false;
                damage = Settings.PlayerBaseDamage; // sets damage with the player base from settings

                healthSystem = new HealthSystem();  // creates new health system for player
                healthSystem.SetHealth(Settings.playerMaxHP); // sets max hp
                name = Settings.playerName; // player name
                this.map = gameManager.gameMap; // Hands map to player
                this.uiManager = gameManager.uiManager;
                this.enemyManager = gameManager.enemyManager; // Hands EnemyManager to player
                this.itemManager = gameManager.itemManager;
                this.questManager = gameManager.questManager;
            }

            /// <summary>
            /// Initializes player position to prevent leaving the screen.
            /// </summary>
            public void Initialize()
            {
                position = map.FindPlayerStartPosition();
            }

            /// <summary>
            /// Updates player status and handles interactions based on input.
            /// </summary>
            public void Update()
            {
                GetPlayerInput();
                HandleMovement(dirX, dirY);
                if (map.levelChange || map.goToStore)
                    map.FindPlayerStartPosition();
            }

            /// <summary>
            /// Handles player input and updates the player based on interactions.
            /// </summary>
            private void GetPlayerInput()
            {
                ConsoleKeyInfo input = Console.ReadKey(true); // Read key without displaying it

                dirX = 0; // Reset direction
                dirY = 0; // Reset direction

                switch (input.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        dirY = -1; // Move up (decrease Y)
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        dirY = 1; // Move down (increase Y)
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        dirX = -1; // Move left (decrease X)
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        dirX = 1; // Move right (increase X)
                        break;
                    case ConsoleKey.Spacebar:
                        return; // using for testing, player doesn't move
                    case ConsoleKey.Escape:
                        System.Environment.Exit(0);
                        return;
                }
            }

            /// <summary>
            /// Handles the movement of the player
            /// </summary>
            private void HandleMovement(int moveY, int moveX)
            {
                // Calculate the new position based on movement direction
                int newX = position.x + moveX;
                int newY = position.y + moveY;

                // Check if the player is attempting to move into an enemy
                Enemy enemy = enemyManager.GetEnemyAtPosition(newX, newY);
                Item item = itemManager.GetItemAtPosition(newX, newY);
                if (enemy != null)
                {
                    // Apply damage to the enemy
                    //Debug.WriteLine(enemy.name);
                    //Debug.WriteLine($"Enemy encountered at ({newX}, {newY})");
                    enemy.healthSystem.TakeDamage(damage); // Use the player's damage value
                    uiManager.AddEventLogMessage($"{name} attacked {enemy.enemyType}");
                    return; // player doesn't move if there's an enemy
                }
                else if (item != null)
                {
                    if (!map.inStore)
                    {
                        ApplyEffect(item);
                        if (item.itemType == "Spike")
                        {
                            position.x = newX;
                            position.y = newY;
                        }
                    }
                    else
                    {
                        if (PlayerCoins >= item.cost)
                        {
                            ApplyEffect(item);
                        }
                    }
                }
                else
                {
                    if (map.IsStairs(newY, newX))
                    {
                        map.levelChange = true;
                    }
                    else if (map.IsStore(newY, newX))
                    {
                        map.goToStore = true;
                    }
                    // Proceed with movement if there's no enemy and the tile is walkable
                    else if (map.IsWithinBounds(newY, newX) && map.IsWalkable(newY, newX))
                    {
                        // Update player position
                        position.x = newX;
                        position.y = newY;
                        //Debug.WriteLine($"Moved to: x={position.x}, y={position.y}");
                    }
                    //else
                    //{
                    //    Debug.WriteLine("No movement");
                    //}
                }
            }


            /// <summary>
            /// Draws the player on the map.
            /// </summary>
            public void Draw()
            {
                // Draw the player in the new position
                Console.SetCursorPosition(position.y, position.x);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(avatar);
                Console.ResetColor();
            }

            private void ApplyEffect(Item item)
            {
                switch (item.itemType)
                {
                    case "Coin":
                        PlayerCoins += item.gainAmount;
                        questManager.UpdateQuestProgress(questManager.questCollectCoins, item.gainAmount);
                        uiManager.AddEventLogMessage($"Player collected coin");
                        item.isCollected = true;
                        break;
                    case "Health":
                        int previousHealth = healthSystem.health;
                        if (healthSystem.health != healthSystem.maxHealth)
                        {
                            healthSystem.Heal(item.gainAmount);
                            int healedAmount = healthSystem.health - previousHealth;
                            uiManager.AddEventLogMessage($"Player healed {healedAmount}");
                            item.isCollected = true;
                        }
                        else
                            uiManager.AddEventLogMessage($"Cannot heal anymore");
                        break;
                    case "Armor":
                        healthSystem.IncreaseArmor(item.gainAmount);
                        uiManager.AddEventLogMessage($"Player gained {item.gainAmount} armor");
                        item.isCollected = true;
                        break;
                    case "Sword":
                        damage += item.gainAmount;
                        uiManager.AddEventLogMessage($"Player gained {item.gainAmount} damage");
                        item.isCollected = true;
                        break;
                    case "Spike":
                        healthSystem.TakeDamage(item.gainAmount);
                        uiManager.AddEventLogMessage($"{name} hurt by {item.itemType}, lost {item.gainAmount} health");
                        break;
                    default:
                        Debug.WriteLine("Unknown item type.");
                        break;
                }
            }
        }
    }
}