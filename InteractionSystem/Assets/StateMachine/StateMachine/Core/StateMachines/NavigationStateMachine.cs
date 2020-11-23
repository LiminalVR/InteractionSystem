using UnityEngine;

namespace App.StateMachine
{
    /// <summary>
    /// A state machine that can go back and forth.
    /// </summary>
    public class NavigationStateMachine : StateMachine
    {
        public StateBase InitialState;

        [ContextMenu("Back")]
        public void Back()
        {
            GoToState(GetPrevious());
        }

        [ContextMenu("Next")]
        public void Next()
        {
            GoToState(GetNext());
        }

        public override void Entered(IState from)
        {
            GoToState(InitialState);
            base.Entered(from);
        }

        public IState GetNext()
        {
            var currentIndex = States.IndexOf(Current);
            var nextIndex = currentIndex == States.Count - 1 ? 0 : currentIndex + 1;
            return States[nextIndex];
        }

        public IState GetPrevious()
        {
            var currentIndex = States.IndexOf(Current);
            var index = currentIndex == 0 ? States.Count - 1 : currentIndex - 1;
            return States[index];
        }
    }
}