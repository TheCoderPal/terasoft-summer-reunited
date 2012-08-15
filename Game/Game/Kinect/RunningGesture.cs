using Game.Text;
using Microsoft.Kinect;
using System.Diagnostics;

namespace Game.Kinect
{
    class RunningGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            Joint leftAnkle = Constants.oldSkeleton.Joints[JointType.AnkleLeft];
            Joint leftKnee = Constants.oldSkeleton.Joints[JointType.KneeLeft];
            Joint newLeftAnkle = skeleton.Joints[JointType.AnkleLeft];
            Joint newLeftKnee = skeleton.Joints[JointType.KneeLeft];
            SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
            analyzer.SetBodySegments(skeleton.Joints[JointType.AnkleLeft], skeleton.Joints[JointType.KneeLeft], skeleton.Joints[JointType.HipLeft]);
            if (newLeftAnkle.Position.Y > leftAnkle.Position.Y)
            {
                return GesturePartResult.Suceed;
            }
            return GesturePartResult.Fail;
        }
    }
    class RunningGesture2 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            Joint rightAnkle = Constants.oldSkeleton.Joints[JointType.AnkleRight];
            Joint rightKnee = Constants.oldSkeleton.Joints[JointType.KneeRight];
            Joint newRightAnkle = skeleton.Joints[JointType.AnkleRight];
            Joint newRightKnee = skeleton.Joints[JointType.KneeRight];
            SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
            analyzer.SetBodySegments(skeleton.Joints[JointType.AnkleRight], skeleton.Joints[JointType.KneeRight], skeleton.Joints[JointType.HipRight]);
              if (newRightAnkle.Position.Y > rightAnkle.Position.Y)
                       return GesturePartResult.Suceed;
              return GesturePartResult.Fail;
        }
    }
}
    

