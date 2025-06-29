using Unity.Entities;
using UnityEngine;
using UnityEngine.UIElements;

public class UxmlDocumentToUseAuthoring : MonoBehaviour
{
    public VisualTreeAsset UxmlDocument;
    
    public class UxmlDocumentToUseBaker : Baker<UxmlDocumentToUseAuthoring>
    {
        public override void Bake(UxmlDocumentToUseAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            
            AddComponent(entity, new UxmlDocumentToUse
            {
                UxmlDocument = authoring.UxmlDocument
            });
        }
    }
}