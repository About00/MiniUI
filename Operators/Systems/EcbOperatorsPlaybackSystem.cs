using Unity.Entities;

namespace MiniUI.Operators
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct EcbOperatorsPlaybackSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            Operators.Instance.EcbOperatorList.PlaybackEntityCommandBuffers(ref state);
        }
    }
}