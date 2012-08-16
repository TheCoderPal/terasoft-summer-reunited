using Game.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{ 
    class BendGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
                Joint hip = Constants.oldSkeleton.Joints[JointType.HipCenter];
                Joint leftKnee = Constants.oldSkeleton.Joints[JointType.KneeLeft];
                Joint rightKnee = Constants.oldSkeleton.Joints[JointType.KneeRight];
                SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
                analyzer.SetBodySegments(skeleton.Joints[JointType.KneeLeft], skeleton.Joints[JointType.HipCenter], skeleton.Joints[JointType.KneeRight]);
                if (skeleton.Joints[JointType.HipCenter].Position.Y < hip.Position.Y - 0.2)
                {
                    if (analyzer.GetBodySegmentAngle(skeleton.Joints) > -140)
                        return GesturePartResult.Suceed;
                    return GesturePartResult.Pausing;
                }
                else return GesturePartResult.Fail;
            }
        }


}