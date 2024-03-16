using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class EnemyManager
    {
        public List<Enemy> enemiesList; 
        public Map gameMap;
        public EnemyManager(Map map)
        {
            gameMap = map;
            enemiesList = new List<Enemy>();
        }
        public void AddEnemiesToList(string type)
        {
            Enemy enemy = new Enemy();
            enemy.enemyType = type;
            enemy.name = type;
            enemy.SetEnemyMaxPosition(gameMap);
            enemiesList.Add(enemy);
        }
        public void Update()
        {
            for(int i = 0; i < enemiesList.Count(); i++)
            {
                enemiesList[i].MoveEnemy(gameMap);
            }
        }
        public void Draw()
        {
            gameMap.DrawEnemiesToMap(enemiesList);
        }
    }
}
