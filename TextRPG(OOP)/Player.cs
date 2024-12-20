﻿using System;
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
            private int dirX;
            private int dirY;
            private int prevX;
            private int prevY;
            public int PlayerCoins;
            public ConsoleKeyInfo PlayerInput;
            private Map map;
            private EnemyManager enemyManager; // Reference to EnemyManager
            private ItemManager itemManager; // references to itemManager
            private UIManager uiManager;
            private GameManager GameManager;
            private QuestManager questManager;
            internal bool boughtItem;

            private PlayerAttributes attributes;

            public Player(GameManager gameManager, PlayerAttributes attributes)
            {
                this.GameManager = gameManager;
                this.attributes = attributes;

                avatar = (char)2; // Sets player to smiley face

                damage = attributes.BaseDamage; // sets damage with the player base from settings
                healthSystem = new HealthSystem();  // creates new health system for player
                healthSystem.SetHealth(attributes.BaseHP); // sets max hp
                name = attributes.Name; // player name
                PlayerCoins = attributes.StartingCoins;

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
                if (map.changeLevel)
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
                    if(enemy.healthSystem.wasHurt)
                    {
                        uiManager.AddEventLogMessage($"{name} attacked {enemy.enemyType}");
                        enemy.healthSystem.wasHurt = false;
                    }
                    else
                        uiManager.AddEventLogMessage($"{name}'s armor is too strong for {name}");

                    return; // player doesn't move if there's an enemy
                }
                else if (item != null)
                {
                    if (!map.isInStore(position.y, position.x))
                    {
                        ApplyEffect(item);
                            position.x = newX;
                            position.y = newY;
                    }
                    else
                    {
                        if (PlayerCoins >= item.cost)
                        {
                            if (item.itemType == "Health" && healthSystem.health == healthSystem.maxHealth)
                            {
                                uiManager.AddEventLogMessage($"{name} doesn't need health");
                            }
                            else
                            {
                                uiManager.AddEventLogMessage($"{name} spent {item.cost} coins for {item.itemType}");
                                boughtItem = true;
                                ApplyEffect(item);
                                PlayerCoins -= item.cost;
                            }
                        }
                        else
                            uiManager.AddEventLogMessage($"{name} cannot afford {item.itemType}, it cost {item.cost} coins");
                    }
                }
                else
                {
                    if (map.IsStairs(newY, newX))
                    {
                        map.changeLevel = true;
                    }

                    // Proceed with movement if there's no enemy and the tile is walkable
                    else if (map.IsWithinBounds(newY, newX) && map.IsWalkable(newY, newX))
                    {
                        prevX = position.x;
                        prevY = position.y;

                        // Update player position
                        position.x = newX;
                        position.y = newY;
                    }
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
                item.Apply(this, uiManager, questManager);
                boughtItem = false;
            }
        }
    }
}