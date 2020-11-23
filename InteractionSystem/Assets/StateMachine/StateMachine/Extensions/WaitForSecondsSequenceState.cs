using System.Collections;
using UnityEngine;

namespace App.StateMachine
{
    public class WaitForSecondsSequenceState : SequenceState
    {
        public float WaitTime = 1;

        public override IEnumerator Run()
        {
            yield return new WaitForSeconds(WaitTime);
        }
    }
}