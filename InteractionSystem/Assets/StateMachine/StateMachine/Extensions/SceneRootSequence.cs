using UnityEngine;

namespace App.StateMachine.Examples
{
    /// <summary>
    /// Placed at the root of the scene and will finish the sequence when OnCompleted is called.
    /// </summary>
    public class SceneRootSequence : SequenceStateMachine, ISceneRoot
    {
        public bool IsDone { get; set; }

        public override void OnCompleted()
        {
            base.OnCompleted();
            IsDone = true;
        }
    }
}