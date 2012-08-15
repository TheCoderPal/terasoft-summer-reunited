using Game.Text;
using Microsoft.Kinect;
using System.Diagnostics;

namespace Game.Kinect
{
    class DumbbellGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (Constants.oldSkeleton != null)
            {
                if (skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.ElbowLeft].Position.Z)
                {
                    // Hands between shoulder and hip
                    if ((skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y) &&
                        (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderLeft].Position.X) &&
                        (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.HipCenter].Position.X) &&
                         (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.HandRight].Position.Y))
                    {
                        return GesturePartResult.Suceed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }
    class DumbbellGesture2 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (Constants.oldSkeleton != null)
            {
                if (skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z)
                {
                    // Hands between shoulder and hip
                    if ((skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y) &&
                        (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X) &&
                         (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.HandLeft].Position.Y))
                    {
                        return GesturePartResult.Suceed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }
}