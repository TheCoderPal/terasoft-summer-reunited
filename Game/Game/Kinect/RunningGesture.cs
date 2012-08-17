using Microsoft.Kinect;

namespace Game.Kinect
{
    /// AUTHOR: Khaled
    class RunningGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (skeleton.Joints[JointType.KneeLeft].Position.Y > skeleton.Joints[JointType.KneeRight].Position.Y+0.004)
            {
                    return GesturePartResult.Suceed;
            }
            else return GesturePartResult.Fail;
        }
    }
    /// AUTHOR: Khaled
    class RunningGesture2 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if ((skeleton.Joints[JointType.KneeRight].Position.Y > skeleton.Joints[JointType.KneeLeft].Position.Y+0.004))
            {
                return GesturePartResult.Suceed;
            }
            else return GesturePartResult.Fail;
        }
    }

}


