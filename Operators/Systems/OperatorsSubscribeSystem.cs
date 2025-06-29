using MiniUI.UIScreens;
using Unity.Entities;

namespace MiniUI.Operators
{
    [UpdateInGroup(typeof(UIScreenHandlingSystemGroup))]
    [UpdateAfter(typeof(ScreensUpdateSystem))]
    public partial struct OperatorsSubscribeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var _ in SystemAPI.Query<RefRO<UxmlDocumentToUse>>()
                                       .WithNone<UxmlDocumentWasAppliedEvent>())
            {
                Operators.Instance.OperatorList.InitializeOperatorsData();
            }
        }
    }
}