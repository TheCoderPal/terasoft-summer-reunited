using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    class BendGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (skeleton.Joints[JointType.Head].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y)
            {
                if (skeleton.Joints[JointType.Head].Position.Z > (skeleton.Joints[JointType.HipCenter].Position.Z+20))
                {
                    return GesturePartResult.Suceed;
                }
                return GesturePartResult.Pausing;
            }
                return GesturePartResult.Fail;
            }
        }

    }
