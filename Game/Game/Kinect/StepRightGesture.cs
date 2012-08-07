using Microsoft.Kinect;
using Game.Text;

namespace Game.Kinect
{
        
        class StepRightGesture1 : IRelativeGestureSegment
        {
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                Constants.HipPosX= skeleton.Joints[JointType.HipCenter].Position.X;
                if ((skeleton.Joints[JointType.Head].Position.X >= skeleton.Joints[JointType.HipCenter].Position.X-2)&&
                    (skeleton.Joints[JointType.Head].Position.X <= (skeleton.Joints[JointType.HipCenter].Position.X+2)))
                {
                    return GesturePartResult.Suceed;
                }
                return GesturePartResult.Fail;
            }
            class StepRightGesture2 : IRelativeGestureSegment
            {
                public GesturePartResult CheckGesture(Skeleton skeleton)
                {
                    if (skeleton.Joints[JointType.Head].Position.X >= (Constants.HipPosX + Constants.minimumDistanceMoved))
                    {
                        if (skeleton.Joints[JointType.HipCenter].Position.X >= (Constants.HipPosX + Constants.minimumDistanceMoved))
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
