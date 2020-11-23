using System.Collections;
using UnityEngine;

namespace App.StateMachine
{
    public class WaitForEventSequenceState : SequenceState
    {
        public bool IsDone { get; set; }

        public override IEnumerator Run()
        {
            yield return new WaitUntil(() => IsDone);
            
            if (!Persistent)
                IsDone = false;
        }

        [ContextMenu("Done")]
        public void Done()
        {
            IsDone = true;
        }
    }
}