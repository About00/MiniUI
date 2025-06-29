using System.Collections.Generic;

namespace MiniUI.Listeners
{
    internal class ListenerList
    {
        #region Private Fields
        
        private readonly List<IListener> _listeners = new List<IListener>();
        
        #endregion
        
        #region Initialization
        
        public void Register(IListener instance)
        {
            _listeners.Add(instance);
        
            instance.Initialize();
        }
        
        #endregion
        
        #region ListenerList Logic
        
        public void InitializeListenersData()
        {
            foreach (var listener in _listeners)
            {
                listener.Initialize();
            }
        }
        
        #endregion
    }
}