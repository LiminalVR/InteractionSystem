using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class Trigger : MonoBehaviour
    {
        public LayerMask Ignore;
        public bool Active = true;

        public ColliderEvent OnEnter;
        public ColliderEvent OnExit;

        private void OnTriggerEnter(Collider col)
        {
            if (ShouldIgnore(col))
                return;

            OnEnter.Invoke(col);
        }

        private void OnTriggerExit(Collider col)
        {
            if (ShouldIgnore(col))
                return;

            OnExit.Invoke(col);
        }

        public bool ShouldIgnore(Collider col)
        {
            return !Active || LayerUtils.LayerExists(col.gameObject.layer, Ignore);
        }
    }
}

public static class LayerUtils
{
    public static bool LayerExists(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}