using Microsoft.Kinect;
using Game.Text;
using System.Diagnostics;

namespace Game.Kinect
{
            class StepRightGesture1 : IRelativeGestureSegment
            {
                public GesturePartResult CheckGesture(Skeleton skeleton)
                {
                    if (skeleton.Joints[JointType.Head].Position.X >= (Constants.HipPosX))
                    {
                        if (skeleton.Joints[JointType.HipCenter].Position.X >= Constants.HipPosX)
                        {
                            return GesturePartResult.Suceed;
                        }
                        return GesturePartResult.Pausing;
                    }
                    return GesturePartResult.Fail;
                }

            }
        }
