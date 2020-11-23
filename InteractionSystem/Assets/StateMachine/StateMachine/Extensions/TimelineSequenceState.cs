using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace App.StateMachine
{
    public class TimelineSequenceState : SequenceState
    {
        public PlayableDirector Director;

        public override IEnumerator Run()
        {
            Director.Play();
            yield return new WaitUntil(() => Director.state == PlayState.Playing);
            yield return new WaitUntil(() => Director.state != PlayState.Playing);
        }
    }
}