using Game.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    /// AUTHOR: Khaled
    class StepRightGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (skeleton.Joints[JointType.Head].Position.X > (Constants.HipPosX + 0.3))
            {
                if (skeleton.Joints[JointType.HipCenter].Position.X > Constants.HipPosX + 0.3)
                {
                    return GesturePartResult.Suceed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }

    }
}
