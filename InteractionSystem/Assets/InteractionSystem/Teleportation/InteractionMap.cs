using System.Collections.Generic;
using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    [CreateAssetMenu]
    public class InteractionMap : ScriptableObject
    {
        public List<EButtonType> GrabButtons = new List<EButtonType>();
        public List<EButtonType> DropButtons = new List<EButtonType>();
        public List<EButtonType> TeleportButtons = new List<EButtonType>();
    }
}