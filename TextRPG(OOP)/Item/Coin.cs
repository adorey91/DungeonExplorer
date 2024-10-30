using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal class Coin : Item
    {
        public Coin(ItemAttributes attributes) 
        {
            gainAmount = attributes.ItemGain;
            avatar = attributes.ItemChar;
            color = ConvertToConsoleColor(attributes.ItemColor);
            itemType = "Coin";
        }

        public override void Apply(Player player, UIManager uiManager, QuestManager questManager)
        {
            player.PlayerCoins += gainAmount;
            questManager.UpdateQuestProgress(questManager.questCollectCoins, gainAmount);
            uiManager.AddEventLogMessage($"{player.name} collected coin");
            isCollected = true;
        }
    }
}
