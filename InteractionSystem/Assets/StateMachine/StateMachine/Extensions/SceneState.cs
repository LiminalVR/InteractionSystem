using System;
using System.Collections;
using App.StateMachine.Examples;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace App.StateMachine.Examples
{
    public class SceneState<T> : SceneState where T : SceneState
    {
        public override void Bind(ISceneRoot sceneRoot)
        {
            if (sceneRoot is SceneRoot<T> s)
                s.Data = this as T;

            base.Bind(sceneRoot);
        }
    }

    /// <summary>
    /// Load a scene and run the state on it until it finishes.
    /// This requires a SequenceStateMachine or manually calling Run();
    /// </summary>
    public class SceneState : SequenceState
    {
        public SceneReference SceneReference;

        public ISceneRoot SceneRoot { get; protected set; }

        public override IEnumerator TransitionIn(IState from)
        {
            yield return SceneManager.LoadSceneAsync(SceneReference, LoadSceneMode.Additive);
        }

        public override IEnumerator Run()
        {
            var scene = SceneManager.GetSceneByPath(SceneReference.ScenePath);
            SceneManager.SetActiveScene(scene);
            
            SceneRoot = FindSceneSequence(scene.GetRootGameObjects());
            Bind(SceneRoot);

            yield return new WaitUntil(() => SceneRoot.IsDone);
        }

        public virtual void Bind(ISceneRoot sceneRoot)
        {
            sceneRoot.Begin();
        }

        public override IEnumerator TransitionOut(IState to)
        {
            if (!SceneManager.GetSceneByPath(SceneReference.ScenePath).isLoaded)
                yield break;

            yield return SceneManager.UnloadSceneAsync(SceneReference);
            yield return base.TransitionOut(to);
        }

        public ISceneRoot FindSceneSequence(GameObject[] rootObjects)
        {
            foreach (var rootObject in rootObjects)
            {
                var sceneSequence = rootObject.GetComponentInChildren<ISceneRoot>();
                if (sceneSequence != null)
                    return sceneSequence;
            }

            return null;
        }
    }
}
