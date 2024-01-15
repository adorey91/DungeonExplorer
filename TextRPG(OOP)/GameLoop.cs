using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class GameLoop
    {
        static Player MainPlayer;
        static Enemy firstEnemy;
        static void Main(string[] args)
        {
            MainPlayer = new Player();
            firstEnemy = new Enemy();
            Console.WriteLine("Welcome to my Text RPG game!");
            Console.WriteLine();
            Console.WriteLine("The players HP: " + MainPlayer.PlayerHealth.health);
            Console.WriteLine("The Enemies HP: " + firstEnemy.EnemyHealth.health);
            MainPlayer.PlayerHealth.TakeDamage(10);
            firstEnemy.EnemyHealth.TakeDamage(20);
            Console.WriteLine("The players HP: " + MainPlayer.PlayerHealth.health);
            Console.WriteLine("The Enemies HP: " + firstEnemy.EnemyHealth.health);
            MainPlayer.PlayerHealth.Heal(5);
            firstEnemy.EnemyHealth.Heal(10);
            Console.WriteLine("The players HP: " + MainPlayer.PlayerHealth.health);
            Console.WriteLine("The Enemies HP: " + firstEnemy.EnemyHealth.health);
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }
    }
}
