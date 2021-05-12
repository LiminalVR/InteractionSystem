using UnityEngine;

namespace Liminal.SDK.InteractableSystem
{
    public interface IHand
    {
        EHandType HandType { get; }
        bool IsActive { get; }
        Transform PointerTransform { get; }
        Transform transform { get; }
    }
}