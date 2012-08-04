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
                posX = skeleton.Joints[JointType.HipCenter].Position.X;
                if ((skeleton.Joints[JointType.Head].Position.X >= skeleton.Joints[JointType.HipCenter].Position.X+2) ||(skeleton.Joints[JointType.Head].Position.X <= (skeleton.Joints[JointType.HipCenter].Position.X+2)))
                {
                    return GesturePartResult.Suceed;
                }
                return GesturePartResult.Fail;
            }
            class StepRightGesture2 : IRelativeGestureSegment
            {
                public GesturePartResult CheckGesture(Skeleton skeleton)
                {
                    if (skeleton.Joints[JointType.Head].Position.X >= (posX+10))
                    {
                        if (skeleton.Joints[JointType.HipCenter].Position.X >=(posX+10))
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
