using System;
using System.Collections.Generic;
using Unity.Entities;

namespace MiniUI.Operators
{
    internal class EntityDataOperatorList
    {
        #region Private Fields
        
        private readonly List<IEntityDataOperatorItem> _operators = new List<IEntityDataOperatorItem>();
        
        #endregion
        
        #region Initialization
        
        public void Register(IOperator instance, Type[] arguments, ref SystemState state)
        {
            var type = typeof(EntityDataOperatorItem<>).MakeGenericType(arguments);
            var itemInstance = (IEntityDataOperatorItem)Activator.CreateInstance(type);
        
            itemInstance.Register(instance, ref state);
        
            _operators.Add(itemInstance);
        }
        
        #endregion
        
        #region Generic Item
        
        private class EntityDataOperatorItem<D> : IEntityDataOperatorItem 
                where D : struct, IComponentData
        {
            private IEntityDataOperator<D> _entityOperator;
        
            public void Register(IOperator instance, ref SystemState state)
            {
                _entityOperator = (IEntityDataOperator<D>)instance;
                _entityOperator.Entity = state.EntityManager.CreateEntity(typeof(D));

                if (ComponentType.ReadWrite<D>().IsEnableable)
                {
                    state.EntityManager.SetComponentEnabled(_entityOperator.Entity, ComponentType.ReadWrite<D>(), false);
                }
            }
        }
        
        #endregion
        
        #region Item Interface
        
        private interface IEntityDataOperatorItem
        {
            void Register(IOperator instance, ref SystemState state);
        }
        
        #endregion
    }
}