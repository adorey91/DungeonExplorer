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
        public string questCollectCoins = $"Collect {Settings.questTargetCoin} coins";
        public string questKillEnemyType = $"Kill {Settings.questTargetEnemy1} {Settings.questEnemyType}";
        public string questKillEnemyType2 = $"Kill {Settings.questTargetEnemy2} {Settings.questEnemyType2}";
        public string questKillEnemies = $"Kill {Settings.questTargetMulti} enemies";
        
        public QuestManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
            quests = new List<Quest>
        {
            new Quest(questCollectCoins, Settings.questTargetCoin, gameManager), // Collect coins quest
            new Quest(questKillEnemyType, Settings.questTargetEnemy1, gameManager),    // Kill specific enemy quest
            new Quest(questKillEnemyType2 , Settings.questTargetEnemy2, gameManager), // kill other specific enemy quest
            new Quest(questKillEnemies, Settings.questTargetMulti, gameManager)   // Kill more enemies quest
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
