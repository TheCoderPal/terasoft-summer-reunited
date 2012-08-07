using Game.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
       class RunningGesture1 : IRelativeGestureSegment
       {
           public GesturePartResult CheckGesture(Skeleton skeleton)
           {
               Constants.posY = skeleton.Joints[JointType.HipCenter].Position.Y;
               if (skeleton.Joints[JointType.KneeLeft].Position.Y > (skeleton.Joints[JointType.KneeRight].Position.Y+Constants.kneeDiff))
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
               if (skeleton.Joints[JointType.KneeRight].Position.Y > (skeleton.Joints[JointType.KneeLeft].Position.Y + Constants.kneeDiff))
               {
                   if (skeleton.Joints[JointType.HipCenter].Position.Y > (Constants.posY + Constants.hipDiff))
                   {
                       return GesturePartResult.Suceed;
                   }
                   return GesturePartResult.Pausing;
               }
               return GesturePartResult.Fail;
           }
       }
    
}
