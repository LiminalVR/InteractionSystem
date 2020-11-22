using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class VelocityEstimator : MonoBehaviour
    {
        public Vector3 Velocity;

        private Vector3 _previous;

        private void Update()
        {
            Velocity = (transform.position - _previous) / Time.deltaTime;
            _previous = transform.position;
        }
    }
}