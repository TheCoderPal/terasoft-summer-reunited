using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Text
{
    class Constants
    {
        public static bool isJumping;
        public static bool isSwappingHand;
        public static bool isBending;
        public static bool isPunching;
        public static bool isDumbbell;
        public static bool isRunning;
        public static bool isSteppingRight;
        public static double HipPosX; // Skeleton initial X position needed for step right gesture.
        public const double minimumDistanceMoved=10; //The minimum distance the skeleton has to move on x position to recognize the gesture
        public static double posY; //initial hip Y position to make sure the skeleton is running
        public const double kneeDiff = 5; //minimum distance between the two knees to detect the running gesture;
        public const double hipDiff = 2; //minimum change in hip position to be able to detect the running gesture
        public static double posZ; //initial Z position of right hand needed for the punch gesture
        public const double handElbowDiff=5; // tolerance in the difference between hand and elbow x,y,z positions
        public const double minHipDiff = 3; //minimum distance the user has to crouch to detect the bend gesture
        public const double handElbowYDiff = 5; //minimum Y distance difference between the hand and the elbow to ddetect the dumbbell playing gesture.
        public static double diffHandElbow;
        public static double elbowPosY;

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
