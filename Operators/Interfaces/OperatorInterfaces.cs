using Unity.Entities;

namespace MiniUI.Operators
{
    public interface IOperator
    {
        void Initialize();
        void Subscribe();
    }

    public interface IEcbOperator : IOperator
    {
        public EntityCommandBuffer ECB { get; internal set; }
    }

    public interface IEntityOperator<M> : IEcbOperator
            where M : IMarker
    {
        Entity Entity { get; internal set; }
    }

    public interface IMarker { }

    public interface IEntityDataOperator<D> : IEcbOperator
            where D : IComponentData
    {
        Entity Entity { get; internal set; }
    }
}