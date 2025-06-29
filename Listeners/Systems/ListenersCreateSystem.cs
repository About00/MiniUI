using MiniUI.Utilities;
using Unity.Entities;

namespace MiniUI.Listeners
{
    [UpdateInGroup(typeof(ListenersSystemGroup))]
    public partial struct ListenersCreateSystem : ISystem, ISystemStartStop
    {
        public void OnStartRunning(ref SystemState state)
        {
            #region Getting variables

            var targetType = typeof(IListener);
            var types = ClassTypeUtility.GetAssignableTypesFrom(targetType);

            #endregion

            foreach (var type in types)
            {
                Listeners.Instance.Add(type, ref state);
            }
        }

        public void OnStopRunning(ref SystemState state) { }
    }
}