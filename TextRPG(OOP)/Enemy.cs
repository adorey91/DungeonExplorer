using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TextRPG_OOP_
{
    internal class Enemy : Character
    {
        //public HealthSystem EnemyHealth; old
        public int enemyDamage;
        public int expDrop;
        public int enemyMaxHP;
        public int enemyNumber;
        public int enemyMaxX;
        public int enemyMaxY;
        public int enemyType;
        public int levelNumber;
        public Enemy()
        {
            enemyMaxHP = 2;
            expDrop = 5;
            enemyDamage = 1;
            healthSystem.SetHealth(enemyMaxHP);
            enemyType = 1;
            //Console.Write("Initialized enemy");
        }
        public void SetEnemyMaxPosition(Map map)
        {
            int mapX;
            int mapY;
            mapX = map.activeMap.GetLength(1);
            mapY = map.activeMap.GetLength(0);
            enemyMaxX = mapX - 1;
            enemyMaxY = mapY - 1;
        }
        public void SetEnemyStats()
        {
            if(enemyType == 1)
            {
                healthSystem.health = 3 + levelNumber;
                healthSystem.armor = levelNumber - 1;
            }
            if(enemyType == 2)
            {
                healthSystem.health = 0 + levelNumber;
                healthSystem.armor = 0;
            }
            if(enemyType == 3)
            {
                healthSystem.health = 5 + levelNumber;
                healthSystem.armor = levelNumber;
            }
        }
        public void MoveEnemy(Map gameMap)
        {
            int enemyMoveX;
            int enemyMoveY;
            // move up
            if(healthSystem.IsAlive == false)
            {
                position.x = 0;
                position.y = 0;
            }
            if(enemyType == 1) // this type moves at random
            {
                Random moveRoll = new Random();
                int moveResult = moveRoll.Next(1,5);
                if(moveResult == 1)
            {
                enemyMoveY = position.y - 1;
                if(enemyMoveY <= 0)
                {
                    enemyMoveY = 0;
                }
                if(gameMap.CretureInTarget(enemyMoveY, position.x) && gameMap.index != enemyNumber) // != enemyNumber is needed to prevent enemies from self harming
                {
                    gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                    return;
                }
                if(gameMap.CheckTile(enemyMoveY, position.x) == false)
                {
                    Debug.WriteLine("HitWall");
                    enemyMoveY = position.y;
                    position.y = enemyMoveY;
                    return;
                }
                else
                {
                    //Debug.WriteLine("Moved up");
                    position.y = enemyMoveY;
                    if(position.y <= 0)
                    {
                        position.y = 0;
                    }
                }
            }
            // move down
            if(moveResult == 2)
            {
                enemyMoveY = position.y + 1;
                if(enemyMoveY >= enemyMaxY)
                {
                    enemyMoveY = enemyMaxY;
                }
                if(gameMap.CretureInTarget(enemyMoveY, position.x) && gameMap.index != enemyNumber)
                {
                    gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                    return;
                }
                if(gameMap.CheckTile(enemyMoveY, position.x) == false)
                {
                    Debug.WriteLine("HitWall");
                    enemyMoveY = position.y;
                    position.y = enemyMoveY;
                    return;
                }
                else
                {
                    Debug.WriteLine("Moved down");
                    position.y = enemyMoveY;
                    if(position.y >= enemyMaxY)
                    {
                        position.y = enemyMaxY;
                    }
                }
            }
            // move left
            if(moveResult == 3)
            {
                enemyMoveX = position.x - 1;
                if(enemyMoveX >= enemyMaxX)
                {
                    enemyMoveX = enemyMaxX;
                }
                if(enemyMoveX <= 0)
                {
                    enemyMoveX = 0;
                }
                if(gameMap.CretureInTarget(position.y, enemyMoveX)&& gameMap.index != enemyNumber)
                {
                    gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                    return;
                }
                if(gameMap.CheckTile(position.y, enemyMoveX) == false)
                {
                    Debug.WriteLine("HitWall");
                    enemyMoveX = position.x;
                    position.x = enemyMoveX;
                    return;
                }
                else
                {
                    //Debug.WriteLine("Moved left");
                    position.x = enemyMoveX;
                    if(position.x <= 0)
                    {
                        position.x = 0;
                    }
                }
            }
            // move
            if(moveResult == 4)
            {
                enemyMoveX = position.x + 1;
                if(gameMap.CretureInTarget(position.y, enemyMoveX)&& gameMap.index != enemyNumber)
                {
                    gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                    return;
                }
                if(gameMap.CheckTile(position.y, enemyMoveX) == false)
                {
                    Debug.WriteLine("HitWall");
                    enemyMoveX = position.x;
                    position.x = enemyMoveX;
                    return;
                }
                else
                {
                    //Debug.WriteLine("Moved right");
                    position.x = enemyMoveX;
                    if(position.x >= enemyMaxX)
                    {
                        position.x = enemyMaxX;
                    }
                }
            }
            }
            if(enemyType == 2) // this type will flee from player
            {
                int rangeMaxX = 7;
            int rangeMaxY = 5;
            int rangeX = position.x - gameMap.characters[0].position.x; //characters[0] is always the player!
            int rangeY = position.y - gameMap.characters[0].position.y;
            if((rangeX < rangeMaxX && rangeX > -rangeMaxX)&&(rangeY < rangeMaxY && rangeY > -rangeMaxY))
            {
                if(rangeX < rangeMaxX && rangeX > 0)
                {
                    enemyMoveX = position.x + 1;
                    Debug.WriteLine("Moved ");
                    if(gameMap.CretureInTarget(position.y, enemyMoveX)&& gameMap.index != enemyNumber)
                    {
                        gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                        return;
                    }
                    if(gameMap.CheckTile(position.y, enemyMoveX) == false)
                    {
                        Debug.WriteLine("HitWall");
                        enemyMoveX = position.x;
                        position.x = enemyMoveX;
                        return;
                    }
                    else
                    {
                        position.x = enemyMoveX;
                        if(gameMap.characters[0].position.x >= enemyMaxX)
                        {
                            position.x = enemyMaxX;
                        }
                        return;
                    }
                }
                if(rangeX > -rangeMaxX && rangeX < 0)
                {
                    enemyMoveX = position.x - 1;
                    if(enemyMoveX >= enemyMaxX)
                    {
                        enemyMoveX = enemyMaxX;
                    }
                    if(enemyMoveX <= 0)
                    {
                        enemyMoveX = 0;
                    }
                    if(gameMap.CretureInTarget(position.y, enemyMoveX)&& gameMap.index != enemyNumber)
                    {
                        gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                        return;
                    }
                    if(gameMap.CheckTile(position.y, enemyMoveX) == false)
                    {
                        Debug.WriteLine("HitWall");
                        enemyMoveX = position.x;
                        position.x = enemyMoveX;
                        return;
                    }
                    else
                    {
                        position.x = enemyMoveX;
                        if(gameMap.characters[enemyNumber].position.x >= enemyMaxX)
                        {
                            position.x = enemyMaxX;
                        }
                        return;
                    }
                }
            }
            if((rangeX < rangeMaxX && rangeX > -rangeMaxX)&&(rangeY < rangeMaxY && rangeY > -rangeMaxY))
            {
                if(rangeY < rangeMaxY && rangeY > 0)
                {
                    enemyMoveY = position.y + 1;
                    if(enemyMoveY >= enemyMaxY)
                    {
                        enemyMoveY = enemyMaxY;
                    }
                    if(gameMap.CretureInTarget(enemyMoveY, position.x) && gameMap.index != enemyNumber) // != enemyNumber is needed to prevent enemies from self harming
                    {
                        gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                        return;
                    }
                    if(gameMap.CheckTile(enemyMoveY, position.x) == false)
                    {
                        Debug.WriteLine("HitWall");
                        enemyMoveY = position.y;
                        position.y = enemyMoveY;
                        return;
                    }
                    else
                    {
                        position.y = enemyMoveY;
                        if(position.y >= enemyMaxY)
                        {
                            position.y = enemyMaxY;
                        }
                        return;
                    }
                }
                if(rangeY > -rangeMaxY && rangeY < 0)
                {
                    enemyMoveY = position.y - 1;
                    if(enemyMoveY <= 0)
                    {
                        enemyMoveY = 0;
                    }
                    if(gameMap.CretureInTarget(enemyMoveY, position.x) && gameMap.index != enemyNumber) // != enemyNumber is needed to prevent enemies from self harming
                    {
                        gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                        return;
                    }
                    if(gameMap.CheckTile(enemyMoveY, position.x) == false)
                    {
                        Debug.WriteLine("HitWall");
                        enemyMoveY = position.y;
                        position.y = enemyMoveY;
                        return;
                    }
                    else
                    {
                        position.y = enemyMoveY;
                        if(position.y <= 0)
                        {
                            position.y = 0;
                        }
                        return;
                    }
                }
                else
                {return;}
            }
            }
            if(enemyType == 3) // this type will chase player
            {
                int rangeMaxX = 7;
                int rangeMaxY = 5;
                int rangeX = position.x - gameMap.characters[0].position.x; //characters[0] is always the player!
                int rangeY = position.y - gameMap.characters[0].position.y;
                if((rangeX < rangeMaxX && rangeX > -rangeMaxX)&&(rangeY < rangeMaxY && rangeY > -rangeMaxY))
                {
                    if(rangeX < rangeMaxX && rangeX > 0)
                    {
                        enemyMoveX = position.x - 1;
                        Debug.WriteLine("Moved ");
                        if(gameMap.CretureInTarget(position.y, enemyMoveX)&& gameMap.index != enemyNumber)
                        {
                            gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                            return;
                        }
                        if(gameMap.CheckTile(position.y, enemyMoveX) == false)
                        {
                            Debug.WriteLine("HitWall");
                            enemyMoveX = position.x;
                            position.x = enemyMoveX;
                            return;
                        }
                        else
                        {
                            position.x = enemyMoveX;
                            if(gameMap.characters[0].position.x >= enemyMaxX)
                            {
                                position.x = enemyMaxX;
                            }
                            return;
                        }
                    }
                    if(rangeX > -rangeMaxX && rangeX < 0)
                    {
                        enemyMoveX = position.x + 1;
                        if(enemyMoveX >= enemyMaxX)
                        {
                            enemyMoveX = enemyMaxX;
                        }
                        if(enemyMoveX <= 0)
                        {
                            enemyMoveX = 0;
                        }
                        if(gameMap.CretureInTarget(position.y, enemyMoveX)&& gameMap.index != enemyNumber)
                        {
                            gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                            return;
                        }
                        if(gameMap.CheckTile(position.y, enemyMoveX) == false)
                        {
                            Debug.WriteLine("HitWall");
                            enemyMoveX = position.x;
                            position.x = enemyMoveX;
                            return;
                        }
                        else
                        {
                            position.x = enemyMoveX;
                            if(gameMap.characters[enemyNumber].position.x >= enemyMaxX)
                            {
                                position.x = enemyMaxX;
                            }
                            return;
                        }
                    }
                }
                if((rangeX < rangeMaxX && rangeX > -rangeMaxX)&&(rangeY < rangeMaxY && rangeY > -rangeMaxY))
                {
                    if(rangeY < rangeMaxY && rangeY > 0)
                    {
                        enemyMoveY = position.y - 1;
                        if(enemyMoveY >= enemyMaxY)
                        {
                            enemyMoveY = enemyMaxY;
                        }
                        if(gameMap.CretureInTarget(enemyMoveY, position.x) && gameMap.index != enemyNumber) // != enemyNumber is needed to prevent enemies from self harming
                        {
                            gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                            return;
                        }
                        if(gameMap.CheckTile(enemyMoveY, position.x) == false)
                        {
                            Debug.WriteLine("HitWall");
                            enemyMoveY = position.y;
                            position.y = enemyMoveY;
                            return;
                        }
                        else
                        {
                            position.y = enemyMoveY;
                            if(position.y >= enemyMaxY)
                            {
                                position.y = enemyMaxY;
                            }
                            return;
                        }
                    }
                    if(rangeY > -rangeMaxY && rangeY < 0)
                    {
                        enemyMoveY = position.y + 1;
                        if(enemyMoveY <= 0)
                        {
                            enemyMoveY = 0;
                        }
                        if(gameMap.CretureInTarget(enemyMoveY, position.x) && gameMap.index != enemyNumber) // != enemyNumber is needed to prevent enemies from self harming
                        {
                            gameMap.characters[gameMap.index].healthSystem.TakeDamage(enemyDamage);
                            return;
                        }
                        if(gameMap.CheckTile(enemyMoveY, position.x) == false)
                        {
                            Debug.WriteLine("HitWall");
                            enemyMoveY = position.y;
                            position.y = enemyMoveY;
                            return;
                        }
                        else
                        {
                            position.y = enemyMoveY;
                            if(position.y <= 0)
                            {
                                position.y = 0;
                            }
                            return;
                        }
                    }
                    else
                    {return;}
                }
            }
        }
        public void SetLevelNumber(int level)
        {
            levelNumber = level;
        }
    }
}
