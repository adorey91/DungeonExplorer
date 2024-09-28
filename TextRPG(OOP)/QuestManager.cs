using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class QuestManager
    {
        private List<Quest> quests;
        public GameManager gameManager;
        
        public QuestManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
            quests = new List<Quest>
        {
            new Quest($"Collect {Settings.questTargetCoin} coins", Settings.questTargetCoin, gameManager), // Collect coins quest
            new Quest($"Kill {Settings.questTargetEnemy1} {Settings.questEnemyType}", Settings.questTargetEnemy1, gameManager),    // Kill specific enemy quest
            new Quest($"Kill {Settings.questTargetEnemy2} enemies", Settings.questTargetEnemy2, gameManager)   // Kill more enemies quest
        };
        }

        public void UpdateQuestProgress(string questDescription, int amount)
        {
            var quest = quests.FirstOrDefault(q => q.Description == questDescription);
            if (quest != null && !quest.IsCompleted) // Prevent updating if completed
            {
                quest.UpdateProgress(amount);
            }
        }

        public void PrintQuestStatus()
        {
            Console.WriteLine("Quest Status: ");
            foreach (var quest in quests)
            {
                // Clear the line by moving the cursor and overwriting with spaces
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);

                // Check if the quest is completed and apply strikethrough
                if (quest.IsCompleted)
                {
                    Console.Write($"{quest.Description} - Completed");
                    Console.WriteLine($"~~{quest.CurrentProgress}/{quest.TargetAmount}~~");
                }
                else
                {
                    Console.Write($"{quest.Description}: {quest.CurrentProgress}/{quest.TargetAmount} - In Progress");
                    Console.WriteLine();
                }
            }
        }
    }
}
