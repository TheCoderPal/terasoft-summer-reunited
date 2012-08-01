using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Engine
{
    public class Object
    {
        private string name;

        /// <summary>
        /// Author: Ahmed Shirin.
        /// </summary>
        public Object(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }
    }
}
