namespace Game
{
    public static class Program
    {
        
        public static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
        //Tamer for testing  [To be Removed After Integration]
        /*
        static void Main(string[] args)
        {
            kinect x = new kinect();
            while (x.swapFlag != true)
            {
                Console.WriteLine(x.swapFlag);
            }
            if (x.swapFlag == true)
                Console.WriteLine("true");
        }*/
  
    }
}

