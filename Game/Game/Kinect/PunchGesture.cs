using Microsoft.Kinect;
using Game.Text;
using System.Diagnostics;

namespace Game.Kinect
{
    class PunchGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if (Constants.oldSkeleton != null)
            {
                Joint rightHand = Constants.oldSkeleton.Joints[JointType.HandRight];
                Joint rightElbow = Constants.oldSkeleton.Joints[JointType.ElbowRight];
                Joint rightShoulder = Constants.oldSkeleton.Joints[JointType.ShoulderRight];
                Joint newRightHand = skeleton.Joints[JointType.HandRight];
                Joint newRightElbow = skeleton.Joints[JointType.ElbowRight];
                SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
                analyzer.SetBodySegments(skeleton.Joints[JointType.HandRight], skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight]);
                if ((newRightHand.Position.Z > rightHand.Position.Z) &&
                    (newRightElbow.Position.Z > rightElbow.Position.Z) &&
                   (rightHand.Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X + 0.5) &&
                   (rightHand.Position.X > skeleton.Joints[JointType.HipCenter].Position.X) &&
                   (rightHand.Position.Y > rightElbow.Position.Y))
                {
                    if (analyzer.GetBodySegmentAngle(skeleton.Joints) >= -4.0 && analyzer.GetBodySegmentAngle(skeleton.Joints) <= 4.0)
                        return GesturePartResult.Suceed;
                    return GesturePartResult.Pausing;
                }
                else
                    return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }
}



