using System;
using MiniUI.Utilities;
using Unity.Entities;

namespace MiniUI.Listeners
{
    internal class Listeners : Singleton<Listeners>
    {
        #region Public Properties
        
        public ListenerList ListenerList { get; } = new ListenerList();
        public DataListenerList DataListenerList { get; } = new DataListenerList();
        public EventListenerList EventListenerList { get; } = new EventListenerList();
        public MarkableDataListenerList MarkableDataListenerList { get; } = new MarkableDataListenerList();
        
        #endregion
        
        #region Listeners Logic
        
        public void Add(Type type, ref SystemState state)
        {
            var instance = (IListener) Activator.CreateInstance(type);
        
            ListenerList.Register(instance);
        
            if (ClassTypeUtility.IsAssignableFromGenericInterface(type, typeof(IDataListener<>)))
            {
                foreach (var arguments in ClassTypeUtility.GetGenericArgumentTypes(type, typeof(IDataListener<>)))
                {
                    DataListenerList.Register(instance, arguments, ref state);
                }
            }
            if (ClassTypeUtility.IsAssignableFromGenericInterface(type, typeof(IEventListener<>)))
            {
                foreach (var arguments in ClassTypeUtility.GetGenericArgumentTypes(type, typeof(IEventListener<>)))
                {
                    EventListenerList.Register(instance, arguments, ref state);
                }
            }
            if (ClassTypeUtility.IsAssignableFromGenericInterface(type, typeof(IMarkableDataListener<,>)))
            {
                foreach (var arguments in ClassTypeUtility.GetGenericArgumentTypes(type, typeof(IMarkableDataListener<,>)))
                {
                    MarkableDataListenerList.Register(instance, arguments,  ref state);
                }
            }
        }
        
        #endregion
    }
}