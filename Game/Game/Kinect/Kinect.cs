using Microsoft.Kinect;
namespace Game.Kinect
{
    /// <summary>
    /// Author : Microsoft
    /// </summary>
    class kinect
    {
        private Skeleton[] skeletons;
        private KinectSensor nui;
        public bool swapFlag; //Tamer
        SwapHand swapHand; //Tamer
        /// <summary>
        /// The ID if the skeleton to be tracked.
        /// </summary>
        private int nearestId = -1;
        public kinect()
        {
            skeletons = new Skeleton[0];
            this.InitializeNui();
            swapFlag = false; //tamer
            swapHand = new SwapHand(); //tamer
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
            this.nui.SkeletonStream.Enable();
            this.nui.SkeletonFrameReady += this.OnSkeletonFrameReady;
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
                    // Resize the skeletons array if a new size (normally only on first call).
                    if (this.skeletons.Length != frame.SkeletonArrayLength)
                    {
                        this.skeletons = new Skeleton[frame.SkeletonArrayLength];
                    }
                    // Get the skeletons.
                    frame.CopySkeletonDataTo(this.skeletons);
                    // Assume no nearest skeleton and that the nearest skeleton is a long way away.
                    var newNearestId = -1;
                    var nearestDistance2 = double.MaxValue;
                    // Look through the skeletons.
                    foreach (var skeleton in this.skeletons)
                    {
                        // Only consider tracked skeletons.
                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            // Find the distance squared.
                            var distance2 = (skeleton.Position.X * skeleton.Position.X) +
                                (skeleton.Position.Y * skeleton.Position.Y) +
                                (skeleton.Position.Z * skeleton.Position.Z);

                            // Is the new distance squared closer than the nearest so far?
                            if (distance2 < nearestDistance2)
                            {
                                // Use the new values.
                                newNearestId = skeleton.TrackingId;
                                nearestDistance2 = distance2;
                            }
                        }
                    }
                    if (this.nearestId != newNearestId)
                    {
                        this.nearestId = newNearestId;
                    }
                    //Tamer Nabil
                    // Pass skeletons to recognizer.
                    swapHand.activeRecognizer.Recognize(sender, frame, this.skeletons);
                    swapFlag = swapHand.requestFlag();
                }
            }
        }
        public Skeleton[] requestSkeleton()
        {
            return skeletons;
        }
    }
}
