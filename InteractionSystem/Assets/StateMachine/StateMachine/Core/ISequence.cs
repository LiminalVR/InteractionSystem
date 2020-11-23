using System.Collections;

namespace App.StateMachine
{
    public interface ISequence : IState
    {
        bool Persistent { get; }
        IEnumerator Run();
    }
}