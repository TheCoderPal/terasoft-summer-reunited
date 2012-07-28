

namespace Game.Engine
{
    public class Food : Item
    {

        private int type;    
                
        public Food(int type)
        {
            this.type = type;
        }

        ///<remarks>Author: Ahmed Shirin</remarks>
        /// <summary>
        /// This function is used to return the effect of interacting with a given item.
        /// </summary>
        /// <returns></returns>
        public override int GetEffect() 
        {            
            return type*10;
        }

    }
}
