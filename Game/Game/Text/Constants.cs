using Microsoft.Kinect;

namespace Game.Text
{
    class Constants
    {
        public static double jumpFraction = 1.2;
        public static bool isJumping;
        public static bool isSwappingHand;
        public static bool isBending;
        public static bool isPunching;
        public static bool isDumbbell;
        public static bool isRunning;
        public static bool isSteppingRight;
        public static Skeleton oldSkeleton;
        public static double HipPosX; // Skeleton initial X position needed for step right gesture.
        public static double posY; //initial hip Y position to make sure the skeleton is running
        public static double posZ; //initial Z position of right hand needed for the punch gestureo ddetect the dumbbell playing gesture.
        public static double diffHandElbow;
        public static double elbowPosY;
        public static int healthy1 = 3;
        public static int healthy2 = 5;
        public static int healthy3 = 7;
        public static int healthy4 = 9;
        public static int healthy5 = 11;
        public static int healthy6 = 13;
        public static int unhealthy1 = -4;
        public static int unhealthy2 = -6;
        public static int unhealthy3 = -8;
        public static int unhealthy4 = -10;
        public static int unhealthy5 = -12;
        public static int unhealthy6 = -14;
        public static int level1 = -5;
        public static int level2 = -8;
        public static int level3 = -12;
        public static int sword1 = -2;
        public static int sword2 = -4;
        public static int sword3 = -6;



        public static void ResetFlags()
        {
        isJumping=false;
        isSwappingHand=false;
        isBending=false;
        isPunching=false;
        isDumbbell=false;
        isRunning=false;
        isSteppingRight=false;
        }
    }
}
