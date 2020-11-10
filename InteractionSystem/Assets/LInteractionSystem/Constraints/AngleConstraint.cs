using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// Lock an angle.
    /// </summary>
    [ExecuteInEditMode]
    public class AngleConstraint : MonoBehaviour
    {
        public Transform Target;
        public Transform End;
        public Transform Start;

        public float Dot;
        public float EndDot;

        private void LateUpdate()
        {
            // You need to apply future value instead.
        }
    }
}