using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class VRInteractionRig : MonoBehaviour
    {
        public static VRInteractionRig Instance { get; private set; }

        public Transform Root;

        public VRHandGrabber PrimaryGrabber;
        public VRHandGrabber SecondaryGrabber;

        public InteractionCommandSystem InteractionCommandSystem;

        private void Awake()
        {
            Instance = this;
        }

        public VRHandGrabber GetGrabber(EHandType type) =>
            type == EHandType.Primary ? PrimaryGrabber : SecondaryGrabber;
    }
}