using App.StateMachine;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Provides a set of Unity Events to state changes.
/// </summary>
public class StateUnityEvent : MonoBehaviour
{
    public StateBase State;

    public UnityEvent OnTransitionIn, OnEntered;
    public UnityEvent OnTransitionOut, OnExited;

    private void OnValidate()
    {
        State = GetComponent<StateBase>();
    }

    private void Awake()
    {
        State.OnTransitionIn += (_) => OnTransitionIn.Invoke();
        State.OnTransitionOut += (_) => OnTransitionOut.Invoke();
        State.OnEntered += (_) => OnEntered.Invoke();
        State.OnExited += (_) => OnExited.Invoke();
    }
}
