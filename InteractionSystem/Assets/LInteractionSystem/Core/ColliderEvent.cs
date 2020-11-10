using System;
using UnityEngine;
using UnityEngine.Events;

namespace Liminal.SDK.InteractableSystem
{
    [Serializable]
    public class ColliderEvent : UnityEvent<Collider>
    {
    }

    [Serializable]
    public class Vector3Event : UnityEvent<Vector3>
    {
    }

    [Serializable]
    public class InfluenceEvent : UnityEvent<Vector3, Vector3, Vector3>
    {
    }
}