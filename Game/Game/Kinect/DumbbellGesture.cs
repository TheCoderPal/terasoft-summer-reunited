using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    public class DumbbellGesture
    {
        class DumbbellGesture1 : IRelativeGestureSegment
        {
            public static GesturePartResult CheckGesture(Skeleton skeleton)
            {
                if ((skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y + 5) && ((skeleton.Joints[JointType.HandLeft].Position.Z > skeleton.Joints[JointType.ElbowLeft].Position.Z + 5)))
                {
                    if (DumbbellGesture2.CheckGesture(skeleton)==GesturePartResult.Fail)
                    {
                        return GesturePartResult.Suceed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Fail;
            }
        }
        class DumbbellGesture2 : IRelativeGestureSegment
        {
            public static GesturePartResult CheckGesture(Skeleton skeleton)
            {
                if ((skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y + 5) && ((skeleton.Joints[JointType.HandRight].Position.Z > skeleton.Joints[JointType.ElbowRight].Position.Z + 5)))
                {
                    if (DumbbellGesture1.CheckGesture(skeleton) == GesturePartResult.Fail)
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