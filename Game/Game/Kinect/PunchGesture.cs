using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    public class PunchGesture
    {
        static double posZ;
        class PunchGesture1 : IRelativeGestureSegment
        {
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                posZ = skeleton.Joints[JointType.HandRight].Position.Z;
                if (skeleton.Joints[JointType.HandRight].Position.Y <= (skeleton.Joints[JointType.ElbowRight].Position.Y + 5) && (skeleton.Joints[JointType.HandRight].Position.Y >= (skeleton.Joints[JointType.ElbowRight].Position.Y - 5)))
                {
                    if (skeleton.Joints[JointType.HandRight].Position.X <= (skeleton.Joints[JointType.ElbowRight].Position.X + 5) && (skeleton.Joints[JointType.HandRight].Position.X >= (skeleton.Joints[JointType.ElbowRight].Position.X - 5)) && (skeleton.Joints[JointType.HandRight].Position.Z > (skeleton.Joints[JointType.ElbowRight].Position.Z)))
                    {
                        return GesturePartResult.Suceed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Fail;
            }
        }
        class PunchGesture2 : IRelativeGestureSegment
        {
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                if (skeleton.Joints[JointType.HandRight].Position.Y <= (skeleton.Joints[JointType.ElbowRight].Position.Y + 5) && (skeleton.Joints[JointType.HandRight].Position.Y >= (skeleton.Joints[JointType.ElbowRight].Position.Y - 5)))
                {
                    if (skeleton.Joints[JointType.HandRight].Position.X <= (skeleton.Joints[JointType.ElbowRight].Position.X + 5) && (skeleton.Joints[JointType.HandRight].Position.X >= (skeleton.Joints[JointType.ElbowRight].Position.X - 5)) && (skeleton.Joints[JointType.HandRight].Position.Z > (skeleton.Joints[JointType.ElbowRight].Position.Z)) && (skeleton.Joints[JointType.HandRight].Position.Z>(posZ+10)))
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

