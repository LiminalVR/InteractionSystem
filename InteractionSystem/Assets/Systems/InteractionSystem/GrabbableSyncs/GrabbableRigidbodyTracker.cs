using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class GrabbableRigidbodyTracker : GrabbableTrackerBase
    {
        public Rigidbody Rigidbody;

        public EGrabPhysicsFlags GrabFlags;
        public EGrabPhysicsFlags UnGrabFlags;

        public float Speed = 5;
        public float ThrowSensitivity = 1;

        public AnimationCurve Curve = AnimationCurve.EaseInOut(0,0,1,1);

        public bool SyncRotation;

        public override void TrackBegin(Grabber grabber, GrabPointProvider pointProvider)
        {
            Rigidbody.isKinematic = GrabFlags.HasFlag(EGrabPhysicsFlags.IsKinematic);
            Rigidbody.detectCollisions = GrabFlags.HasFlag(EGrabPhysicsFlags.Collision);
            Rigidbody.useGravity = GrabFlags.HasFlag(EGrabPhysicsFlags.UseGravity);

            Rigidbody.velocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
        }

        public override void Track(Grabber grabber, GrabPointProvider pointProvider)
        {
            var point = pointProvider.Point;
            var direction = point.position - transform.position;
            direction.Normalize();

            var distance = Vector3.Distance(transform.position, point.position);
            var outputSpeed = Mathf.Lerp(0, Speed, Curve.Evaluate(distance));

            Rigidbody.position += direction * outputSpeed * Time.fixedDeltaTime;

            if(SyncRotation)
                Rigidbody.rotation = point.rotation;
        }

        public override void TrackEnd(Grabber grabber, GrabPointProvider pointProvider)
        {
            Rigidbody.isKinematic = UnGrabFlags.HasFlag(EGrabPhysicsFlags.IsKinematic);
            Rigidbody.detectCollisions = UnGrabFlags.HasFlag(EGrabPhysicsFlags.Collision);
            Rigidbody.useGravity = UnGrabFlags.HasFlag(EGrabPhysicsFlags.UseGravity);

            if (ThrowSensitivity > 0)
            {
                var velocity = grabber.VelocityEstimator.Velocity;
                Rigidbody.velocity = velocity * ThrowSensitivity;
            }
        }
    }
}