using Game.Text;
using Microsoft.Kinect;
using System.Collections.Generic;
using System.Text;
using System;
using System.Globalization;

namespace Game.Kinect
{
    class PunchGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            //List<float> handPos = Kinect.Fill_Joint_Pos(skeleton, skeleton.Joints[JointType.HandRight], "x");
            //float min=0f;
            //float max=0f;
            //SkeletonAnalyzer analyzer = new SkeletonAnalyzer();
            SkeletonAnalyzer analyzer2 = new SkeletonAnalyzer();
            //if (!Constants.minmax.Equals(""))
            //{
            //    string[] minmax = Constants.minmax.Split(',');
            //     min = float.Parse(minmax[0], CultureInfo.InvariantCulture);
            //     max = float.Parse(minmax[1], CultureInfo.InvariantCulture);
            //}
            analyzer2.SetBodySegments(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight], skeleton.Joints[JointType.HipCenter]);
            if ((skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X + 0.5) &&
                (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.HipCenter].Position.X))
            {
                    if (analyzer2.GetBodySegmentAngle(skeleton.Joints) > 10 && analyzer2.GetBodySegmentAngle(skeleton.Joints) <60)
                    return GesturePartResult.Suceed;
                return GesturePartResult.Pausing;
            }
            else
                return GesturePartResult.Fail;
        }
    }
}



