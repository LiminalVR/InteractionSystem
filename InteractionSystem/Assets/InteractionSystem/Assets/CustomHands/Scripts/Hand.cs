using Liminal.SDK.InteractableSystem;
using Liminal.SDK.VR.Avatars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR.Input;

public class Hand : MonoBehaviour
{
    public EHandType HandType;
    public Animator Animator;

    public float FlexAmount => Animator.GetFloat(_FlexParam);

    private const string _FlexParam = "Flex";
    private IVRInputDevice _device;

    public void SetFlex(float flexProgress)
    {
        Animator.SetFloat(_FlexParam, flexProgress);
    }
}
