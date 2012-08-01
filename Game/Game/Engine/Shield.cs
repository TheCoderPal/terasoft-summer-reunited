using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Engine
{
    class Shield : Collectible
    {
        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Shield(string name, bool acquired) : base(acquired, name)
        {

        }

        public void AcquireShield()
        {
            base.AcquireObject();
        }
    }
}
