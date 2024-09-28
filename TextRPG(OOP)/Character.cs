using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_OOP_
{
    /// <summary>
    /// Base class for player and all enemies. holds position and health system.
    /// </summary>
    internal abstract class Character
    {
        public HealthSystem healthSystem;
        public Position position;
        public string name;
        public char avatar;
        public int damage;
    }
}
