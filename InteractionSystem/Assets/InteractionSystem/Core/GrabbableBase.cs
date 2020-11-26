using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

namespace Liminal.SDK.InteractableSystem
{
    public abstract class GrabbableBase : MonoBehaviour
    {
        [Header("Settings")]
        public EUpdateMode UpdateMode;

        [EnumFlags]
        public EGrabFlags GrabFlags;
        public GrabPointMarker GrabPointMarker;

        [Header("Triggers")]
        public Trigger OnNearTrigger;
        public Trigger OnTouchTrigger;

        [Header("Events")]
        public GrabUnityEvent OnGrabbed;
        public GrabUnityEvent OnGrabbing;
        public GrabUnityEvent OnUnGrabbed;
        public GrabUnityEvent OnUse;

        public Transform Point => PointProvider?.Point;
        public GrabPointProvider PointProvider { get; private set; }
        public Grabber Grabber { get; set; }

        public virtual void Grabbed(Grabber grabber)
        {
            PointProvider = grabber.GrabPointProvidersTable[GrabPointMarker];

            if (GrabFlags.HasFlag(EGrabFlags.SnapPosition))
                transform.position = Point.position;

            if (GrabFlags.HasFlag(EGrabFlags.SnapRotation))
                transform.localRotation = Point.rotation;

            OnGrabbed.Invoke(grabber);
        }

        public virtual void Grabbing(Grabber grabber)
        {
            OnGrabbing.Invoke(grabber);
        }

        public virtual void UnGrabbed(Grabber grabber)
        {
            OnUnGrabbed.Invoke(grabber);
            PointProvider = null;
        }

        public virtual void Use(Grabber grabber)
        {
            if (PointProvider != null)
                OnUse.Invoke(grabber);
        }
    }

    public enum EUpdateMode
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    [Flags]
    public enum EGrabFlags
    {
        HideControllers = 1 << 0,
        UnGrabOthers = 1 << 1,
        StayAttachedToControllerWhenUnGrabbed = 1 << 2,
        SnapPosition = 1 << 3,
        SnapRotation = 1 << 4,
    }

    [Flags]
    public enum EGrabPhysicsFlags
    {
        IsKinematic = 1 << 0,
        UseGravity = 1 << 1,
        Collision = 1 << 2,
    }

    [Serializable]
    public class GrabUnityEvent : UnityEvent<Grabber> { }
}