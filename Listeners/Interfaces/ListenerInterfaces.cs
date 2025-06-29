using Unity.Entities;

namespace MiniUI.Listeners
{
    public interface IListener
    {
        void Initialize();
    }

    public interface IDataListener<D> : IListener
        where D : unmanaged, IComponentData
    {
        void OnDataChanged(D data);
    }

    public interface IEventListener<E> : IListener
        where E : unmanaged, IComponentData
    {
        void OnHasEvent(E evnt);
    }

    public interface IMarkableDataListener<D, M> : IListener
        where D : unmanaged, IComponentData
        where M : unmanaged, IComponentData
    {
        void OnDataChanged(D data);
    }
}