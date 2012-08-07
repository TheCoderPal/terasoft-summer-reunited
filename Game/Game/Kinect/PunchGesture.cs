using Microsoft.Kinect;
using Game.Text;
using System.Diagnostics;

namespace Game.Kinect
{
        class PunchGesture1 : IRelativeGestureSegment
        {
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {      
                if ((skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ShoulderRight].Position.Y)
                    &&(skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y))
                {
                    if ((skeleton.Joints[JointType.HandRight].Position.X <skeleton.Joints[JointType.HipCenter].Position.X) &&
                        (skeleton.Joints[JointType.HandRight].Position.Z < (skeleton.Joints[JointType.ElbowRight].Position.Z)))
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
                if(skeleton.Joints[JointType.HandRight].Position.Z < Constants.posZ )
                    {
                        return GesturePartResult.Suceed;
                    }
                    return GesturePartResult.Fail;
                }
            }
        }

