using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TextRPG_OOP_
{
    internal class DungeonExplorer
    {
        static GameManager gameManager = new GameManager();
        static void Main(string[] args)
        {
            Intro();
            gameManager.PlayGame();
        }
        static void Intro()
        {
            Debug.WriteLine("Into!");
            Console.WriteLine("Welcome to Dungeon Explorer!"); // placeholderTitle
            Console.WriteLine();
            Console.Write("Find your way to the challace. ");
            gameManager.gameMap.DrawFinalLoot();
            Console.WriteLine();
            Console.Write("Collect coins ");
            gameManager.gameMap.DrawCoin(); 
            Console.Write(" to increase your attack power.");
            Console.WriteLine();
            Console.Write("Collect hearts to heal.");
            gameManager.gameMap.DrawHealthPickup();
            Console.WriteLine();
            Console.Write("Collect peices of armor "); 
            gameManager.gameMap.DrawArmor();
            Console.Write(" to up your defence.");
            Console.WriteLine();
            Console.Write("Avoid or fight the monsters!");
            gameManager.gameMap.DrawEnemy(1);
            gameManager.gameMap.DrawEnemy(2);
            gameManager.gameMap.DrawEnemy(3);
            Console.WriteLine();
            Console.WriteLine("Press any key to get started!");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
