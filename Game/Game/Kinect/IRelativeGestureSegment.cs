﻿using Microsoft.Kinect;

namespace Game.Kinect
{
    /// <summary>
    /// Defines a single gesture segment which uses relative positioning 
    /// of body parts to detect a gesture
    /// </summary>
    public interface IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        GesturePartResult CheckGesture(Skeleton skeleton);
    }
}
