using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    class PunchGesture : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (skeleton.Joints[JointType.HandRight].Position.Y >= skeleton.Joints[JointType.ElbowRight].Position.Y)
            {
                if (skeleton.Joints[JointType.HandRight].Position.Z > (skeleton.Joints[JointType.ElbowRight].Position.Z))
                {
                    return GesturePartResult.Suceed;
                }
                return GesturePartResult.Pausing;
            }
                return GesturePartResult.Fail;
            }
        }

    }

