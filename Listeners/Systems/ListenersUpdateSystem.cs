using Unity.Entities;

namespace MiniUI.Listeners
{
    [UpdateInGroup(typeof(ListenersSystemGroup))]
    public partial struct ListenersUpdateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            Listeners.Instance.DataListenerList.Update();
            Listeners.Instance.EventListenerList.Update(ref state);
            Listeners.Instance.MarkableDataListenerList.Update();
        }
    }
}