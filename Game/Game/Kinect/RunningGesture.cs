using Game.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    class RunningGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
            SkeletonAnalyzer analyzer2 = new SkeletonAnalyzer();
            analyzer.SetBodySegments(skeleton.Joints[JointType.AnkleLeft], skeleton.Joints[JointType.KneeLeft], skeleton.Joints[JointType.HipCenter]);
            if (skeleton.Joints[JointType.KneeLeft].Position.Y > skeleton.Joints[JointType.KneeRight].Position.Y+0.002)
            {
                    return GesturePartResult.Suceed;
            }
            else return GesturePartResult.Fail;
        }
    }
    class RunningGesture2 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
            SkeletonAnalyzer analyzer2 = new SkeletonAnalyzer();
            analyzer.SetBodySegments(skeleton.Joints[JointType.AnkleRight], skeleton.Joints[JointType.KneeRight], skeleton.Joints[JointType.HipCenter]);
            if ((skeleton.Joints[JointType.KneeRight].Position.Y > skeleton.Joints[JointType.KneeLeft].Position.Y+0.002))
            {
                return GesturePartResult.Suceed;
            }
            else return GesturePartResult.Fail;
        }
    }

}


