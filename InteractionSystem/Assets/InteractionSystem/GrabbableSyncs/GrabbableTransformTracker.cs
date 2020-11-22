using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class GrabbableTransformTracker : GrabbableTrackerBase
    {
        public bool ParentToPoint;

        private Transform _parent;

        public override void TrackBegin(Grabber grabber, GrabPointProvider pointProvider)
        {
            _parent = transform.parent;

            if(ParentToPoint)
                transform.SetParent(pointProvider.Point);
        }

        public override void Track(Grabber grabber, GrabPointProvider pointProvider)
        {
            if (ParentToPoint)
                return;

            var point = pointProvider.Point;
            transform.position = point.position;
            transform.rotation = point.rotation;
        }

        public override void TrackEnd(Grabber grabber, GrabPointProvider pointProvider)
        {
            transform.SetParent(_parent);
        }
    }
}