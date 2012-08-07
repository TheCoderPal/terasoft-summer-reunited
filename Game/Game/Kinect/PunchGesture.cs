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
            if ((skeleton.Joints[JointType.HandRight].Position.Z > Constants.posZ) && (skeleton.Joints[JointType.ElbowRight].Position.Z > Constants.posZ)&&
                (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X+0.5) &&
                (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.HipCenter].Position.X)
                &&(skeleton.Joints[JointType.HandRight].Position.Y > Constants.elbowPosY))

            {
                if (analyzer.GetBodySegmentAngle(skeleton.Joints) >= -4.0 && analyzer.GetBodySegmentAngle(skeleton.Joints) <= 4.0)
                    return GesturePartResult.Suceed;
                return GesturePartResult.Pausing;
            }
            else
                return GesturePartResult.Fail;
        }
    }
}



