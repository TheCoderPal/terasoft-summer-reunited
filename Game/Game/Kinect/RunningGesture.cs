using Game.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    class RunningGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
            analyzer.SetBodySegments(skeleton.Joints[JointType.AnkleLeft], skeleton.Joints[JointType.KneeLeft], skeleton.Joints[JointType.HipLeft]);
            if (skeleton.Joints[JointType.KneeLeft].Position.Y > skeleton.Joints[JointType.KneeRight].Position.Y + 0.007)
            {
                //if (analyzer.GetBodySegmentAngle(skeleton.Joints) < 150 && analyzer.GetBodySegmentAngle(skeleton.Joints) > 30)
                return GesturePartResult.Suceed;
                //else return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
    }
    class RunningGesture2 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
            analyzer.SetBodySegments(skeleton.Joints[JointType.AnkleRight], skeleton.Joints[JointType.KneeRight], skeleton.Joints[JointType.HipRight]);
            if (skeleton.Joints[JointType.KneeRight].Position.Y > skeleton.Joints[JointType.KneeLeft].Position.Y + 0.007)
            {
                //if (analyzer.GetBodySegmentAngle(skeleton.Joints) < 150 && analyzer.GetBodySegmentAngle(skeleton.Joints) > 30)
                return GesturePartResult.Suceed;
                //    else
                //        return GesturePartResult.Pausing;
                //
            }
            return GesturePartResult.Fail;
        }
    }

}


