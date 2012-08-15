using Microsoft.Kinect;
using Game.Text;
using System.Diagnostics;

namespace Game.Kinect
{
            class StepRightGesture1 : IRelativeGestureSegment
            {
                public GesturePartResult CheckGesture(Skeleton skeleton)
                {
                    if (skeleton.Joints[JointType.Head].Position.X > (Constants.hipPosX+0.4))
                    {
                        if (skeleton.Joints[JointType.HipCenter].Position.X > Constants.hipPosX + 0.4)
                        {
                            return GesturePartResult.Suceed;
                        }
                        else return GesturePartResult.Fail;
                    }
                    return GesturePartResult.Fail;
                }

            }
        }
