namespace App.StateMachine.Examples
{
    public class SceneRoot : SceneRoot<SceneState>
    {
        public bool DoneOnAwake = false;

        private void Awake()
        {
            if(DoneOnAwake)
                Done();
        }
    }
}