using Game.Text;
using Microsoft.Kinect;
using System.Diagnostics;

namespace Game.Kinect
{
       class RunningGesture1 : IRelativeGestureSegment
       {
           public GesturePartResult CheckGesture(Skeleton skeleton)
           {
               if (skeleton.Joints[JointType.KneeLeft].Position.Y > skeleton.Joints[JointType.KneeRight].Position.Y)
               {
                   return GesturePartResult.Pausing;
               }
               return GesturePartResult.Fail;
           }
       }
       class RunningGesture2 : IRelativeGestureSegment
       {
           public GesturePartResult CheckGesture(Skeleton skeleton)
           {
            if (skeleton.Joints[JointType.KneeRight].Position.Y > skeleton.Joints[JointType.KneeLeft].Position.Y)
               {
                   return GesturePartResult.Pausing;
               }
               return GesturePartResult.Fail;
           }
       }

   }
    

