using Unity.Collections;
using Unity.Entities;

[UpdateInGroup(typeof(UIToolkitLinkerSystemGroup), OrderLast = true)]
public partial struct UxmlDocumentWasAppliedEventAddSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        if (MainUIDocument.Instance == null)
        {
            return;
        }
        
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        
        foreach (var enumerable in SystemAPI.Query<RefRO<UxmlDocumentToUse>>()
                                            .WithNone<UxmlDocumentWasAppliedEvent>()
                                            .WithEntityAccess())
        {
            #region [ Getting variables ]

            var entity = enumerable.Item2;
            
            #endregion
            
            ecb.AddComponent<UxmlDocumentWasAppliedEvent>(entity);
        }
        
        ecb.Playback(state.EntityManager);
    }
}