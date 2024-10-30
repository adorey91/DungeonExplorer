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
        public Sword(ItemAttributes attributes)
        {
            gainAmount = attributes.ItemDamage;
            avatar = attributes.ItemChar;
            color = ConvertToConsoleColor(attributes.ItemColor);
            itemType = "Sword";
            cost = attributes.ItemCost;
        }

        public override void Apply(Player player, UIManager uiManager, QuestManager questManager)
        {
            player.damage += gainAmount;
            uiManager.AddEventLogMessage($"{player.name} gained {gainAmount} damage");
            if(!player.boughtItem)
                isCollected = true;

        }
    }
}