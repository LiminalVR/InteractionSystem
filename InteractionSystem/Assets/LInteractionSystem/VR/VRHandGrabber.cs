using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;
using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// The bridge between Liminal VRAvatar and the Interaction System.
    /// </summary>
    public class VRHandGrabber : MonoBehaviour
    {
        public Grabber Grabber;
        public VRAvatarHand Hand;

        public Vector3 Offset;

        [Header("Automatically place grabber at collision")]
        public bool PositionOnCollide;
        public bool RaycastFromPointer;
        public LayerMask Mask;

        private RaycastHit _lastHit;

        private void Update()
        {
            if (!Hand.IsActive)
            {
                if(Grabber.Grabbing)
                    Grabber.UnGrab();

                return;
            }

            if (Hand.InputDevice?.Pointer == null)
                return;

            var origin = RaycastFromPointer ? Hand.InputDevice.Pointer.Transform : Hand.transform;
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

            if (VRDevice.Device.GetButtonDown(VRButton.One))
            {
                Grabber.Grab();
            }

            if (VRDevice.Device.GetButtonUp(VRButton.One))
            {
                Grabber.UnGrab();
            }

            // Right Mouse click to UnGrab atm.
            if (UnityEngine.Input.GetMouseButtonDown(2))
            {
                Grabber.UnGrab(ignorePolicy: true);
            }
        }
    }
}