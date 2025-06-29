using MiniUI.Utilities;
using Unity.Entities;

namespace MiniUI.Operators
{
    public partial struct OperatorsCreateSystem : ISystem, ISystemStartStop
    {
        public void OnStartRunning(ref SystemState state)
        {
            #region Getting variables

            var targetType = typeof(IOperator);
            var types = ClassTypeUtility.GetAssignableTypesFrom(targetType);

            #endregion

            foreach (var type in types)
            {
                Operators.Instance.Add(type, ref state);
            }
        }

        public void OnStopRunning(ref SystemState state) { }
    }
}