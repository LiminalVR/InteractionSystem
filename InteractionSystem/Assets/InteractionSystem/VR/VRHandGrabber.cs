using Liminal.SDK.VR.Avatars;
using System.Collections;
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
        public Hand PrimaryHand;
        public Hand SecondaryHand;
        public Vector3 Offset;
        public LayerMask Mask;
        public bool StayGrabbed;

        [Header("Style Settings")]
        public bool PositionOnCollide;
        public bool RaycastFromPointer;

        private RaycastHit _lastHit;
        private Hand _activeHand;
        private Coroutine _flexRoutine;

        public IVRAvatarHand Hand => HandType == EHandType.Primary ? VRAvatar.Active.PrimaryHand : VRAvatar.Active.SecondaryHand;
        public InteractionCommandSystem InteractionCommandSystem => VRInteractionRig.Instance.InteractionCommandSystem;
        public CommandBase GrabCommand => InteractionCommandSystem.GetSet(HandType).Grab;
        public CommandBase UseCommand => InteractionCommandSystem.GetSet(HandType).Use;
        public CommandBase DropCommand => InteractionCommandSystem.GetSet(HandType).Drop;

        private void Start()
        {
            PrimaryHand.gameObject.SetActive(HandType == EHandType.Primary);
            SecondaryHand.gameObject.SetActive(HandType == EHandType.Secondary);

            _activeHand = HandType == EHandType.Primary ? PrimaryHand : SecondaryHand;
        }

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
            {
                Grabber.Grab();

                if (_flexRoutine != null)
                    StopCoroutine(_flexRoutine);

                _flexRoutine = StartCoroutine(SetFlexProgressCoro(1f, 0.2f));

            }

            if (GrabCommand.Up)
            {
                Grabber.UnGrab();

                if (!Grabber.StayGrabbed)
                {
                    if (_flexRoutine != null)
                        StopCoroutine(_flexRoutine);

                    _flexRoutine = StartCoroutine(SetFlexProgressCoro(0f, 0.2f));
                }
            }

            if (DropCommand.Down)
            {
                Grabber.UnGrab(ignorePolicy: true);

                if (_flexRoutine != null)
                    StopCoroutine(_flexRoutine);

                _flexRoutine = StartCoroutine(SetFlexProgressCoro(0f, 0.2f));
            }

        }


        private IEnumerator SetFlexProgressCoro(float targetFlex, float time)
        {
            var elapsedTime = 0f;
            var startAmount = _activeHand.FlexAmount;

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                var flex = Mathf.Lerp(startAmount, targetFlex, elapsedTime / time);
                _activeHand.SetFlex(flex);
                yield return new WaitForEndOfFrame();
            }
            _activeHand.SetFlex(targetFlex);
            _flexRoutine = null;
        }
    }
}