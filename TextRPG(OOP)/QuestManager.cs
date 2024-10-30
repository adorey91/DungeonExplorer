using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class QuestManager
    {
        private QuestData questData;

        private List<Quest> quests;
        public GameManager gameManager;
        
        
        public QuestManager(GameManager gameManager, QuestData questData)
        {
            this.gameManager = gameManager;
            this.questData = questData;

            quests = new List<Quest>
            {
                new Quest(questData.Quest["CoinQuest"].Description,
                          questData.Quest["CoinQuest"].TargetAmount.ToString(),
                          gameManager), // Collect coins quest
                          
                new Quest(questData.Quest["EnemyQuest1"].Description,
                          questData.Quest["EnemyQuest1"].TargetAmount.ToString(),
                          gameManager), // Kill specific enemy quest
                          
                new Quest(questData.Quest["EnemyQuest2"].Description,
                          questData.Quest["EnemyQuest2"].TargetAmount.ToString(),
                          gameManager), // Kill another specific enemy quest
                          
                new Quest(questData.Quest["EnemyMultiQuest"].Description,
                          questData.Quest["EnemyMultiQuest"].TargetAmount.ToString(),
                          gameManager)   // Kill multiple enemies quest
            };
        }



        public void UpdateQuestProgress(string questDescription, int amount)
        {
            var quest = quests.FirstOrDefault(q => q.Description == questDescription);
            if (quest != null && !quest.IsCompleted) // Prevent updating if completed
            {
                quest.UpdateProgress(amount);
                CheckAllQuests();
            }
        }

        private void CheckAllQuests()
        {
            int count = 0;
            foreach (var quest in quests)
            {
                if (quest.IsCompleted)
                    count++;
                if (count == quests.Count)
                {
                    gameManager.GameOver = true;
                    gameManager.GameWon = true;
                }
            }
        }

        public void PrintQuestStatus()
        {
            Console.WriteLine("Quest Status: ");
            foreach (var quest in quests)
            {
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
