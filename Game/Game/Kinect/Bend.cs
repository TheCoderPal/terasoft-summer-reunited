using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
namespace Game.Kinect
{
    class Bend
    {
        private List<float> headPosY;//bending
        private List<float> headPosX;//entering gyn
        private List<float> kneePosYRight;
        private List<float> kneePosYLeft;
        private List<float> hipPosX;
        private List<float> hipPosY;
        private List<float> hipPosZ;
        private List<float> handPosY;
        private List<float> handPosZ;
        private List<float> elbowPosZ;
        bool flag1;
        bool flag2;
        Skeleton user;
        Kinect kinect;

        public void Initialize()
        {
           // user = kinect.Skeletons[0];
        }

        public void fill_pos()
        {
            kneePosYLeft.Add((float)Math.Round((user.Joints[JointType.KneeLeft].Position.Y), 1));
            kneePosYRight.Add((float)Math.Round((user.Joints[JointType.KneeRight].Position.Y), 1));
            headPosY.Add((float)Math.Round((user.Joints[JointType.Head].Position.Y), 1));
            headPosX.Add((float)Math.Round((user.Joints[JointType.Head].Position.X), 1));
            hipPosZ.Add((float)Math.Round((user.Joints[JointType.HipCenter].Position.Z), 1));
            hipPosY.Add((float)Math.Round((user.Joints[JointType.HipCenter].Position.Y), 1));
            hipPosX.Add((float)Math.Round((user.Joints[JointType.HipCenter].Position.X), 1));
            handPosY.Add((float)Math.Round((user.Joints[JointType.HandRight].Position.Y), 1));
            handPosZ.Add((float)Math.Round((user.Joints[JointType.HandRight].Position.Z), 1));
            elbowPosZ.Add((float)Math.Round((user.Joints[JointType.ElbowRight].Position.Z), 1));
        }
        public bool hasBent()
        {
            for(int i=1; i<=headPosY.Count(); i++)
            {
                if(headPosY[headPosY.Count()-i]>headPosY[headPosY.Count()-(i-1)])
                   flag1=true;
                else if (headPosY[headPosY.Count()-i]==headPosY[headPosY.Count()-(i-1)])
                  headPosY.RemoveAt(headPosY.Count() - (i-1));
            }
            for (int i = 1; i <= hipPosY.Count(); i++)
            {
                if (hipPosY[hipPosY.Count() - i] == hipPosY[hipPosY.Count() - (i - 1)])
                    flag2 = true;
                else if (hipPosY[hipPosY.Count() - i] == hipPosY[hipPosY.Count() - (i - 1)])
                    hipPosY.RemoveAt(hipPosY.Count() - (i - 1));
            }
            return flag1 && flag2;
        }
    }
}
