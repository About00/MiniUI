using Unity.Entities;

namespace MiniUI.Operators
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct EcbOperatorsDisposeSystem : ISystem
    {
        public void OnDestroy(ref SystemState state)
        {
            Operators.Instance.EcbOperatorList.DisposeEntityCommandBuffers();
        }
    }
}