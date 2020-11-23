using System.Collections;
using App.StateMachine;
using UnityEngine;

public class StateExample : GenericState
{
    public GameObject Target;

    public override void Setup()
    {
        Target.SetActive(false);
    }

    // Turn Target on while you are transitioning to this state
    public override IEnumerator TransitionIn(IState from)
    {
        Target.SetActive(true);
        yield break;
    }

    // Turn Target off while you are transitioning out of this state.
    public override IEnumerator TransitionOut(IState to)
    {
        Target.SetActive(false);
        yield break;
    }
}