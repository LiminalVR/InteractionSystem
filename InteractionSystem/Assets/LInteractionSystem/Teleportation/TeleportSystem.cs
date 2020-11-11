using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// The teleport system is reactive to the ray.
    /// </summary>
    public class TeleportSystem : MonoBehaviour
    {
        public RayBase Ray;
        public RayDisplay RayDisplay;
        public RayPhysicsCaster RayPhysicsCaster;
        public Transform Target;

        public string TeleportLayer = "Teleportable";
        public bool DeactivateWhenGrabbing = true;

        public TeleportUnityEvent OnValueChanged;
        public TeleportUnityEvent OnInvalidPosition;
        public TeleportUnityEvent OnValidPosition;

        public TeleportEventHandler TeleportData { get; set; }

        private void Start()
        {
            RayPhysicsCaster.OnRaycastHitFound.AddListener(OnRayCastHitFound);
            TeleportData = new TeleportEventHandler
            {
                RayCaster = RayPhysicsCaster,
                RayDisplay = RayDisplay,
            };
        }

        private void Update()
        {
            if (DeactivateWhenGrabbing && Ray.Grabber.GrabbedInteractables.Count > 0)
                RayDisplay.Active = false;
            else
            {
                if (UnityEngine.Input.GetMouseButton(1))
                {
                    if (!RayDisplay.Active)
                        OnRayActivated();
                }
                else
                {
                    if (RayDisplay.Active)
                        OnRayDeactivated();
                }
            }

            var found = GetNavHit(out var navHit);
            TeleportData.NavHit = navHit;

            if (TeleportData.Found && !found)
            {
                TeleportData.Found = false;
                OnInvalidPosition?.Invoke(TeleportData);
            }

            if (!TeleportData.Found && found)
            {
                TeleportData.Found = true;
                OnValidPosition?.Invoke(TeleportData);
            }

            TeleportData.Found = found;

            OnValueChanged?.Invoke(TeleportData);
        }

        private void OnRayActivated()
        {
            RayDisplay.Show();
        }

        private void OnRayDeactivated()
        {
            RayDisplay.Hide();

            if (GetNavHit(out var navHit))
                Target.position = navHit.position;
        }

        public bool GetNavHit(out NavMeshHit navHit)
        {
            var point = RayPhysicsCaster.Hit.point;

            var mask = 1 << NavMesh.GetAreaFromName(TeleportLayer);
            if (NavMesh.SamplePosition(point, out navHit, 1.0f, mask))
            {
                var distance = Vector3.Distance(point, navHit.position);
                if (distance <= 0.5f)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnRayCastHitFound(RaycastHit hit)
        {
            //if (VRDevice.Device.GetButtonDown(VRButton.One))
            //    Target.position = hit.point;
        }
    }

    public class TeleportEventHandler
    {
        public NavMeshHit NavHit;
        public RayDisplay RayDisplay;
        public RayPhysicsCaster RayCaster;
        public bool Found;
    }

    [Serializable]
    public class TeleportUnityEvent : UnityEvent<TeleportEventHandler>
    {
    }
}