using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class PositionConstraint : MonoBehaviour
    {
        public Transform Target;
        public Transform End;

        [Range(0,1)]
        public float Value;

        public Constraints AxisConstraints;

        private void LateUpdate()
        {
            var localPosition = Target.localPosition;
            if (Target.localPosition.z < 0)
                localPosition.z = 0;

            if (Target.localPosition.z >= End.localPosition.z)
                localPosition.z = End.localPosition.z;

            if (AxisConstraints.X)
                localPosition.x = 0;
            if (AxisConstraints.Y)
                localPosition.y = 0;
            if (AxisConstraints.Z)
                localPosition.z = 0;

            Target.localPosition = localPosition;

            var max = End.localPosition.z;
            Value = localPosition.z / max;
        }
    }
}