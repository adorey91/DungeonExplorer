using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal class UIManager
    {
        public Player player;
        public Map gameMap;
        public GameManager gameManager;
        public QuestManager questManager;

        private const int MaxEventLogMessages = 2;
        public List<string> eventMessages = new List<string>();
        public char[] legendsChar = new char[] {
    Settings.PlasmoidChar,
    Settings.ConstructChar,
    Settings.GoblinFolkChar,
    Settings.spikeChar,
    Settings.healthChar,
    Settings.coinChar,
    Settings.armorChar,
    Settings.swordChar,
    Settings.finalLootChar,
            Settings.storeChar,

};


        public void Initialize(GameManager gameManager)
        {
            this.gameManager = gameManager;
            player = gameManager.player;
            gameMap = gameManager.gameMap;
            questManager = gameManager.questManager;
        }

        public void Update()
        {

        }

        public void Draw()
        {
            // Calculate the starting position based on the height of the map
            int mapHeight = gameMap.activeMap.GetLength(0);
            int mapWidth = gameMap.activeMap.GetLength(1);
            Console.SetCursorPosition(0, mapHeight + 1); // Set cursor below the map

            PlayerStats(); // Draw player stats
            Console.WriteLine(new string('-', mapWidth));
            EventLog(); // Draw event log
            Console.WriteLine(new string('-', mapWidth));
            questManager.PrintQuestStatus();

            MapLegend(mapWidth);
        }

        /// <summary>
        /// Intro
        /// </summary>
        public void WriteIntro()
        {
            //Debug.WriteLine("Intro!");
            Console.WriteLine("Welcome to Dungeon Explorer!"); // placeholderTitle
            Console.WriteLine();

            //Console.Write("Escape the dungeon and climb to the 2nd floor to find the chalace. ");
            ////DisplayChar(Settings.finalLootChar, "Final Loot");
            //Console.ResetColor();
            //Console.WriteLine();

            Console.Write("Complete the quests to escape the dungeon but beware, \nif you go to a new floor you can't turn back!");
            Console.WriteLine();

            Console.WriteLine();
            Console.Write("Collect coins ");
            Console.ForegroundColor = Settings.coinColor; // change the color for coins\
            Console.Write(Settings.coinChar);
            Console.ResetColor();
            Console.Write(" to buy things at the store in each level.");
            Console.WriteLine();

            Console.Write("Collect hearts ");
            Console.ForegroundColor = Settings.healthColor; // change the color for health
            Console.Write(Settings.healthChar); // Write the health character
            Console.ResetColor();
            Console.Write(" to heal. ");
            Console.WriteLine();

            Console.Write("Collect pieces of armor ");
            Console.ForegroundColor = Settings.armorColor; // Change color for armor character
            Console.Write(Settings.armorChar); // Write the armor character
            Console.ResetColor();
            Console.WriteLine(" to up your defense.");

            Console.WriteLine();
            Console.Write("Avoid or fight the monsters!");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Press any key to get started!");
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Prints the event log
        /// </summary>
        private void EventLog()
        {
            Console.WriteLine("Event Log:");

            // Determine the number of blank lines needed based on the count of event messages
            int blankLinesToPrint = Math.Max(2 - eventMessages.Count, 0); // Maximum of 2 - current count, ensuring it's not negative

            // Print the required number of blank lines
            for (int i = 0; i < blankLinesToPrint; i++)
            {
                Console.WriteLine(); // Print a blank line
            }

            int currentCursorTop = Console.CursorTop;

            foreach (var message in eventMessages)
            {
                // Move the cursor to the start of the message's line
                Console.SetCursorPosition(0, currentCursorTop);
                // Clear the current line by overwriting it with spaces
                Console.Write(new string(' ', Console.WindowWidth));
                // Move the cursor back to the start of the line to print the new message
                Console.SetCursorPosition(0, currentCursorTop);

                // Write the event message
                Console.WriteLine(message);

                // Move cursor to the next line for the next message
                currentCursorTop++;
            }
        }


        /// <summary>
        /// Shows current player stats
        /// </summary>
        private void PlayerStats()
        {
            Console.Write($"Player Stats: Health: {player.healthSystem.health} - Armor: {player.healthSystem.armor} - Damage: {player.damage} - Coins: {player.PlayerCoins}      ");
            Console.WriteLine();
        }

        /// <summary>
        /// Adds event to event log
        /// </summary>
        /// <param name="message"></param>
        public void AddEventLogMessage(string message)
        {
            eventMessages.Add(message);

            // If there are more than MaxEventLogMessages, remove the oldest
            if (eventMessages.Count > MaxEventLogMessages)
                eventMessages.RemoveAt(0); // Remove the oldest message
        }

        private void MapLegend(int mapWidth)
        {
            // Start printing the legend at a specific column, say column (mapWidth + 2)
            int legendColumn = mapWidth + 2;

            // Print the legend header
            Console.SetCursorPosition(legendColumn, 2); // Set cursor to row 3 for the header
            Console.WriteLine("Legend:"); // Print the header

            // Print the legend at a fixed starting position for the rows
            int startingRow = 3; // Choose the row to start printing the legend items
            for (int i = 0; i < legendsChar.Length; i++)
            {
                Console.SetCursorPosition(legendColumn, startingRow + i); // Move cursor to the appropriate row
                DisplayChar(legendsChar[i]); // Print the character
                Console.Write($" - {Name(legendsChar[i])}");
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Displays char, decides color based on symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="name"></param>
        private void DisplayChar(char symbol)
        {
            Console.ForegroundColor = Color(symbol);
            Console.Write(symbol);
            Console.ResetColor();
        }

        private ConsoleColor Color(char avatar)
        {
            switch (avatar)
            {
                case Settings.PlasmoidChar:
                case Settings.ConstructChar:
                case Settings.GoblinFolkChar:
                    return Console.ForegroundColor;
                case Settings.healthChar: return Settings.healthColor;
                case Settings.coinChar: return Settings.coinColor;
                case Settings.armorChar: return Settings.armorColor;
                case Settings.swordChar: return Settings.swordColor;
                case Settings.finalLootChar: return Settings.finalLootColor;
                case Settings.spikeChar: return Settings.spikeColor;
                case Settings.storeChar: return ConsoleColor.Red;
                default: return ConsoleColor.Black;
            }
        }

        private string Name(char avatar)
        {
            switch (avatar)
            {
                case Settings.PlasmoidChar: return "Plasmoid";
                case Settings.ConstructChar: return "Construct";
                case Settings.GoblinFolkChar: return "Goblin Folk";
                case Settings.spikeChar: return "Spike";
                case Settings.healthChar: return "Health";
                case Settings.coinChar: return "Coins";
                case Settings.armorChar: return "Armor";
                case Settings.swordChar: return "Sword";
                case Settings.finalLootChar: return "Final Loot";
                case Settings.storeChar: return "Store";
                default: return "Character name not available";
            }
        }
    }
}