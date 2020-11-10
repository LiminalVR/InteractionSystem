using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class TeleportSystem : MonoBehaviour
    {
        public RayPhysicsCaster RayPhysicsCaster;
        public Transform Target;

        private void Start()
        {
            RayPhysicsCaster.OnRaycastHitFound.AddListener(OnRayCastHitFound);
        }

        private void OnRayCastHitFound(RaycastHit hit)
        {
            if (VRDevice.Device.GetButtonDown(VRButton.One))
                Target.position = hit.point;
        }
    }
}