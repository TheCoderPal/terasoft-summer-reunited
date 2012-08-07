using Microsoft.Kinect;
using Game.Text;
using System.Diagnostics;

namespace Game.Kinect
{
    class PunchGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            if ((skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ElbowRight].Position.X+0.2) &&
                (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ElbowRight].Position.X-0.2)&&
                (skeleton.Joints[JointType.HandRight].Position.Y > (skeleton.Joints[JointType.ElbowRight].Position.Y-0.2)) &&
                (skeleton.Joints[JointType.HandRight].Position.Y < (skeleton.Joints[JointType.ElbowRight].Position.Y+0.2))&&
                (skeleton.Joints[JointType.HandRight].Position.Y < (skeleton.Joints[JointType.ShoulderRight].Position.Y+0.2)) &&
                (skeleton.Joints[JointType.HandRight].Position.Y > (skeleton.Joints[JointType.ShoulderRight].Position.Y-0.2)) &&
                (skeleton.Joints[JointType.HandRight].Position.X > (skeleton.Joints[JointType.ShoulderRight].Position.X-0.2)) &&
                (skeleton.Joints[JointType.HandRight].Position.X < (skeleton.Joints[JointType.ShoulderRight].Position.X+0.2)))
            {
                return GesturePartResult.Suceed;
            }
            return GesturePartResult.Fail;
        }
    }
}

