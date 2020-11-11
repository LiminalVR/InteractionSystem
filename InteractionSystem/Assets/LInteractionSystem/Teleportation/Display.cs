using System.Collections;
using Liminal.Platform.Experimental.Services;
using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// An object that has an on and off sequence but doesn't necessarily require a state machine to work.
    /// </summary>
    public class Display : MonoBehaviour
    {
        public bool Active = true;

        private void Awake()
        {
            UpdateDisplayFromState();
        }

        public void Toggle()
        {
            Active = !Active;
            UpdateDisplayFromState();
        }

        public void UpdateDisplayFromState()
        {
            if (Active)
                Show();
            else
                Hide();
        }

        public Coroutine Show()
        {
            Active = true;
            return CoroutineService.Instance.StartCoroutine(ShowRoutine());
        }

        public Coroutine Hide()
        {
            Active = false;
            return CoroutineService.Instance.StartCoroutine(HideRoutine());
        }

        public virtual IEnumerator ShowRoutine()
        {
            gameObject.SetActive(true);
            yield break;
        }

        public virtual IEnumerator HideRoutine()
        {
            gameObject.SetActive(false);
            yield break;
        }
    }
}