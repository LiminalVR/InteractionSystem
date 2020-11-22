using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class TeleportVisualSystem : MonoBehaviour
    {
        public TeleportSystem TeleportSystem;
        public Transform Indicator;

        private void Awake()
        {
            TeleportSystem.OnValueChanged.AddListener(OnTeleportValueChanged);
        }

        private void OnTeleportValueChanged(TeleportEventHandler data)
        {
            Indicator.gameObject.SetActive(data.RayDisplay.Active);

            var renderer = Indicator.GetComponent<Renderer>();
            renderer.material.color = data.Found ? Color.blue : Color.red;

            transform.up = data.Found ? data.NavHit.normal : data.RayCaster.Hit.normal;
            Indicator.position = data.Found ? data.NavHit.position : data.RayCaster.Hit.point;
        }
    }
}