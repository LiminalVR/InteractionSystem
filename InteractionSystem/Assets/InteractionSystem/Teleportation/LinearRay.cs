using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class LinearRay : RayBase
    {
        public float MaxDistance = 10;

        private void Start()
        {
            Positions = new Vector3[2];
        }

        private void Update()
        { 
            SetPoints();
        }

        private void SetPoints()
        {
            var a = transform.position;
            var b = transform.position + transform.forward * MaxDistance;

            Positions[0] = a;
            Positions[1] = b;
        }
    }
}