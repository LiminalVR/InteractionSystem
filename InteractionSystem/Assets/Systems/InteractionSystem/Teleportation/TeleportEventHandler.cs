using UnityEngine;
using UnityEngine.AI;

namespace Liminal.SDK.InteractableSystem
{
    public class TeleportEventHandler
    {
        public NavMeshHit NavHit;
        public RayDisplay RayDisplay;
        public RayPhysicsCaster RayCaster;
        public Vector3 Direction;
        public bool Found;
    }
}