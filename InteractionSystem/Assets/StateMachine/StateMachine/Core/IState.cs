using System;
using System.Collections;

namespace App.StateMachine
{
    public interface IState
    {
        Action<IState> OnTransitionIn { get; set; }
        Action<IState> OnTransitionOut { get; set; }

        Action<IState> OnEntered { get; set; }
        Action<IState> OnExited { get; set; }

        void Setup();
        void Entered(IState state);
        void Exited(IState state);

        void Wake();
        void Sleep(IState state);

        IEnumerator TransitionIn(IState state);
        IEnumerator TransitionOut(IState state);

        bool IsSubState { get; }
    }
}