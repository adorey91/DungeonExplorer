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
        public Armor(ItemAttributes attributes)
        {
            gainAmount = attributes.ItemGain;
            avatar = attributes.ItemChar;
            color = ConvertToConsoleColor(attributes.ItemColor);
            itemType = "Armor";
            cost = attributes.ItemCost;
        }


        public override void Apply(Player player, UIManager uiManager, QuestManager questManager)
        {
            player.healthSystem.IncreaseArmor(gainAmount);
            uiManager.AddEventLogMessage($"{player.name} gained {gainAmount} armor");
            if (!player.boughtItem)
                isCollected = true;
        }
    }
}
