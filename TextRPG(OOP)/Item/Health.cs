using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal class Health : Item
    {
        public Health(ItemAttributes attributes)
        {
            gainAmount = attributes.ItemGain;
            avatar = attributes.ItemChar;
            color = ConvertToConsoleColor(attributes.ItemColor);
            itemType = "Health";
            cost = attributes.ItemCost;
        }


        public override void Apply(Player player, UIManager uiManager, QuestManager questManager)
        {
            int previousHealth = player.healthSystem.health;
            if (player.healthSystem.health != player.healthSystem.maxHealth)
            {
                player.healthSystem.Heal(gainAmount);
                int healedAmount = player.healthSystem.health - previousHealth;
                uiManager.AddEventLogMessage($"{player.name} healed {healedAmount}");
                if (!player.boughtItem)
                    isCollected = true;
            }
            else
            {
                uiManager.AddEventLogMessage("Cannot heal anymore");
            }
        }

    }
}
