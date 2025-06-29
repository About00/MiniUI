using Unity.Entities;

[UpdateInGroup(typeof(UIToolkitLinkerSystemGroup), OrderFirst = true)]
public partial struct VisualTreeAssetSetSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        if (MainUIDocument.Instance == null)
        {
            return;
        }
        
        foreach (var enumerable in SystemAPI.Query<RefRO<UxmlDocumentToUse>>()
                                            .WithNone<UxmlDocumentWasAppliedEvent>())
        {
            #region [ Getting variables ]
            
            var uxmlDocument = enumerable.ValueRO.UxmlDocument;
            
            #endregion
            
            MainUIDocument.Instance.visualTreeAsset = uxmlDocument.Value;
        }
    }
}