using Game.Text;
using Microsoft.Kinect;
using System.Diagnostics;

namespace Game.Kinect
{
       class RunningGesture1 : IRelativeGestureSegment
       {
           public GesturePartResult CheckGesture(Skeleton skeleton)
           {
               if (skeleton.Joints[JointType.KneeLeft].Position.Y > skeleton.Joints[JointType.KneeRight].Position.Y+0.2)
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
            if (skeleton.Joints[JointType.KneeRight].Position.Y > skeleton.Joints[JointType.KneeLeft].Position.Y+0.2)
               {
                   return GesturePartResult.Suceed;
               }
               return GesturePartResult.Fail;
           }
       }

   }
    

