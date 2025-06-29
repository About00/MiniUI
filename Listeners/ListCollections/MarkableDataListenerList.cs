using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace MiniUI.Listeners
{
    internal class MarkableDataListenerList
    {
        #region Private Fields
        
        private readonly List<IMarkableDataListenerItem> _listeners = new List<IMarkableDataListenerItem>();
        
        #endregion
        
        #region Initialization
        
        public void Register(IListener instance, Type[] arguments, ref SystemState state)
        {
            var type = typeof(MarkableDataListenerItem<,>).MakeGenericType(arguments);
            var itemInstance = (IMarkableDataListenerItem) Activator.CreateInstance(type);
        
            itemInstance.Register(instance, ref state);
        
            _listeners.Add(itemInstance);
        }
        
        #endregion
        
        #region MarkableDataListenerList Logic
        
        public void Update()
        {
            foreach (var listener in _listeners)
            {
                listener.UpdateData();
            }
        }
        
        #endregion
        
        #region Generic Item
        
        private class MarkableDataListenerItem<D, M> : IMarkableDataListenerItem
                where D : unmanaged, IComponentData
                where M : unmanaged, IComponentData
        {
            private D _previousData;
    
            private EntityQuery _query;
    
            private IMarkableDataListener<D, M> _listener;
    
            public void Register(IListener instance, ref SystemState state)
            {
                _listener = (IMarkableDataListener<D, M>) instance;

                _query = state.GetEntityQuery(typeof(D), typeof(M));//EntityQueryOptions.IgnoreComponentEnabledState);
            }
    
            public void UpdateData()
            {
                if (!_query.HasSingleton<M>())
                {
                    return;
                }
        
                foreach (var currentData in _query.ToComponentDataArray<D>(Allocator.Temp))
                {
                    if (_previousData.Equals(currentData))
                    {
                        return;
                    }
            
                    _previousData = currentData;
                }
            
                _listener.OnDataChanged(_previousData);
            }
        }
        
        #endregion
        
        #region Item Interface
        
        private interface IMarkableDataListenerItem
        {
            void Register(IListener instance, ref SystemState state);
            void UpdateData();
        }
        
        #endregion
    }
}