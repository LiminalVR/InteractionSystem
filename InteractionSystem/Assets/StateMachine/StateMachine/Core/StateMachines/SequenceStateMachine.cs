using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace App.StateMachine
{
    /// <summary>
    /// A state machine with a set of states to run. It has a start and an end.
    /// </summary>
    public class SequenceStateMachine : StateMachine, ISequence
    {
        public bool Loop = false;
        public bool Persistent => false;
        public bool AutoStart;

        private List<ISequence> _states = new List<ISequence>();
        private List<ISequence> _initialStates = new List<ISequence>();
        private ISequence _current;

        public UnityEvent OnStateMachineCompleted;

        public override void Initialize()
        {
            foreach (Transform t in transform)
            {
                if (!t.gameObject.activeInHierarchy || t == transform)
                    continue;

                var state = t.GetComponent<SequenceState>();

                if (state != null)
                {
                    _states.Add(state);
                    _initialStates.Add(state);
                }
            }
        }

        public override void OnInitialized()
        {
            base.OnInitialized();

            if(AutoStart)
                Begin();
        }

        public void Begin()
        {
            Tools.CoroutineService.Instance.StartCoroutine(Run());
        }

        public IEnumerator Run()
        {
            for (var i = 0; i < _states.Count; i++)
            {
                var state = _states[i];
                var previous = i == 0 ? null : _states[i-1];
                var transitionOutPrevious = previous != null && !previous.Persistent;
                _current = state;

                yield return GoTo(state, transitionOutPrevious);
                yield return state.Run();
            }

            OnCompleted();

            if (Loop)
                yield return Restart();
        }

        public void RestartStateMachine()
        {
            StartCoroutine(Restart());
        }

        public IEnumerator Restart()
        {
            foreach (var state in _states)
                yield return state.TransitionOut(null);

            _states = new List<ISequence>(_initialStates);

            SetupStates();
            Begin();
        }

        public virtual void OnCompleted()
        {
            OnStateMachineCompleted.Invoke();
        }

        public void Add(ISequence state)
        {
            var currentIndex = _states.FindIndex(x => x == _current);
            _states.Insert(currentIndex + 1, state);
        }

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