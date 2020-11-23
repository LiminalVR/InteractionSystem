using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoroutineService = Tools.CoroutineService;

namespace App.StateMachine
{
    /// <summary>
    /// A generic finite state machine to go from one state to another.
    /// </summary>
    public class StateMachine : StateBase
    {
        public Action<IState, IState> OnStateTransitionIn, OnStateEntered;
        public Action<IState, IState> OnStateTransitionOut, OnStateExited;

        public IState Current { get; set; }

        public bool Transitioning { get; protected set; }
        public List<IState> States { get; protected set; }

        protected virtual void Awake()
        {
            Initialize();
            SetupStates();
            OnInitialized();
        }

        public virtual void OnInitialized()
        {
        }

        public void SetupStates()
        {
            States = new List<IState>();

            foreach (Transform t in transform)
            {
                var state = t.GetComponent<IState>();
                if (state != null && t.gameObject.activeSelf)
                    States.Add(state);
            }

            foreach (var state in States)
            {
                if (ReferenceEquals(this, state))
                    continue;

                state.Setup();
                state.Sleep(null);
            }
        }

        public virtual void Initialize() { }

        public Coroutine GoToState(StateBase state)
        {
            return CoroutineService.Instance.StartCoroutine(GoTo(state));
        }

        public void GoToState(IState state)
        {
            StartCoroutine(GoTo(state));
        }

        public IEnumerator GoTo(IState nextState, bool transitionOutPreviousState = true)
        {
            if (Transitioning)
                yield break;

            if (ReferenceEquals(nextState, Current))
                yield break;

            Transitioning = true;

            var previous = Current;

            // Transition out of the current state
            if (previous != null && transitionOutPreviousState)
            {
                previous.OnTransitionOut?.Invoke(nextState);
                OnStateTransitionOut?.Invoke(previous, nextState);

                yield return previous.TransitionOut(nextState);

                previous.OnExited?.Invoke(nextState);
                previous.Exited(nextState);
                OnStateExited?.Invoke(previous, nextState);
                previous.Sleep(nextState);
            }

            Current = nextState;

            // Transition in to the next state
            nextState.Wake();
            nextState.OnTransitionIn?.Invoke(previous);
            OnStateTransitionIn?.Invoke(nextState, previous);

            yield return nextState.TransitionIn(previous);

            nextState.OnEntered?.Invoke(previous);
            nextState.Entered(previous);
            OnStateEntered?.Invoke(nextState, previous);
            StateEntered(nextState);
            Transitioning = false;
        }

        public override IEnumerator TransitionIn(IState from) { yield break; }
        public override IEnumerator TransitionOut(IState to) { yield break; }
        public override void Setup() { }
        public override void Entered(IState from) { }
        public override void Exited(IState entering) { }

        public virtual void StateEntered(IState state)
        {
        }
    }
}

