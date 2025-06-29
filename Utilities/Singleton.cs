namespace MiniUI.Utilities
{
    public abstract class Singleton<T> : ISingleton
        where T : Singleton<T>, new()
    {

        #region Fields

        private static T _instance;

        private SingletonStatus _status = SingletonStatus.None;

        #endregion

        #region Properties

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(T))
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                            _instance.Awake();
                        }
                    }
                }

                return _instance;
            }
        }

        public virtual bool IsStarted => _status == SingletonStatus.Started;

        #endregion

        #region Protected Methods

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnStart()
        {
        }

        #endregion

        #region Public Methods

        public virtual void Awake()
        {
            if (_status != SingletonStatus.None)
            {
                return;
            }

            _status = SingletonStatus.Awake;
            OnAwake();
            _status = SingletonStatus.Started;
            OnStart();
        }

        public virtual void Destroy()
        {
        }

        public static void CreateInstance()
        {
            DestroyInstance();
            _instance = Instance;
        }

        public static void DestroyInstance()
        {
            if (_instance == null)
            {
                return;
            }

            _instance.Destroy();
            _instance = default(T);
        }

        #endregion

    }

    public enum SingletonStatus
    {
        None,
        Awake,
        Started
    }

    public interface ISingleton
    {
        public void Awake();

        public void Destroy();
    }
}