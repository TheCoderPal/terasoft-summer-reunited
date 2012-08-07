using Microsoft.Kinect;
using Game.Text;
using System.Diagnostics;

namespace Game.Kinect
{
    class PunchGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
            analyzer.SetBodySegments(skeleton.Joints[JointType.HandRight], skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight]);
            if (skeleton.Joints[JointType.HandRight].Position.Z > Constants.posZ)
            {
                if (analyzer.GetBodySegmentAngle(skeleton.Joints) >= -2.0 && analyzer.GetBodySegmentAngle(skeleton.Joints) <= 2.0)
                    return GesturePartResult.Suceed;
                return GesturePartResult.Pausing;
            }
            else
                return GesturePartResult.Fail;
        }
    }
}



