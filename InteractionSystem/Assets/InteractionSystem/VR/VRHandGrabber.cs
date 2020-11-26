using Liminal.SDK.VR.Avatars;
using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// The bridge between Liminal VRAvatar and the Interaction System.
    /// </summary>
    public class VRHandGrabber : MonoBehaviour
    {
        public Grabber Grabber;
        public RayBase Ray;

        [Header("Settings")]
        public EHandType HandType;
        public Vector3 Offset;
        public LayerMask Mask;

        [Header("Style Settings")]
        public bool PositionOnCollide;
        public bool RaycastFromPointer;

        private RaycastHit _lastHit;

        public IVRAvatarHand Hand => HandType == EHandType.Primary ? VRAvatar.Active.PrimaryHand : VRAvatar.Active.SecondaryHand;
        public InteractionCommandSystem InteractionCommandSystem => VRInteractionRig.Instance.InteractionCommandSystem;
        public CommandBase GrabCommand => InteractionCommandSystem.GetSet(HandType).Grab;
        public CommandBase UseCommand => InteractionCommandSystem.GetSet(HandType).Use;
        public CommandBase DropCommand => InteractionCommandSystem.GetSet(HandType).Drop;

        private void LateUpdate()
        {
            if (!Hand.IsActive)
            {
                if(Grabber.Grabbing)
                    Grabber.UnGrab();

                return;
            }

            if (Hand.InputDevice?.Pointer == null)
                return;

            var origin = RaycastFromPointer ? Hand.InputDevice.Pointer.Transform : Hand.Transform;
            if (origin == null)
                return;

            transform.rotation = origin.rotation;

            if (PositionOnCollide)
            {
                transform.position = origin.transform.position + origin.forward * _lastHit.distance;
            }
            else
            {
                transform.position = origin.position;
                var offset = origin.TransformVector(Offset);
                transform.position += offset;
            }

            if (!Grabber.Grabbing)
            {
                if (Physics.Raycast(origin.transform.position, origin.transform.forward, out var hit, 1000, Mask))
                    _lastHit = hit;
            }

            //stops users picking up an object and using it on the same frame.
            if (UseCommand.Down)
                Grabber.Use();

            if (GrabCommand.Down)
                Grabber.Grab();

            if (GrabCommand.Up)
                Grabber.UnGrab();

            if (DropCommand.Down)
                Grabber.UnGrab(ignorePolicy: true);

        }
    }
}