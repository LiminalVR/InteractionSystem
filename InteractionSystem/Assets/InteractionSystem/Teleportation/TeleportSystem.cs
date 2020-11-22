using UnityEngine;
using UnityEngine.AI;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// A teleport system for each ray.
    /// </summary>
    public class TeleportSystem : MonoBehaviour
    {
        public EHandType HandType;

        [Header("Settings")]

        public string TeleportLayer = "Teleportable";
        public bool DeactivateWhenGrabbing = true;
        public float DetectionThreshold = 0.5f;

        [Header("Events")]
        public TeleportUnityEvent OnValueChanged;
        public TeleportUnityEvent OnInvalidPosition;
        public TeleportUnityEvent OnValidPosition;

        public TeleportEventHandler TeleportData { get; set; }

        public InteractionCommandSystem InteractionCommandSystem => VRInteractionRig.Instance.InteractionCommandSystem;
        public CommandBase TeleportCommand => InteractionCommandSystem.GetSet(HandType).Teleport;

        public RayBase Ray => VRInteractionRig.Instance.GetGrabber(HandType).Ray;
        public RayDisplay RayDisplay => Ray.Display;
        public RayPhysicsCaster RayPhysicsCaster => Ray.Caster;
        public Transform Target => VRInteractionRig.Instance.Root;

        private void Start()
        {
            TeleportData = new TeleportEventHandler
            {
                RayCaster = RayPhysicsCaster,
                RayDisplay = RayDisplay,
            };
        }

        private void Update()
        {
            if (DeactivateWhenGrabbing && Ray.Grabber.Grabbing)
                RayDisplay.Active = false;
            else
            {
                if (TeleportCommand.Pressed)
                {
                    if (!RayDisplay.Active)
                        OnRayActivated(RayDisplay);
                }
                else
                {
                    if (RayDisplay.Active)
                        OnRayDeactivated(RayDisplay);
                }
            }

            var found = GetNavHit(out var navHit, RayPhysicsCaster);
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

        private void OnRayActivated(RayDisplay display)
        {
            display.Show();
        }

        private void OnRayDeactivated(RayDisplay display)
        {
            display.Hide();

            if (GetNavHit(out var navHit, RayPhysicsCaster))
                Teleport(navHit.position, Target.forward);
        }

        public void Teleport(Vector3 position, Vector3 direction)
        {
            Target.position = position;
        }

        public bool GetNavHit(out NavMeshHit navHit, RayPhysicsCaster caster)
        {
            var point = caster.Hit.point;

            var mask = 1 << NavMesh.GetAreaFromName(TeleportLayer);
            if (NavMesh.SamplePosition(point, out navHit, 1.0f, mask))
            {
                var distance = Vector3.Distance(point, navHit.position);
                if (distance <= DetectionThreshold)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

// Teleport using Forward
// Teleport using Custom