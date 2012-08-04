using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    public class BendGesture
    {
        static double posY;
        class BendGesture1 : IRelativeGestureSegment
        {
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                posY = skeleton.Joints[JointType.HipCenter].Position.Y;
                if (skeleton.Joints[JointType.Head].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y)
                {
                        return GesturePartResult.Suceed;
                }
                return GesturePartResult.Fail;
            }
        }
        class BendGesture2 : IRelativeGestureSegment
        {
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                if (skeleton.Joints[JointType.Head].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y)
                {
                    if (skeleton.Joints[JointType.HipCenter].Position.Y < (posY-5))
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