using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    public class StepRightGesture
    {
        static double posX;
        class StepRightGesture1 : IRelativeGestureSegment
        {
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                posX = skeleton.Joints[JointType.HipCenter].Position.Z;
                if (skeleton.Joints[JointType.Head].Position.X == skeleton.Joints[JointType.HipCenter].Position.X)
                {
                    if (skeleton.Joints[JointType.Head].Position.Z == (skeleton.Joints[JointType.HipCenter].Position.Z))
                    {
                        return GesturePartResult.Suceed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Fail;
            }
            class StepRightGesture2 : IRelativeGestureSegment
            {
                public GesturePartResult CheckGesture(Skeleton skeleton)
                {
                    if (skeleton.Joints[JointType.Head].Position.X >posX)
                    {
                        if (skeleton.Joints[JointType.HipCenter].Position.X >posX)
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
}
