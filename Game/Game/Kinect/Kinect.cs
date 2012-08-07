using Microsoft.Kinect;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.ComponentModel;
using Game.Text;
namespace Game.Kinect
{
    /// <summary>
    /// Author : Microsoft
    /// </summary>
    public class Kinect
    {
        private GestureController gestureController;
        private string _gesture;
        public event PropertyChangedEventHandler PropertyChanged;
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
        private Skeleton[] skeletons;
        private KinectSensor nui;
        //Tracked Skeleton
        public Skeleton trackedSkeleton;
      //  public bool swapFlag,jumpFlag; //Tamer
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
            //swapFlag =false;//Tamer
          //  jumpFlag = false;//Tamer
            swapHand = new SwapHand();//Tamer
             ScreenHeight = screenHeight; //omar
            ScreenWidth = screenWidth; //omar
            this.InitializeNui();
        }
        /// <summary>
        /// Handle insertion of Kinect sensor.
        /// </summary>
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
                this.nui.SkeletonStream.Enable();
            }
            catch (Exception)
            { return; }
            this.nui.SkeletonFrameReady += this.OnSkeletonFrameReady;
            gestureController = new GestureController();
            gestureController.GestureRecognized += OnGestureRecognized;
            InitializeGestures();
        }

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
            break;
        case GestureType.DumbbellGesture:
            Gesture = "DumbbellGesture";
            Constants.isDumbbell = true;
            break;
        default:
            break;
    }
        //Debug.Write(Constants.isDumbbell.ToString());
        //Debug.Write(Constants.isBending.ToString());
        //Debug.Write(Constants.isPunching.ToString());
        //Debug.Write(Constants.isRunning.ToString());
        //Debug.Write(Constants.isSteppingRight.ToString());
        //Constants.ResetFlags();
        //Debug.Write(Constants.isDumbbell.ToString());
        //Debug.Write(Constants.isBending.ToString());
        //Debug.Write(Constants.isPunching.ToString());
        //Debug.Write(Constants.isRunning.ToString());
        //Debug.Write(Constants.isSteppingRight.ToString());
    }
        /// <summary>
        /// Handler for skeleton ready handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            // Get the frame.
            using (var frame = e.OpenSkeletonFrame())
            {
                // Ensure we have a frame.
                if (frame != null)
                {
                    // trackedSkeleton = null;
                    // Resize the skeletons array if a new size (normally only on first call).
                    if (this.skeletons.Length != frame.SkeletonArrayLength)
                    {
                        this.skeletons = new Skeleton[frame.SkeletonArrayLength];
                    }
                    // Get the skeletons.
                    frame.CopySkeletonDataTo(this.skeletons);
                    foreach (var skeleton in this.skeletons)
                    {
                        // Only consider tracked skeletons.
                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            if (trackedSkeleton == null || skeleton.Position.Z < trackedSkeleton.Position.Z)
                                trackedSkeleton = skeleton;
                        }
                    }
                    //Tamer
                      swapHand.activeRecognizer.Recognize(null, null, this.skeletons);
                      //swapFlag = swapHand.requestFlag();
                    if (trackedSkeleton != null)
                    {
                        JumpHelp();
                        gestureController.UpdateAllGestures(trackedSkeleton);
                    }
                }
            }
        }

        private void JumpHelp()
        {
            double average = 0, average2 = 0;
            if (list.Count == 10)
            {
                list.RemoveAt(0);
                list.Add(trackedSkeleton.Joints[JointType.AnkleRight].Position.Y);
            }
            else
                list.Add(trackedSkeleton.Joints[JointType.AnkleRight].Position.Y);

            if (list2.Count == 10)
            {
                list2.RemoveAt(0);
                list2.Add(trackedSkeleton.Joints[JointType.AnkleLeft].Position.Y);
            }
            else
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

                if (list[9] >= average / 1.2 && list2[9] >= average2 / 1.2)
                {
                   // jumpFlag = true;
                    Constants.isJumping = true;
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
        public void InitializeGestures()
        {
             IRelativeGestureSegment[] StepRightSegments = new IRelativeGestureSegment[1];
             StepRightGesture1 stepRightGesture1 = new StepRightGesture1();
             StepRightSegments[0]=stepRightGesture1;
             this.gestureController.AddGesture(GestureType.StepRightGesture, StepRightSegments);
             IRelativeGestureSegment[] BendSegments = new IRelativeGestureSegment[1];
             BendGesture1 bendGesture1 = new BendGesture1();
             BendSegments[0] = bendGesture1;
             this.gestureController.AddGesture(GestureType.BendGesture, BendSegments);
             IRelativeGestureSegment[] PunchSegments = new IRelativeGestureSegment[2];
             PunchGesture1 punchGesture1 = new PunchGesture1();
             PunchGesture2 punchGesture2 = new PunchGesture2();
             PunchSegments[0] = punchGesture1;
             PunchSegments[1] = punchGesture2;
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
    }
}
