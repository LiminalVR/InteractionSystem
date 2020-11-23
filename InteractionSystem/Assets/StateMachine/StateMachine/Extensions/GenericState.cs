using System.Collections;

namespace App.StateMachine
{
    /// <summary>
    /// A state that does not and simply fire events. This is good if you don't want to have to implement everything from state base.
    /// </summary>
    public class GenericState : StateBase
    {
        public override IEnumerator TransitionIn(IState from)
        {
            yield break;
        }

        public override IEnumerator TransitionOut(IState to)
        {
            yield break;
        }

        public override void Setup()
        {
        }

        public override void Entered(IState from)
        {
        }

        public override void Exited(IState from)
        {
        }
    }
}
