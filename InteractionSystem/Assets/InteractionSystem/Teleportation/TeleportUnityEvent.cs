using System;
using UnityEngine.Events;

namespace Liminal.SDK.InteractableSystem
{
    [Serializable]
    public class TeleportUnityEvent : UnityEvent<TeleportEventHandler>
    {
    }
}