using Liminal.SDK.VR.Avatars;
using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    // Liminal Hand Grabber
    public class VRHandGrabber : HandGrabberBase
    {
        public override IHand PrimaryHand { get; } = new LiminalHand(EHandType.Primary);
        public override IHand SecondaryHand { get; } = new LiminalHand(EHandType.Secondary);
    }

    public class LiminalHand : IHand
    {
        public IVRAvatarHand Hand => HandType == EHandType.Primary ? VRAvatar.Active.PrimaryHand : VRAvatar.Active.SecondaryHand;
        public EHandType HandType { get; private set; }
        public bool IsActive => Hand.IsActive;
        public Transform PointerTransform => Hand.InputDevice.Pointer.Transform;
        public Transform transform => Hand.Transform;

        public LiminalHand(EHandType handType)
        {
            HandType = handType;
        }
    }

    public class OculusHandGrabber : HandGrabberBase
    {
        public override IHand PrimaryHand { get; }
        public override IHand SecondaryHand { get; }
    }
}