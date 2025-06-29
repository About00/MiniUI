using MiniUI.Utilities;
using Unity.Entities;

namespace MiniUI.UIScreens
{
    [UpdateInGroup(typeof(UIScreenHandlingSystemGroup))]
    public partial struct ScreensCreateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            #region [ Getting variables ]

            var targetType = typeof(UIScreen);
            var types = ClassTypeUtility.GetImplementedTypesFrom(targetType);

            #endregion

            foreach (var type in types)
            {
                Screens.Instance.TryAdd(type);
            }
        }
    }
}