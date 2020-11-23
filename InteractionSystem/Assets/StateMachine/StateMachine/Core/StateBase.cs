using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace App.StateMachine
{
    public abstract class StateBase : MonoBehaviour, IState
    {
        public Action<IState> OnTransitionIn { get; set; }
        public Action<IState> OnTransitionOut { get; set; }
        public Action<IState> OnEntered { get; set; }
        public Action<IState> OnExited { get; set; }

        public bool IsSubState;
        bool IState.IsSubState => IsSubState;

        /// <summary>
        /// The state is in the process of entering, this is a great place for animation like fading in.
        /// </summary>
        public abstract IEnumerator TransitionIn(IState from);

        /// <summary>
        /// The state is exiting, this is a great place for animation, like fading out.
        /// </summary>
        public abstract IEnumerator TransitionOut(IState to);

        /// <summary>
        /// Initialize variables or states here.
        /// </summary>
        public abstract void Setup();

        /// <summary>
        /// Is called after transition in.
        /// </summary>
        /// <param name="from"></param>
        public abstract void Entered(IState state);

        /// <summary>
        /// Is called after transition out.
        /// </summary>
        /// <param name="from"></param>
        public abstract void Exited(IState state);

        public void Wake()
        {
            gameObject.SetActive(true);
        }

        public virtual void Sleep(IState state)
        {
            if (state != null && state.IsSubState)
                return;

            gameObject.SetActive(false);
        }
    }
}