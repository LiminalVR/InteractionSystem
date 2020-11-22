using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// Sync between grabbable and grabber positions or rotation. This is useful when you don't parent.
    /// </summary>
    public abstract class GrabbableTrackerBase : MonoBehaviour
    {
        public abstract void TrackBegin(Grabber grabber, GrabPointProvider pointProvider);
        public abstract void Track(Grabber grabber, GrabPointProvider pointProvider);
        public abstract void TrackEnd(Grabber grabber, GrabPointProvider pointProvider);
    }
}