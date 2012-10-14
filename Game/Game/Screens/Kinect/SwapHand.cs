using Game.Text;
using Microsoft.Samples.Kinect.SwipeGestureRecognizer;
namespace Game.Kinect
{
    /// <summary>
    /// Tamer Nabil
    /// </summary>
    class SwapHand
    {
        public readonly Recognizer activeRecognizer;
        public SwapHand()
        {
            this.activeRecognizer = this.CreateRecognizer();
        }
        public Recognizer CreateRecognizer()
        {
            var recognizer = new Recognizer();
            recognizer.SwipeRightDetected += (s, e) =>
            {
                Constants.isSwappingHand = true;
            };
            recognizer.SwipeLeftDetected += (s, e) =>
            {
                Constants.isSwappingHand = true;
            };
            return recognizer;
        }
    }
}