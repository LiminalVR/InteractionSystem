using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// This class handles highlighting etc.
    /// </summary>
    public class InteractableAnimator : MonoBehaviour
    {
        public GrabbableBase Interactable;

        private void Awake()
        {
            Interactable.OnNearTrigger.OnEnter.AddListener(OnNearTriggerEnter);
            Interactable.OnNearTrigger.OnExit.AddListener(OnNearTriggerExit);
            Interactable.OnTouchTrigger.OnEnter.AddListener(OnTouchTriggerEnter);
            Interactable.OnTouchTrigger.OnExit.AddListener(OnTouchTriggerExit);
        }

        protected virtual void OnNearTriggerEnter(Collider col)
        {
            Debug.Log("Near Entered");
        }

        protected virtual void OnNearTriggerExit(Collider col)
        {
            Debug.Log("Near Exited");
        }

        protected virtual void OnTouchTriggerEnter(Collider col)
        {
            Debug.Log("Touch Entered");
        }

        protected virtual void OnTouchTriggerExit(Collider col)
        {
            Debug.Log("Touch Exited");
        }
    }
}