using Microsoft.Kinect;
using Game.Text;

namespace Game.Kinect
{
    class BendGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            Constants.posY = skeleton.Joints[JointType.HipCenter].Position.Y;
            return GesturePartResult.Suceed;
        }
    }
    class BendGesture2 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (skeleton.Joints[JointType.HipCenter].Position.Y < (Constants.posY - Constants.minHipDiff))
            {
                return GesturePartResult.Suceed;
            }
            else return GesturePartResult.Fail;
        }
    }


}