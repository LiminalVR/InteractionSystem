using System.Collections;
using UnityEngine;

namespace App.StateMachine
{
    public abstract class SequenceState : GenericState, ISequence
    {
        public bool Persistent;
        bool ISequence.Persistent => Persistent;

        public abstract IEnumerator Run();
    }
}