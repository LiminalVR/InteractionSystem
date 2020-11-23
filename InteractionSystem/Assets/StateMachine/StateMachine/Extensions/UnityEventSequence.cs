using System.Collections;
using App.StateMachine;
using UnityEngine.Events;

public class UnityEventSequence : SequenceState
{
    public UnityEvent OnFire;

    public override IEnumerator Run()
    {
        OnFire?.Invoke();
        yield break;
    }
}