using Unity.Entities;
using UnityEngine.UIElements;

public struct UxmlDocumentToUse : IComponentData
{
    public UnityObjectRef<VisualTreeAsset> UxmlDocument;
}