using System.Collections;
using System.Collections.Generic;
using App.StateMachine;

public class StackStateMachine : StateMachine
{
    public Stack<IState> StateStack = new Stack<IState>();

    public override void StateEntered(IState state)
    {
        StateStack.Push(state);
        base.StateEntered(state);
    }

    public void Back()
    {
        if (Transitioning)
            return;

        if (StateStack.Count == 1)
            return;

        StateStack.Pop();
        var previous = StateStack.Pop();
        GoToState(previous);
    }
}
