using System;
using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    /// <summary>
    /// Provides a value for Concrete Interactables to read and react to.
    /// </summary>
    public class DriverGrabbable : GrabbableBase
    {
        private Vector3 _grabStartPosition;
        private Vector3 _grabLastPosition;

        public InfluenceEvent OnMove;

        public Action<Grabber> OnMoveBegin;

        public float UpdateStep = 0.01f;

        public override void Grabbed(Grabber grabber)
        {
            OnMoveBegin?.Invoke(grabber);

            _grabStartPosition = grabber.transform.position;
            _grabLastPosition = grabber.transform.position;
        }

        public override void Grabbing(Grabber grabber)
        {
            var delta = grabber.transform.position - _grabLastPosition;

            if (delta.sqrMagnitude >= UpdateStep)
            {
                _grabLastPosition = grabber.transform.position;
                OnMove.Invoke(delta, _grabStartPosition, grabber.transform.position);
            }
        }

        public override void UnGrabbed(Grabber grabber)
        {
        }
    }
}