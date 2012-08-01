using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Engine
{
    class Sword : Collectible
    {
        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Sword(string name, bool acquired) : base(acquired, name)
        {

        }

        public void AcquireSword()
        {
            base.AcquireObject();
        }
    }
}
