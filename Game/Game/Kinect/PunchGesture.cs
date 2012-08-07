using Microsoft.Kinect;
using Game.Text;

namespace Game.Kinect
{
        class PunchGesture1 : IRelativeGestureSegment
        {
            public GesturePartResult CheckGesture(Skeleton skeleton)
            {
                Constants.posZ = skeleton.Joints[JointType.HandRight].Position.Z;
                if (skeleton.Joints[JointType.HandRight].Position.X <= (skeleton.Joints[JointType.ElbowRight].Position.X + Constants.handElbowDiff) &&
                        (skeleton.Joints[JointType.HandRight].Position.X >= (skeleton.Joints[JointType.ElbowRight].Position.X - Constants.handElbowDiff)) &&
                        (skeleton.Joints[JointType.HandRight].Position.Z > (skeleton.Joints[JointType.ElbowRight].Position.Z + Constants.handElbowDiff)))
                   {
                       if
                       (skeleton.Joints[JointType.HandRight].Position.Y <= (skeleton.Joints[JointType.ElbowRight].Position.Y + Constants.handElbowDiff) &&
                       (skeleton.Joints[JointType.HandRight].Position.Y >= (skeleton.Joints[JointType.ElbowRight].Position.Y - Constants.handElbowDiff)))
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
                if (skeleton.Joints[JointType.HandRight].Position.Y <= (skeleton.Joints[JointType.ElbowRight].Position.Y + Constants.handElbowDiff) &&
                    (skeleton.Joints[JointType.HandRight].Position.Y >= (skeleton.Joints[JointType.ElbowRight].Position.Y - Constants.handElbowDiff)))
                {
                    if (skeleton.Joints[JointType.HandRight].Position.X <= (skeleton.Joints[JointType.ElbowRight].Position.X + Constants.handElbowDiff) &&
                        (skeleton.Joints[JointType.HandRight].Position.X >= (skeleton.Joints[JointType.ElbowRight].Position.X - Constants.handElbowDiff)) && 
                        (skeleton.Joints[JointType.HandRight].Position.Z > (skeleton.Joints[JointType.ElbowRight].Position.Z)) &&
                        (skeleton.Joints[JointType.HandRight].Position.Z > (Constants.posZ + Constants.handElbowDiff)))
                    {
                        return GesturePartResult.Suceed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Fail;
            }
        }
    }

