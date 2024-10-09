using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal class Sword: Item
    {
        public Sword()
        {
            gainAmount = Settings.swordGain;
            avatar = Settings.swordChar;
            color = Settings.swordColor;
            itemType = "Sword";
            cost = Settings.swordCost;
        }

        public override void Apply(Player player, UIManager uiManager, QuestManager questManager)
        {
            player.damage += gainAmount;
            uiManager.AddEventLogMessage($"Player gained {gainAmount} damage");
            if(!player.boughtItem)
                isCollected = true;
        }
    }
}