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
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                if ((skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y + 5) && 
                    ((skeleton.Joints[JointType.HandLeft].Position.Z > skeleton.Joints[JointType.ElbowLeft].Position.Z + 5)))
                {
                    if((skeleton.Joints[JointType.HandLeft].Position.X <= skeleton.Joints[JointType.ElbowLeft].Position.X + 5) && (skeleton.Joints[JointType.HandLeft].Position.X >= skeleton.Joints[JointType.ElbowLeft].Position.X - 5))
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
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                if ((skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y + 5) &&
                    ((skeleton.Joints[JointType.HandRight].Position.Z > skeleton.Joints[JointType.ElbowRight].Position.Z + 5)))
                {
                    if ((skeleton.Joints[JointType.HandRight].Position.X <= skeleton.Joints[JointType.ElbowRight].Position.X + 5) &&
                        (skeleton.Joints[JointType.HandRight].Position.X >= skeleton.Joints[JointType.ElbowRight].Position.X - 5))
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