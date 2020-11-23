using System;
using System.Collections;
using App.StateMachine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Runs a sequence state machine.
/// </summary>
public class RunSequenceStateMachine : SequenceState
{
    public SequenceStateMachine SequenceStateMachine;

    public override IEnumerator Run()
    {
        yield return SequenceStateMachine.Run();
    }
}
