﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_OOP_.TextRPG_OOP_;

namespace TextRPG_OOP_
{
    internal class Spike : Item
    {
        public Spike(ItemAttributes attributes)
        {
            gainAmount = attributes.ItemDamage;
            avatar = attributes.ItemChar;
            color = ConvertToConsoleColor(attributes.ItemColor);
            itemType = "Spike";
        }

        public override void Apply(Player player, UIManager uiManager, QuestManager questManager)
        {
            player.healthSystem.TakeDamage(gainAmount);
            uiManager.AddEventLogMessage($"{player.name} hurt by {itemType}, lost {gainAmount} health");
        }
    }
}
