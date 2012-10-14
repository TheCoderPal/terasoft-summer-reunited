using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Game.Text;
using Microsoft.Kinect;

namespace Game.Kinect
{
    /// <summary>
    /// Author : Microsoft
    /// </summary>
    public class Kinect
    {
        #region Gesture's variables
        private GestureController gestureController;
        private string _gesture;
        public event PropertyChangedEventHandler PropertyChanged;
        public int framesCount;
        public int FramesCount
        {
            get { return framesCount; }
            set { framesCount = value; }
        }
        public String Gesture
        {
            get { return _gesture; }

            private set
            {
                if (_gesture == value)
                    return;

                _gesture = value;

                Debug.WriteLine("Gesture = " + _gesture);

                if (this.PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Gesture"));
            }
        }
        #endregion

        private Skeleton[] skeletons;
        private KinectSensor nui;

        //Tracked Skeleton
        public Skeleton trackedSkeleton;
        private SwapHand swapHand; //Tamer
        private List<double> list, list2;//Tamer

        //Omar Abdulaal
        private int ScreenWidth, ScreenHeight;

        //Used for scaling
        private const float SkeletonMaxX = 0.60f;
        private const float SkeletonMaxY = 0.40f;


        public Kinect(int screenWidth, int screenHeight)
        {
            skeletons = new Skeleton[0];
            trackedSkeleton = null;
            list = new List<double>();//Tamer
            list2 = new List<double>();//Tamer
            swapHand = new SwapHand();//Tamer
            ScreenHeight = screenHeight; //omar
            ScreenWidth = screenWidth; //omar
            this.InitializeNui();
        }


        /// <summary>
        /// Handle insertion of Kinect sensor.
        /// </summary>
        /// AUTHOR: TAMER & KHALED
        private void InitializeNui()
        {
            var index = 0;
            while (this.nui == null && index < KinectSensor.KinectSensors.Count)
            {
                this.nui = KinectSensor.KinectSensors[index];
                this.nui.Start();
            }
            try
            {
                this.skeletons = new Skeleton[this.nui.SkeletonStream.FrameSkeletonArrayLength];
                var parameters = new TransformSmoothParameters
                {
                    Smoothing = 0.75f,
                    Correction = 0.0f,
                    Prediction = 0.0f,
                    JitterRadius = 0.05f,
                    MaxDeviationRadius = 0.04f
                };
                this.nui.SkeletonStream.Enable(parameters);
                this.nui.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            }
            catch (Exception)
            { return; }
            this.nui.SkeletonFrameReady += this.OnSkeletonFrameReady;
            gestureController = new GestureController();
            gestureController.GestureRecognized += OnGestureRecognized;
            InitializeGestures();
        }

        /// AUTHOR: TAMER & KHALED
        /// <summary>
        /// Handler for skeleton ready handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            // Get the frame.
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    frame.CopySkeletonDataTo(this.skeletons);
                    for (int i = 0; i < this.skeletons.Length; i++)
                    {
                        Skeleton skeleton = this.skeletons[i];
                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            this.trackedSkeleton = skeleton;
                        }
                    }
                    swapHand.activeRecognizer.Recognize(null, null, this.skeletons);
                    if (trackedSkeleton != null)
                    {
                        if (framesCount == 1)
                            Constants.oldSkeleton = trackedSkeleton;
                        else if (framesCount % (40) == 0)
                        {
                            Constants.oldSkeleton = trackedSkeleton;
                        }
                        if (GenerateDepth() > 120)
                        {
                            gestureController.UpdateAllGestures(trackedSkeleton);
                            framesCount++;
                            JumpHelp();
                        }
                    }
                }
            }
        }

        public Skeleton[] requestSkeleton()
        {
            return skeletons;
        }

        /// <summary>
        /// Returns right hand position scaled to screen.
        /// </summary>
        /// Author : Omar Abdulaal
        public Joint GetCursorPosition()
        {
            if (trackedSkeleton != null)
                return trackedSkeleton.Joints[JointType.HandRight].ScaleTo(ScreenWidth, ScreenHeight, SkeletonMaxX, SkeletonMaxY);
            else
                return new Joint();
        }

        /// <summary>
        /// Returns the next color frame from the Kinect Sensor
        /// </summary>
        /// <returns>ColorImageFrame containing the frame captured from the sensor.</returns>
        /// Author : Omar Abdulaal
        public ColorImageFrame GetColorFrame(int milliseconds)
        {
            if (this.nui != null)
            {
                return this.nui.ColorStream.OpenNextFrame(milliseconds);
            }
            return null;
        }

        /// <summary>
        /// Returns the color frame data as an array.
        /// </summary>
        /// <param name="milliseconds">Number of milliseconds to wait for frame</param>
        /// <returns>Byte[] Containing color data.</returns>
        /// Author : Omar Abdulaal
        public byte[] GetColorPixels(int milliseconds)
        {
            ColorImageFrame frame = this.GetColorFrame(milliseconds);
            if (frame != null)
            {
                byte[] colorPixels = new byte[this.nui.ColorStream.FramePixelDataLength];
                frame.CopyPixelDataTo(colorPixels);

                byte[] bgraColorPixels = new byte[this.nui.ColorStream.FramePixelDataLength];

                for (int i = 0; i < colorPixels.Length; i += 4)
                {
                    bgraColorPixels[i] = colorPixels[i + 2];
                    bgraColorPixels[i + 1] = colorPixels[i + 1];
                    bgraColorPixels[i + 2] = colorPixels[i];
                    bgraColorPixels[i + 3] = (byte)255;
                }

                return bgraColorPixels;
            }
            return null;
        }

        public int GetFrameWidth()
        {
            return this.nui.ColorStream.FrameWidth;
        }

        public int GetFrameHeight()
        {
            return this.nui.ColorStream.FrameHeight;
        }

        #region Tamer Jump Implementation
        private void JumpHelp()
        {
            double average = 0, average2 = 0;
            if (list.Count == 10)
                list.RemoveAt(0);

            list.Add(trackedSkeleton.Joints[JointType.AnkleRight].Position.Y);
            if (list2.Count == 10)
                list2.RemoveAt(0);

            list2.Add(trackedSkeleton.Joints[JointType.AnkleLeft].Position.Y);
            if (list.Count == 10 && list2.Count == 10)
            {
                for (int i = 5; i < 9; i++)
                {
                    average += list[i];
                    average2 += list2[i];
                }
                average = average / 4;
                average2 = average2 / 4;

                if (list[9] >= average / Constants.jumpFraction && list2[9] >= average2 / Constants.jumpFraction)
                {
                    Constants.isJumping = true;
                }


            }
        }
        #endregion


        #region khaled's methods
        /// AUTHOR: KHALED
        private void OnGestureRecognized(object sender, GestureEventArgs e)
        {
            Debug.WriteLine(e.GestureType);

            switch (e.GestureType)
            {
                case GestureType.BendGesture:
                    Gesture = "BendGesture";
                    Constants.isBending = true;
                    break;
                case GestureType.PunchGesture:
                    Gesture = "PunchGesture";
                    Constants.isPunching = true;
                    break;
                case GestureType.StepRightGesture:
                    Gesture = "StepRightGesture";
                    Constants.isSteppingRight = true;
                    break;
                case GestureType.RunningGesture:
                    Gesture = "RunningGesture";
                    Constants.isRunning = true;
                    Constants.numberOfRuns++;
                    break;
                case GestureType.DumbbellGesture:
                    Gesture = "DumbbellGesture";
                    Constants.isDumbbell = true;
                    Constants.numberOfDumbbells++;
                    break;
                default:
                    break;
            }
        }


        ///AUTHOR:Khaled
        public static List<double> Fill_Joint_Pos(Skeleton skeleton, Joint joint, string dimension)
        {
            double min = 0;
            double max = 0;
            switch (dimension)
            {
                case "x":
                    List<double> xPos = new List<double>();
                    for (int i = 0; i < 9; i++)
                    {
                        xPos.Add((double)Math.Round((skeleton.Joints[joint.JointType].Position.X)));
                        if (xPos.Count == 1)
                        {
                            min = xPos[xPos.Count - 1];
                            max = xPos[xPos.Count - 1];
                        }
                        else if (xPos.Count > 1)
                        {
                            if (xPos[xPos.Count - 1] == xPos[xPos.Count - 2])
                                xPos.RemoveAt(xPos.Count - 2);
                            if (xPos[xPos.Count - 1] > max)
                                max = xPos[xPos.Count - 1];
                            if (xPos[xPos.Count - 1] < min)
                                min = xPos[xPos.Count - 1];
                        }
                        if (xPos.Count == 10)
                            xPos.RemoveAt(0);
                        xPos.Add((double)Math.Round((skeleton.Joints[joint.JointType].Position.X)));

                    }
                    Constants.minmax = ToString(min, max);
                    return xPos;
                case "y":
                    List<double> yPos = new List<double>();
                    for (int i = 0; i < 9; i++)
                    {
                        yPos.Add((double)Math.Round((skeleton.Joints[joint.JointType].Position.Y)));
                        if (yPos.Count == 1)
                        {
                            min = yPos[yPos.Count - 1];
                            max = yPos[yPos.Count - 1];
                        }
                        else if (yPos.Count > 1)
                        {
                            if (yPos[yPos.Count - 1] == yPos[yPos.Count - 2])
                                yPos.RemoveAt(yPos.Count - 2);
                            if (yPos[yPos.Count - 1] > max)
                                max = yPos[yPos.Count - 1];
                            if (yPos[yPos.Count - 1] < min)
                                min = yPos[yPos.Count - 1];
                        }

                        if (yPos.Count == 10)
                            yPos.RemoveAt(0);
                        yPos.Add((double)Math.Round((skeleton.Joints[joint.JointType].Position.Y)));

                    }
                    Constants.minmax = ToString(min, max);
                    return yPos;

                case "z":
                    List<double> zPos = new List<double>();
                    for (int i = 0; i < 9; i++)
                    {
                        zPos.Add((double)Math.Round((skeleton.Joints[joint.JointType].Position.Z)));
                        if (zPos.Count == 1)
                        {
                            min = zPos[zPos.Count - 1];
                            max = zPos[zPos.Count - 1];
                        }
                        else if (zPos.Count > 1)
                        {
                            if (zPos[zPos.Count - 1] == zPos[zPos.Count - 2])
                                zPos.RemoveAt(zPos.Count - 2);
                            if (zPos[zPos.Count - 1] > max)
                                max = zPos[zPos.Count - 1];
                            if (zPos[zPos.Count - 1] < min)
                                min = zPos[zPos.Count - 1];
                        }

                        if (zPos.Count == 10)
                            zPos.RemoveAt(0);
                        zPos.Add((double)Math.Round((skeleton.Joints[joint.JointType].Position.Z)));

                    }
                    Constants.minmax = ToString(min, max);
                    return zPos;
                default: return new List<double>();

            }
        }

        ///AUTHOR:Khaled
        public static string ToString(double min, double max)
        {
            return min + "," + max;
        }

        ///AUTHOR:Khaled
        public void InitializeGestures()
        {
            IRelativeGestureSegment[] StepRightSegments = new IRelativeGestureSegment[1];
            StepRightGesture1 stepRightGesture1 = new StepRightGesture1();
            StepRightSegments[0] = stepRightGesture1;
            this.gestureController.AddGesture(GestureType.StepRightGesture, StepRightSegments);
            IRelativeGestureSegment[] BendSegments = new IRelativeGestureSegment[1];
            BendGesture1 bendGesture1 = new BendGesture1();
            BendSegments[0] = bendGesture1;
            this.gestureController.AddGesture(GestureType.BendGesture, BendSegments);
            IRelativeGestureSegment[] PunchSegments = new IRelativeGestureSegment[1];
            PunchGesture1 punchGesture1 = new PunchGesture1();
            PunchSegments[0] = punchGesture1;
            this.gestureController.AddGesture(GestureType.PunchGesture, PunchSegments);
            IRelativeGestureSegment[] DumbbellSegments = new IRelativeGestureSegment[2];
            DumbbellGesture1 dumbbellGesture1 = new DumbbellGesture1();
            DumbbellGesture2 dumbbellGesture2 = new DumbbellGesture2();
            DumbbellSegments[0] = dumbbellGesture1;
            DumbbellSegments[1] = dumbbellGesture2;
            this.gestureController.AddGesture(GestureType.DumbbellGesture, DumbbellSegments);
            IRelativeGestureSegment[] RunningSegments = new IRelativeGestureSegment[2];
            RunningGesture1 runningGesture1 = new RunningGesture1();
            RunningGesture2 runningGesture2 = new RunningGesture2();
            RunningSegments[0] = runningGesture1;
            RunningSegments[1] = runningGesture2;
            this.gestureController.AddGesture(GestureType.RunningGesture, RunningSegments);
        }

        /// <summary>
        /// Takes the user's index in the array and gets his distance from the kinect device.
        /// </summary>
        /// <remarks>
        /// <para>AUTHOR: Khaled Salah </para>
        /// </remarks>
        /// <param name="index">
        /// The user's index in the array.
        /// </param>
        /// <returns>
        /// Int number which is the calculated depth.
        /// </returns>
        public int GenerateDepth()
        {
            try
            {
                return (int)(100 * this.trackedSkeleton.Joints[JointType.HipCenter].Position.Z);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion
    }
}
