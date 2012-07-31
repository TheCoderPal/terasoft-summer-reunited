using System;
using Microsoft.Samples.Kinect.SwipeGestureRecognizer;
namespace Game.Kinect
{
    /// <summary>
    /// Tamer Nabil
    /// </summary>
    class SwapHand
    {
        public readonly Recognizer activeRecognizer;
        private bool flag;
        public SwapHand()
        {
            flag = false;
            this.activeRecognizer = this.CreateRecognizer();
        }
        public Recognizer CreateRecognizer()
        {
            var recognizer = new Recognizer();
            recognizer.SwipeRightDetected += (s, e) =>
            {
                flag = true;
            };
            recognizer.SwipeLeftDetected += (s, e) =>
            {
                flag = true;
            };
            return recognizer;
        }
        public bool requestFlag()
        {
            return flag;
        }
        public void resetFlag()
        {
            flag = false;
        }
    }
}