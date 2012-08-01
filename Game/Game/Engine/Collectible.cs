using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Engine
{
    class Collectible : Object
    {
        private bool acquired;

        public Collectible(bool acquired, string name) : base(name)
        {
            this.acquired = acquired;
        }

        public string GetName()
        {
            return base.GetName();
        }

        public bool GetAcquired()
        {
            return acquired;
        }

        public void AcquireObject()
        {
            this.acquired = true;
        }
    }
}
