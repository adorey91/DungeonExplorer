using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    internal class Store
    {
        public List<Item> itemsToBuy;
        private GameManager gameManager;
        private ItemManager itemManager;

        public Store()
        {
            itemsToBuy = new List<Item>();
        }

        public void Initialize(GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.itemManager = gameManager.itemManager;
        }

        public void Update()
        {

        }

        public bool CanPlayerBuyItem()
        {
            return true;
        }
    }
}
