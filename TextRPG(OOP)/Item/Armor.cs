using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal class Armor : Item
    {
        public Armor()
        {
            gainAmount = Settings.armorGain;
            avatar = Settings.armorChar;
            color = Settings.armorColor;
            itemType = "Armor";
            cost = Settings.armorCost;
        }


        public override void Apply(Player player, UIManager uiManager, QuestManager questManager)
        {
            player.healthSystem.IncreaseArmor(gainAmount);
            uiManager.AddEventLogMessage($"Player gained {gainAmount} armor");
            if (!player.boughtItem)
                isCollected = true;
        }
    }
}
