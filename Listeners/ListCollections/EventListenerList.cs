using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;

namespace MiniUI.Listeners
{
    internal class EventListenerList
    {
        #region Private Fields
        
        private readonly List<IEventListenerItem> _listeners = new List<IEventListenerItem>();
        
        #endregion
        
        #region Initialization
        
        public void Register(IListener instance, Type[] arguments, ref SystemState state)
        {
            var type = typeof(EventListenerItem<>).MakeGenericType(arguments);
            var itemInstance = (IEventListenerItem) Activator.CreateInstance(type);
            
            itemInstance.Register(instance, ref state);
            
            _listeners.Add(itemInstance);
        }
        
        #endregion
        
        #region EventListenerList Logic
        
        public void Update(ref SystemState state)
        {
            foreach (var listener in _listeners)
            {
                listener.UpdateData(ref state);
            }
        }
        #endregion
        
        #region Generic Item
        
        private class EventListenerItem<E> : IEventListenerItem
                where E : unmanaged, IComponentData
        {
            private E _previousData;
        
            private EntityQuery _query;
        
            private IEventListener<E> _listener;
        
            public void Register(IListener instance, ref SystemState state)
            {
                _listener = (IEventListener<E>) instance;
                
                _query = state.GetEntityQuery(typeof(E));
            }
        
            public void UpdateData(ref SystemState state)
            {
                if (_query.CalculateEntityCount() != 1)
                {
                    return;
                }
                
                if (ComponentType.ReadWrite<E>().IsEnableable)
                {
                    foreach (var currentData in _query.ToComponentDataArray<E>(Allocator.Temp))
                    {
                        var entity = _query.ToEntityArray(Allocator.Temp).First();
                        
                        if (state.EntityManager.IsComponentEnabled(entity, typeof(E)))
                        {
                            _listener.OnHasEvent(currentData);
                        }
                    }

                }
                else
                {
                    foreach (var currentData in _query.ToComponentDataArray<E>(Allocator.Temp))
                    {
                        _listener.OnHasEvent(currentData);
                    }
                }
            }
        }
        
        #endregion
        
        #region Item Interface
        
        private interface IEventListenerItem
        {
            void Register(IListener instance, ref SystemState state);
            void UpdateData(ref SystemState state);
        }
        
        #endregion
    }
}