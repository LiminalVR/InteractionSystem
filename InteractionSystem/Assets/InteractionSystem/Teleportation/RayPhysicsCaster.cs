using System;
using UnityEngine;
using UnityEngine.Events;

namespace Liminal.SDK.InteractableSystem
{
    public sealed class RayPhysicsCaster : MonoBehaviour
    {
        public RayBase Ray;

        public int MaxIntersections = 1;

        public RayCastUnityEvent OnRaycastHitFound;
        public LayerMask Mask;

        public RaycastHit Hit { get; set; }

        private int _hitIndex;

        private void Update()
        {
            if (Ray.Positions.Length == 0)
                return;

            var previousPosition = Ray.Positions[0];
            var intersected = 0;

            for (var i = 1; i < Ray.Positions.Length; i++)
            {
                var position = Ray.Positions[i];
                if (!Physics.Linecast(previousPosition, position, out var hit, Mask))
                    continue;

                _hitIndex = i;
                Hit = hit;
                OnRaycastHitFound.Invoke(hit);

                intersected++;

                if (intersected >= MaxIntersections)
                    break;
            }
        }

        public Vector3[] GetPositions()
        {
            var positions = new Vector3[_hitIndex];
            for (var i = 0; i < _hitIndex; i++)
            {
                positions[i] = Ray.Positions[i];
            }

            return positions;
        }
    }

    [Serializable]
    public class RayCastUnityEvent : UnityEvent<RaycastHit>
    {
    }
}