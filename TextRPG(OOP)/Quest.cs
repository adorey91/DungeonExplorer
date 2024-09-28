using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Quest
    {
        public string Description { get; private set; }
        public int TargetAmount { get; private set; }
        public int CurrentProgress { get; private set; }
        public bool IsCompleted { get; private set; }

        public UIManager uiManager;
        public GameManager gameManager;

        public Quest(string description, int targetAmount, GameManager gameManager)
        {
            Description = description;
            TargetAmount = targetAmount;
            CurrentProgress = 0;
            IsCompleted = false;
            this.gameManager = gameManager;
            this.uiManager = gameManager.uiManager;
        }

        public void UpdateProgress(int amount)
        {
            CurrentProgress += amount;
            if (CurrentProgress >= TargetAmount)
            {
                IsCompleted = true;
                uiManager.AddEventLogMessage($"Quest completed: {Description}");
            }
        }

        public void ResetProgress()
        {
            CurrentProgress = 0;
            IsCompleted = false;
        }
    }

}
