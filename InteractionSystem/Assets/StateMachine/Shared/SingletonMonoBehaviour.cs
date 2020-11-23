using UnityEngine;

namespace Tools
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        /// <summary>
        /// Gets the singleton instance <see cref="T"/>.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null) 
                    return _instance;

                _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                Debug.LogWarning(string.Format("Singleton instance not initialized for {0}", typeof(T).ToString()));

                return _instance;
            }
        }

        public static bool IsInitialised
        {
            get { return _instance != null; }
        }

        public static CustomYieldInstruction WaitUntilInitialised()
        {
            return new WaitUntil(() => _instance != null);
        }

        [Tooltip("Determines if the behaviour is peristent throughought the application lifetime. If unchecked, the singleton will be destroyed when the scene is unloaded.")]
        [SerializeField]
        private bool m_DontDestroyOnLoad = true;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = (T) (MonoBehaviour) this;
            OnAwake();
        }

        // https://answers.unity.com/questions/1211672/dontdestroyonload-works-in-editor-but-not-on-andro.html
        private void Start()
        {
            if (m_DontDestroyOnLoad)
            {
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
                OnDestroyed();
            }
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnDestroyed()
        {
        }
    }
}