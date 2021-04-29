using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class AvatarEmulator : MonoBehaviour
    {
        public Transform CenterEye;

        public float MoveSpeed = 2;
        public bool Grounded = true;

        private void Update()
        {
#if UNITY_EDITOR
            var direction = Vector3.zero;
            direction.x = UnityEngine.Input.GetAxis("Horizontal");
            direction.z = UnityEngine.Input.GetAxis("Vertical");

            direction = CenterEye.TransformDirection(direction);

            if (Grounded)
                direction.y = 0;

            transform.position += direction * Time.deltaTime * MoveSpeed;
#endif
        }
    }
}