using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    /// <summary>
    /// Holds the base values for all Characater
    /// </summary>
    internal static class Settings
    {
        // Settings for map char
        public const char storeChar = 'S';
        public const char storeFloor = '%';
        public const char dungeonWall = '#';
        public const char dungeonFloor = '-';
        public const char stairs = '~';
        public const char newStairsChar = (char)30;
        public const char startingPosition = '=';

        // Settings for quests
        public static string questEnemyType = "Plasmoid";
        public static string questEnemyType2 = "Construct";
        public static int questTargetCoin = 20;
        public static int questTargetEnemy1 = 20;
        public static int questTargetEnemy2 = 10;
        public static int questTargetMulti = 30;

        //Base values for enemy stats. 
        public static int PlasmoidBaseHP = 3;
        public static int PlasmoidBaseDamage = 1;
        public const char PlasmoidChar = (char)4;
        public static int ConstructBaseHP = 3;
        public static int ConstructBaseDamage = 2;
        public const char ConstructChar = (char)5;
        public static int GoblinFolkBaseHP = 3;
        public static int GoblinFolkBaseDamage = 4;
        public const char GoblinFolkChar = (char)6;

        // base value for player stats
        public static int playerMaxHP = 15;
        public static int playerStartingCoins = 0;
        public static int PlayerBaseDamage = 1;
        public static string playerName = "Guard";

        // base values for items
        public static int healthGain = 3;
        public static int coinGain = 1;
        public static int armorGain = 1;
        public static int spikeDamage = 3;
        public static int swordGain = 2;

        // Color For Items
        public static ConsoleColor healthColor = ConsoleColor.Red;
        public static ConsoleColor coinColor = ConsoleColor.DarkYellow;
        public static ConsoleColor armorColor = ConsoleColor.DarkBlue;
        public static ConsoleColor finalLootColor = ConsoleColor.Yellow;
        public static ConsoleColor swordColor = ConsoleColor.DarkGreen;
        public static ConsoleColor spikeColor = ConsoleColor.Red;

        // Characters for Items
        public const char healthChar = (char)3;
        public const char coinChar = (char)164;
        public const char armorChar = (char)21;
        public const char finalLootChar = (char)165;
        public const char spikeChar = (char)23;
        public const char swordChar = '+';

        // Item Cost
        public static int healthCost = 7;
        public static int armorCost = 8;
        public static int swordCost = 10;
        //public static int healthCost = 1;
        //public static int armorCost = 1;
        //public static int swordCost = 1;
    }
}
