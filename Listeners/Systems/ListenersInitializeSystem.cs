using MiniUI.UIScreens;
using Unity.Entities;

namespace MiniUI.Listeners
{
    [UpdateInGroup(typeof(UIScreenHandlingSystemGroup))]
    [UpdateAfter(typeof(ScreensCreateSystem))]
    public partial struct ListenersInitializeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var _ in SystemAPI.Query<RefRO<UxmlDocumentToUse>>()
                                       .WithNone<UxmlDocumentWasAppliedEvent>())
            {
                Listeners.Instance.ListenerList.InitializeListenersData();
            }
        }
    }
}