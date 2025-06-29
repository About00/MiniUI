using Unity.Entities;

namespace MiniUI.UIScreens
{
    [UpdateInGroup(typeof(UIScreenHandlingSystemGroup))]
    [UpdateAfter(typeof(ScreensCreateSystem))]
    public partial struct ScreensUpdateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var _ in SystemAPI.Query<RefRO<UxmlDocumentToUse>>()
                                       .WithNone<UxmlDocumentWasAppliedEvent>())
            {
                #region [ Getting variables ]

                var root = MainUIDocument.Instance.rootVisualElement;

                #endregion

                Screens.Instance.Update(root);
            }
        }
    }
}