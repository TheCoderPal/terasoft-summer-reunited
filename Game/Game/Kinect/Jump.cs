using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
namespace Game.Kinect
{
    class Jump
    {
        public bool flag = false;
        public void skelation(Skeleton s)
        {
            //Angle used 8 ,eqn needed for tilt angle 
            //3ady head position : 0.4 ,if i jumped got 0.6 to 0.7,if(n>0.5) i jump,diff between head to hip is 0.6
            double diff = s.Joints[JointType.Head].Position.Y - s.Joints[JointType.HipCenter].Position.Y;
            double n = (diff * 0.88);
            if (s.Joints[JointType.Head].Position.Y > n)
                flag = true;
        }
    }
}
