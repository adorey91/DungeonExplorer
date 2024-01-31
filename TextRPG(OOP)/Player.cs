using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{ 
    internal class Player : Character
    {
        public int experience;
        public int playerDamage;
        public int playerCoins;
        public int PlayerMaxHP;
        public string playerName;
        public ConsoleKeyInfo playerInput;
        public bool gameIsOver;
        public bool gameWon;
        public Player()
        {
            experience = 0;
            playerCoins = 0;
            playerDamage = 10; // player starting damage
            PlayerMaxHP = 100; // % health out of 100.
            healthSystem.SetHealth(PlayerMaxHP);
            playerName = "Koal"; // Testing for passing string.
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
                    if(moveY == collisionMap.enemy1Y && position.x == collisionMap.enemy1X)
                    {
                        //DoDamage(playerDamage, 1);
                        return;
                    }
                    if(moveY == collisionMap.enemy2Y && position.x == collisionMap.enemy2X)
                    {
                        //DoDamage(playerDamage, 2);
                        return;
                    }
                    if(moveY == collisionMap.enemy3Y && position.x == collisionMap.enemy3X)
                    {
                        //DoDamage(playerDamage, 3);
                        return;
                    }
                    if(moveY == collisionMap.enemy4Y && position.x == collisionMap.enemy4X)
                    {
                        //DoDamage(playerDamage, 4);
                        return;
                    }
                    if(collisionMap.activeMap[moveY,position.x] == '#')
                    {
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
                    if(moveY == collisionMap.enemy1Y && position.x == collisionMap.enemy1X)
                    {
                        //DoDamage(playerDamage, 1);
                        return;
                    }
                    if(moveY == collisionMap.enemy2Y && position.x == collisionMap.enemy2X)
                    {
                        //DoDamage(playerDamage, 2);
                        return;
                    }
                    if(moveY == collisionMap.enemy3Y && position.x == collisionMap.enemy3X)
                    {
                        //DoDamage(playerDamage, 3);
                        return;
                    }
                    if(moveY == collisionMap.enemy4Y && position.x == collisionMap.enemy4X)
                    {
                        //DoDamage(playerDamage, 4);
                        return;
                    }
                    if(collisionMap.activeMap[moveY,position.x] == '#')
                    {
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
                    if(moveX == collisionMap.enemy1X && position.y == collisionMap.enemy1Y)
                    {
                        //DoDamage(playerDamage, 1);
                        return;
                    }
                    if(moveX == collisionMap.enemy2X && position.y == collisionMap.enemy2Y)
                    {
                        //DoDamage(playerDamage, 2);
                        return;
                    }
                    if(moveX == collisionMap.enemy3X && position.y == collisionMap.enemy3Y)
                    {
                        //DoDamage(playerDamage, 3);
                        return;
                    }
                    if(moveX == collisionMap.enemy4X && position.y == collisionMap.enemy4Y)
                    {
                        //DoDamage(playerDamage, 4);
                        return;
                    }
                    
                    if(collisionMap.activeMap[position.y,moveX] == '#')
                    {
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
                    if(moveX == collisionMap.enemy1X && position.y == collisionMap.enemy1Y)
                    {
                        //DoDamage(playerDamage, 1);
                        return;
                    }
                    if(moveX == collisionMap.enemy2X && position.y == collisionMap.enemy2Y)
                    {
                        //DoDamage(playerDamage, 2);
                        return;
                    }
                    if(moveX == collisionMap.enemy3X && position.y == collisionMap.enemy3Y)
                    {
                        //DoDamage(playerDamage, 3);
                        return;
                    }
                    if(moveX == collisionMap.enemy4X && position.y == collisionMap.enemy4Y)
                    {
                        //DoDamage(playerDamage, 4);
                        return;
                    }
                    
                    if(collisionMap.activeMap[position.y,moveX] == '#')
                    {
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
                    //gameWon = true;
                    //gameIsOver = true;
                }
                //if(collisionMap.activeMap[position.y,position.x] == '~' && enemyCount <= 0)
                {
                    //levelNumber += 1;
                    //ChangeLevels();
                }
                if(collisionMap.activeMap[position.y,position.x] == '@')
                {
                    playerCoins += 1;
                    collisionMap.activeMap[position.y,position.x] = '-';
                }
                if(collisionMap.activeMap[position.y,position.x] == '"' && healthSystem.health < PlayerMaxHP)
                {
                    healthSystem.Heal(10, PlayerMaxHP);
                    collisionMap.activeMap[position.y,position.x] = '-';
                }
                if(collisionMap.activeMap[position.y,position.x] == '*')
                    {
                        healthSystem.health -= 1;
                        if(healthSystem.health <= 0)
                        {
                            gameIsOver = true;
                            gameWon = false;
                        }
                    }
                if(playerInput.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
