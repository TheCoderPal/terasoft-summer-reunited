using Game.Text;
using Microsoft.Kinect;
using System.Diagnostics;

namespace Game.Kinect
{
       class RunningGesture1 : IRelativeGestureSegment
       {
           public GesturePartResult CheckGesture(Skeleton skeleton)
           {
               if (skeleton.Joints[JointType.FootLeft].Position.Y > skeleton.Joints[JointType.FootRight].Position.Y+0.05)
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
            if (skeleton.Joints[JointType.FootRight].Position.Y > skeleton.Joints[JointType.FootLeft].Position.Y+0.05)
               {
                   return GesturePartResult.Suceed;
               }
               return GesturePartResult.Fail;
           }
       }

   }
    

