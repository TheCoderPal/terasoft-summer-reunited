using Game.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
        class DumbbellGesture1 : IRelativeGestureSegment
        {
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                if ((skeleton.Joints[JointType.HandLeft].Position.X <= skeleton.Joints[JointType.HipCenter].Position.X) &&
                 (skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.ElbowLeft].Position.Z + (Constants.handElbowDiff/2.5)))
                {
                    if ((skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y + Constants.handElbowYDiff) &&
                        (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ShoulderLeft].Position.Y))  
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

                if ((skeleton.Joints[JointType.HandRight].Position.X <= skeleton.Joints[JointType.HipCenter].Position.X) &&
                 (skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z + (Constants.handElbowDiff / 2.5)))
                {
                    if ((skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y + Constants.handElbowYDiff) &&
                        (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ShoulderRight].Position.Y))
                    {
                        return GesturePartResult.Suceed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Fail;
            }
        }

    }
