using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TextRPG_OOP_
{ 
    internal class Player : Character
    {
        public int experience;
        public static int playerDamage;
        public static int playerCoins;
        public int PlayerMaxHP;
        private static int playerHP;
        public string playerName;
        public ConsoleKeyInfo playerInput;
        public bool gameIsOver;
        public bool gameWon;
        public Player()
        {
            experience = 0;
            playerCoins = 0;
            playerDamage = 1; // player starting damage
            PlayerMaxHP = 10; // % health out of 10.
            healthSystem.SetHealth(PlayerMaxHP);
            playerName = "Koal"; // Testing for passing string.
            playerHP = healthSystem.GetHealth();
            //Console.Write("Initialized" + playerName);
        }
        public void SetMaxPlayerPosition(Map map)
        {
            int mapX;
            int mapY;
            mapX = map.activeMap.GetLength(1);
            mapY = map.activeMap.GetLength(0);
            position.maxX = mapX - 1;
            position.maxY = mapY - 1;
        }
        public void SetPlayerPosition(int x, int y)
        {
            position.x = x;
            position.y = y;
        }
        public void GetPlayerInput(Map collisionMap)
        {
            int moveX;
            int moveY;
            bool playerMoved;
            playerMoved = false;
            while(Console.KeyAvailable == false) 
            {
                Thread.Sleep(250); 
            }
            playerInput = Console.ReadKey(true);
            //Console.WriteLine(playerInput.Key); //debug to see what key is pressed
            if(playerMoved == false)
            {
                if(playerInput.Key == ConsoleKey.W || playerInput.Key == ConsoleKey.UpArrow)
                {
                    //Moves player up
                    moveY = (position.y - 1);
                    if(moveY <= 0)
                    {
                        moveY = 0; //Locks top of screen
                    }
                    if(collisionMap.CretureInTarget(moveY, position.x) && collisionMap.index != 0) // Player should always be 0, need to prevent self harm.
                    {
                        collisionMap.characters[collisionMap.index].healthSystem.TakeDamage(playerDamage);
                        return;
                    }
                    if(collisionMap.CheckTile(moveY, position.x) == false)
                    {
                        Debug.WriteLine("HitWall");
                        moveY = position.y;
                        position.y = moveY;
                        return;
                    }
                    else
                    {
                        playerMoved = true;
                        position.y = moveY;
                        if(position.y <= 0)
                        {
                            position.y = 0;
                        }
                    }
                }
                if(playerInput.Key == ConsoleKey.S || playerInput.Key == ConsoleKey.DownArrow)
                {
                    //Moves player down
                    moveY = (position.y + 1);
                    if(moveY >= position.maxY)
                    {
                        moveY = position.maxY; //Locks top of screen
                    }
                    if(collisionMap.CretureInTarget(moveY, position.x) && collisionMap.index != 0)
                    {
                        collisionMap.characters[collisionMap.index].healthSystem.TakeDamage(playerDamage);
                        return;
                    }
                    if(collisionMap.CheckTile(moveY, position.x) == false)
                    {
                        Debug.WriteLine("HitWall");
                        moveY = position.y;
                        position.y = moveY;
                        return;
                    }
                    else
                    {
                        playerMoved = true;
                        position.y = moveY;
                        if(position.y >= position.maxY)
                        {
                            position.y = position.maxY;
                        }
                    }
                }
                if(playerInput.Key == ConsoleKey.A || playerInput.Key == ConsoleKey.LeftArrow)
                {
                    //Moves player left
                    moveX = (position.x - 1);
                    if(moveX <= 0)
                    {
                        moveX = 0; //Locks top of screen
                    }
                    if(collisionMap.CretureInTarget(position.y, moveX) && collisionMap.index != 0)
                    {
                        collisionMap.characters[collisionMap.index].healthSystem.TakeDamage(playerDamage);
                        return;
                    }
                    if(collisionMap.CheckTile(position.y, moveX) == false)
                    {
                        Debug.WriteLine("HitWall");
                        moveX = position.x;
                        position.x = moveX;
                        return;
                    }
                    else
                    {
                        playerMoved = true;
                        position.x = moveX;
                        if(position.x <= 0)
                        {
                            position.x = 0;
                        }
                    }
                }
                if(playerInput.Key == ConsoleKey.D || playerInput.Key == ConsoleKey.RightArrow)
                {
                    //Moves player right
                    moveX = (position.x + 1);
                    if(moveX >= position.maxX)
                    {
                        moveX = position.maxX; //Locks top of screen
                    }
                    if(collisionMap.CretureInTarget(position.y, moveX) && collisionMap.index != 0)
                    {
                        collisionMap.characters[collisionMap.index].healthSystem.TakeDamage(playerDamage);
                        return;
                    }
                    if(collisionMap.CheckTile(position.y, moveX) == false)
                    {
                        Debug.WriteLine("HitWall");
                        moveX = position.x;
                        position.x = moveX;
                        return;
                    }
                    else
                    {
                        playerMoved = true;
                        position.x = moveX;
                        if(position.x >= position.maxX)
                        {
                            position.x = position.maxX;
                        }
                    }
                }
                if(collisionMap.activeMap[position.y,position.x] == '$')
                {
                    gameWon = true;
                    gameIsOver = true;
                }
                if(collisionMap.activeMap[position.y,position.x] == '~')
                {
                    collisionMap.levelNumber += 1;
                    collisionMap.ChangeLevels();
                }
                if(collisionMap.activeMap[position.y,position.x] == '@')
                {
                    playerCoins += 1;
                    UpPlayerStats();
                    collisionMap.activeMap[position.y,position.x] = '-';
                }
                if(collisionMap.activeMap[position.y,position.x] == '"' && healthSystem.health < PlayerMaxHP)
                {
                    healthSystem.Heal(collisionMap.levelNumber, PlayerMaxHP);
                    collisionMap.activeMap[position.y,position.x] = '-';
                }
                if(collisionMap.activeMap[position.y,position.x] == '*')
                    {
                        healthSystem.health -= 1;
                    }
                if(playerInput.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }
        public void DrawHUD()
        {
            int health;
            int damage;
            int Coins;
            int armor;
            health = healthSystem.health;
            armor = healthSystem.armor;
            damage = playerDamage;
            Coins = playerCoins;
            string FormatString = "HP: {0}  Damage: {1}    Coins: {2}   Armor: {3}    ";
            Console.WriteLine(string.Format(FormatString, health, damage, Coins, armor));
        }
        public void PlayerIsDead()
        {
            Thread.Sleep(3000); 
            Console.Clear();
            Console.WriteLine("You have lost. Reload the game to try again!");
            Thread.Sleep(5000);
            Environment.Exit(0);
        }
        public void CheckPlayerCondition()
        {
            if(!healthSystem.IsAlive)
            {
                PlayerIsDead();
            }
        }
        void UpPlayerStats()
        {
            if(playerCoins < 3)
            {
                playerDamage = 1;
                healthSystem.armor = 0;
            }
            if(playerCoins > 3 && playerCoins < 6)
            {
                playerDamage = 2;
                healthSystem.armor = 1;
            }
            if(playerCoins > 6 && playerCoins < 9)
            {
                playerDamage = 3;
                healthSystem.armor = 2;
            }
            if(playerCoins > 9)
            {
                playerDamage = 5;
                healthSystem.armor = 3;
            }
        }
    }
}
