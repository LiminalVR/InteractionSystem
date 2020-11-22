using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// Syncs Grabbable and a grabbable point. This is useful for tracking without parenting.
    /// </summary>
    public sealed class Grabbable : GrabbableBase
    {
        [Header("Tracks the grabbable to the grabber's point.")]
        public GrabbableTrackerBase Sync;

        public override void Grabbed(Grabber grabber)
        {
            base.Grabbed(grabber);

            Sync?.TrackBegin(grabber, PointProvider);
        }

        public override void Grabbing(Grabber grabber)
        {
            base.Grabbing(grabber);

            if (Point == null || Sync == null)
                return;

            Sync.Track(grabber, PointProvider);
        }

        public override void UnGrabbed(Grabber grabber)
        {
            base.UnGrabbed(grabber);

            Sync?.TrackEnd(grabber, PointProvider);
        }
    }
}