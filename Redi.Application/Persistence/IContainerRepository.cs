using Redi.Domain.Aggregates.ContainerAggregate;
using Redi.Domain.Aggregates.ContainerAggregate.ValueObjects;

namespace Redi.Application.Persistence
{
    public interface IContainerRepository
    {
        void Add(Container container);
        void DeleteById(ContainerId id);
        List<Container> GetAll();
        Container? GetById(ContainerId id);
        Container? GetRoot();
        void Update(Container container);
    }
}
