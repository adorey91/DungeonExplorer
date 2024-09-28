using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Initialize(GameManager gameManager)
        {
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


        public void WriteIntro()
        {
            Debug.WriteLine("Into!");
            Console.WriteLine("Welcome to Dungeon Explorer!"); // placeholderTitle
            Console.WriteLine();

            Console.Write("Escape the dungeon and climb to the 2nd floor to find the chalace. ");
            Console.ForegroundColor = Settings.finalLootColor; // change the color for final loot
            Console.Write(Settings.finalLootChar); // Write the health character
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Collect coins ");
            Console.ForegroundColor = Settings.coinColor; // change the color for coins\
            Console.Write(Settings.coinChar);
            Console.ResetColor();
            Console.Write(" to buy things at the store at the end of each level.");
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

            Console.Write("Avoid or fight the monsters!");
            Console.WriteLine();

            Console.WriteLine("Press any key to get started!");
            Console.ReadKey(true);
            Console.Clear();
        }


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

            // Print existing event messages
            foreach (var message in eventMessages)
            {
                // Clear the line by moving the cursor and overwriting with spaces
                Console.SetCursorPosition(0, Console.CursorTop); // Move to the start of the current line
                Console.Write(new string(' ', Console.WindowWidth)); // Clear the line with spaces
                Console.SetCursorPosition(0, Console.CursorTop - 1); // Move back to the start of the line
                Console.WriteLine(message); // Write the event message
            }
        }


        private void PlayerStats()
        {
            Console.Write($"Player Stats: Health: {player.healthSystem.health} - Armor: {player.healthSystem.armor} - Damage: {player.damage} - Coins: {player.PlayerCoins}      ");
            Console.WriteLine();
        }

        public void AddEventLogMessage(string message)
        {
            eventMessages.Add(message);

            // If there are more than MaxEventLogMessages, remove the oldest
            if (eventMessages.Count > MaxEventLogMessages)
            {
                eventMessages.RemoveAt(0); // Remove the oldest message
            }
        }

        private void MapLegend(int mapWidth)
        {
            // Start printing the legend at a specific column, say column (mapWidth + 2)
            int legendColumn = mapWidth + 2;

            // Print the legend header
            Console.SetCursorPosition(legendColumn, 3); // Set cursor to row 3 for the header
            Console.WriteLine("Legend:"); // Print the header

            // Define the legends to display
            var legends = new (char symbol, string name)[]
            {
        (Settings.PlasmoidChar, "Plasmoid"),
        (Settings.ConstructChar, "Construct"),
        (Settings.GoblinFolkChar, "Goblin Folk"),
        (Settings.spikeChar, "Spike"),
        (Settings.healthChar, "Health"),
        (Settings.coinChar, "Coins"),
        (Settings.armorChar, "Armor"),
        (Settings.finalLootChar, "Final Loot"),
            };

            // Print the legend at a fixed starting position for the rows
            int startingRow = 5; // Choose the row to start printing the legend items
            for (int i = 0; i < legends.Length; i++)
            {
                Console.SetCursorPosition(legendColumn, startingRow + i); // Move cursor to the appropriate row
                DisplayChar(legends[i].symbol, legends[i].name); // Print the character and its name
            }
        }



        private void DisplayChar(char symbol, string name)
        {
            Color(symbol);
            Console.Write(symbol);
            Console.ResetColor();
            Console.Write($" - {name}");
        }

        private void Color(char avatar)
        {
            switch (avatar)
            {
                case Settings.healthChar: // Health symbol
                    Console.ForegroundColor = Settings.healthColor;
                    break;

                case Settings.coinChar:
                    Console.ForegroundColor = Settings.coinColor; // Example color
                    break;

                case Settings.armorChar:
                    Console.ForegroundColor = Settings.armorColor; // Example color
                    break;

                case Settings.finalLootChar:
                    Console.ForegroundColor = Settings.finalLootColor; // Example color
                    break;
                case Settings.spikeChar:
                    Console.ForegroundColor = Settings.spikeColor; // Example color
                    break;
            }
        }

    }
}
