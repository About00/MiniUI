using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace MiniUI.Operators
{
    internal class EcbOperatorList
    {
        #region Private Fields
        
        private readonly List<IEcbOperator> _operators = new List<IEcbOperator>();
    
        #endregion
        
        #region Initialization
        
        public void Register(IEcbOperator instance)
        {
            _operators.Add(instance);
        
            CreateEntityCommandBuffer(instance);
        }
        #endregion
        
        #region EcbOperatorList Logic
        
        public void CreateEntityCommandBuffer(IEcbOperator instance)
        {
            if (instance.ECB.IsCreated)
            {
                instance.ECB.Dispose();
            }
        
            instance.ECB = new EntityCommandBuffer(Allocator.TempJob);
        }
    
        public void DisposeEntityCommandBuffers()
        {
            foreach (var op in _operators)
            {
                if (op.ECB.IsCreated)
                {
                    op.ECB.Dispose();
                }
            }
        }
    
        public void PlaybackEntityCommandBuffers(ref SystemState state)
        {
            foreach (var op in _operators)
            {
                if (op.ECB.IsCreated)
                {
                    op.ECB.Playback(state.EntityManager);
                
                    CreateEntityCommandBuffer(op);
                }
            }
        }
        
        #endregion
    }
}