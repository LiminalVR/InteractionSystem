using App.StateMachine.Examples;
using UnityEngine;

public abstract class SceneRoot<T> : MonoBehaviour, ISceneRoot where T : SceneState
{
    public bool IsDone { get; set; }

    public T Data { get; set; }

    public void Done()
    {
        IsDone = true;
    }

    public virtual void Begin()
    {
    }
}