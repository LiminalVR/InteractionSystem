using System;
using System.Data;
using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public class LinearDriver : MonoBehaviour
    {
        public DriverGrabbable GrabDriver;
        public Transform Content;

        private void Awake()
        {
            GrabDriver.OnMove.AddListener(OnMove);
        }

        private void OnMove(Vector3 worldDelta, Vector3 start, Vector3 grabberPosition)
        {
            var local = transform.InverseTransformPoint(grabberPosition);
            Content.transform.localPosition = local;
        }
    }

    [Serializable]
    public class Constraints
    {
        public bool X = true;
        public bool Y = true;
        public bool Z;

        public Constraints()    
        {
        }

        public Constraints(bool x, bool y, bool z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}