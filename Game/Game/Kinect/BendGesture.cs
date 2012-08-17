using Game.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    /// AUTHOR: Khaled
    class BendGesture1 : IRelativeGestureSegment
    {
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            //double average=0;
            //List<double> list=Kinect.Fill_Joint_Pos(skeleton,skeleton.Joints[JointType.HipCenter],"y");
            //for (int i = 5; i <= 8; i++)
            //    average += list[i];
            //average /= 4;
            //if (list[9] < average/4)
            //{
            //    return GesturePartResult.Suceed;
            //}
            //else return GesturePartResult.Fail;
            if (skeleton.Joints[JointType.HipCenter].Position.Y < (Constants.posY - 0.3))
            {
                return GesturePartResult.Suceed;
            }
            else return GesturePartResult.Fail;
        }
    }


}