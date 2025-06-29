using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace MiniUI.Listeners
{
    internal class DataListenerList
    {
        #region Private Fields
        
        private readonly List<IDataListenerItem> _listeners = new List<IDataListenerItem>();
        
        #endregion
        
        #region Initialization
        
        public void Register(IListener instance, Type[] arguments, ref SystemState state)
        {
            var type = typeof(DataListenerItem<>).MakeGenericType(arguments);
            var itemInstance = (IDataListenerItem) Activator.CreateInstance(type);
        
            itemInstance.Register(instance, ref state);
        
            _listeners.Add(itemInstance);
        }
        
        #endregion
        
        #region DataListenerList Logic
        
        public void Update()
        {
            foreach (var listener in _listeners)
            {
                listener.UpdateData();
            }
        }
        
        #endregion
        
        #region Generic Item
        
        private class DataListenerItem<D> : IDataListenerItem
                where D : unmanaged, IComponentData
        {
            private D _previousData;
    
            private EntityQuery _query;
    
            private IDataListener<D> _listener;
        
            public void Register(IListener instance, ref SystemState state)
            {
                _listener = (IDataListener<D>) instance;
            
                _query = state.GetEntityQuery(typeof(D));
            }
    
            public void UpdateData()
            {
                if (!_query.HasSingleton<D>())
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
        
        private interface IDataListenerItem
        {
            void Register(IListener instance, ref SystemState state);
            void UpdateData();
        }
        
        #endregion
    }
}