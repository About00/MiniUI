using System;
using MiniUI.Utilities;
using Unity.Entities;

namespace MiniUI.Operators
{
    internal class Operators : Singleton<Operators>
    {
        #region Public Properties
        
        public OperatorList OperatorList { get; } = new OperatorList();
        public EcbOperatorList EcbOperatorList { get; } = new EcbOperatorList();
        public EntityOperatorList EntityOperatorList { get; } = new EntityOperatorList();
        public EntityDataOperatorList EntityDataOperatorList { get; } = new EntityDataOperatorList();
        
        #endregion
        
        #region Operators Logic
        
        public void Add(Type type, ref SystemState state)
        {
            var instance = (IOperator)Activator.CreateInstance(type);

            OperatorList.Register(instance);

            if (typeof(IEcbOperator).IsAssignableFrom(type))
            {
                EcbOperatorList.Register((IEcbOperator)instance);
            }

            if (ClassTypeUtility.IsAssignableFromGenericInterface(type, typeof(IEntityOperator<>)))
            {
                foreach (var arguments in ClassTypeUtility.GetGenericArgumentTypes(type, typeof(IEntityOperator<>)))
                {
                    EntityOperatorList.Register(instance, arguments, ref state);
                }
            }

            if (ClassTypeUtility.IsAssignableFromGenericInterface(type, typeof(IEntityDataOperator<>)))
            {
                foreach (var arguments in ClassTypeUtility.GetGenericArgumentTypes(type, typeof(IEntityDataOperator<>)))
                {
                    EntityDataOperatorList.Register(instance, arguments, ref state);
                }
            }
        }
        
        #endregion
    }
}