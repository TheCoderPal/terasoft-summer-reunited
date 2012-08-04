using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
   public class RunningGesture
    {
       static double posY;
       class RunningGesture1 : IRelativeGestureSegment
       {
           public GesturePartResult CheckGesture(Skeleton skeleton)
           {
               posY = skeleton.Joints[JointType.HipCenter].Position.Y;
               if (skeleton.Joints[JointType.KneeLeft].Position.Y > (skeleton.Joints[JointType.KneeRight].Position.Y+10))
               {
                   return GesturePartResult.Suceed;
               }
               return GesturePartResult.Fail;
           }
       }
       class RunningGesture2 : IRelativeGestureSegment
       {
           public GesturePartResult CheckGesture(Skeleton skeleton)
           {
               if (skeleton.Joints[JointType.KneeRight].Position.Y > (skeleton.Joints[JointType.KneeLeft].Position.Y + 10))
               {
                   if (skeleton.Joints[JointType.HipCenter].Position.Y > (posY + 5))
                   {
                       return GesturePartResult.Suceed;
                   }
                   return GesturePartResult.Pausing;
               }
               return GesturePartResult.Fail;
           }
       }
    }
}
