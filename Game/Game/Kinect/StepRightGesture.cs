using Game.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
            class StepRightGesture1 : IRelativeGestureSegment
            {
                public GesturePartResult CheckGesture(Skeleton skeleton)
                {
                    Joint hip;
                        hip = Constants.oldSkeleton.Joints[JointType.HipCenter];
                        if (skeleton.Joints[JointType.Head].Position.X > (hip.Position.X + 0.4))
                        {
                            if (skeleton.Joints[JointType.HipCenter].Position.X > hip.Position.X + 0.4)
                            {
                                return GesturePartResult.Suceed;
                            }
                            else return GesturePartResult.Fail;
                        }
                        return GesturePartResult.Fail;
                    }
            }
        }
