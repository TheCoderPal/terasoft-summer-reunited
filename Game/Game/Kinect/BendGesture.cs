using Microsoft.Kinect;
using Game.Text;
using System.Diagnostics;

namespace Game.Kinect
{ 
    class BendGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
            analyzer.SetBodySegments(skeleton.Joints[JointType.KneeLeft], skeleton.Joints[JointType.HipCenter], skeleton.Joints[JointType.KneeRight]);
            if (skeleton.Joints[JointType.HipCenter].Position.Y < (Constants.hipPosY+0.5))
            {
                if(analyzer.GetBodySegmentAngle(skeleton.Joints)> -140)
                return GesturePartResult.Suceed;
                return GesturePartResult.Pausing;
            }
            else return GesturePartResult.Fail;
        }
    }


}