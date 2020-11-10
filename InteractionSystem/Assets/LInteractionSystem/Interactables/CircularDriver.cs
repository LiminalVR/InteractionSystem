using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class CircularDriver : MonoBehaviour
    {
        [Header("The Local Right Axis (X) determines the drive direction when only Y is not constrained")]
        public DriverGrabbable GrabDriver;
        public Transform Content;

        public Constraints Constraints = new Constraints(true, false, true);

        private void Awake()
        {
            GrabDriver.OnMove.AddListener(OnMove);
            GrabDriver.OnMoveBegin += OnMoveBegin;
        }

        private void OnMoveBegin(Grabber grabber)
        {

        }

        private void OnMove(Vector3 worldDelta, Vector3 start, Vector3 grabberPosition)
        {
            var localDirection = transform.InverseTransformDirection(grabberPosition - transform.position);
            var localEulerAngles = Quaternion.LookRotation(localDirection).eulerAngles;

            if (Constraints.X) localEulerAngles.x = 0;
            if (Constraints.Y) localEulerAngles.y = 0;
            if (Constraints.Z) localEulerAngles.z = 0;

            Content.localRotation = Quaternion.Euler(localEulerAngles);
        }
    }
}