using System;
using System.Collections.Generic;
using Unity.Entities;

namespace MiniUI.Operators
{
    internal class EntityOperatorList
    {
        #region Private Fields
        
        private readonly List<IEntityOperatorItem> _operators = new List<IEntityOperatorItem>();
        
        #endregion
        
        #region Initialization
        
        public void Register(IOperator instance, Type[] arguments, ref SystemState state)
        {
            var type = typeof(EntityOperatorItem<>).MakeGenericType(arguments);
            var itemInstance = (IEntityOperatorItem)Activator.CreateInstance(type);
        
            itemInstance.Register(instance, ref state);
        
            _operators.Add(itemInstance);
        }
        
        #endregion
        
        #region Generic Item
        
        private class EntityOperatorItem<M> : IEntityOperatorItem 
                where M : IMarker
        {
            private IEntityOperator<M> _entityOperator;
        
            public void Register(IOperator instance, ref SystemState state)
            {
                _entityOperator = (IEntityOperator<M>)instance;
                _entityOperator.Entity = state.EntityManager.CreateEntity();
            }
        }
        
        #endregion
        
        #region Item Interface
        
        private interface IEntityOperatorItem
        {
            void Register(IOperator instance, ref SystemState state);
        }
        
        #endregion
    }
}