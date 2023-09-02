using Microsoft.EntityFrameworkCore;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.ContainerAggregate;
using Redi.Domain.Aggregates.ContainerAggregate.Entities;
using Redi.Domain.Aggregates.ContainerAggregate.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Repositories
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly RediDbContext _rediDbContext;

        public ContainerRepository(RediDbContext rediDbContext)
            => _rediDbContext = rediDbContext;        

        public void Add(Container container)
        {
            if (_rediDbContext.Containers.Contains(container))
                throw new Exception($"Duplicate Id Exception: There is already a Container with Id {container.Id}");

            _rediDbContext.Containers.Add(container);

            _rediDbContext.SaveChanges();
        }

        public void DeleteById(ContainerId id)
        {
            var container = _rediDbContext.Containers.Find(id)
                ?? throw new Exception("Container not found");

            if (container.IsRoot())
                throw new Exception("You cannot remove the ROOT container");

            if (_rediDbContext.Containers.Any(c => c.Parent != null && c.Parent.Id == id))
                throw new Exception("Referential Ingegrity Violation: You must remove all child objects before deleteing.");

            _rediDbContext.Containers.Remove(container);

            _rediDbContext.SaveChanges();

        }

        public List<Container> GetAll()
        {
            _rediDbContext.StakerMemberships.Load();
            return _rediDbContext.Containers.ToList();
        }

        public Container? GetById(ContainerId id)
        {

            _rediDbContext.Containers.Load();
            _rediDbContext.StakerMemberships.Load();

            var container = _rediDbContext.Containers.Find(id)
                ?? throw new Exception("Container not found");

            return container;
        }

        public Container? GetRoot()
        {
            _rediDbContext.Containers.Load();
            _rediDbContext.StakerMemberships.Load();

            var container = _rediDbContext.Containers.FirstOrDefault(c => c.Parent == null && c.Stake == 1m) 
                ??  throw new Exception("Root container not found");

            return container;
        }

        public void Update(Container container)
        {
            Queue<Container> currentContainerQueue = new();
            currentContainerQueue.Enqueue(container);
            Container currentContainer;

            while (currentContainerQueue.Count > 0)
            {
                currentContainer = currentContainerQueue.Dequeue();
                _rediDbContext.Update(currentContainer);

                foreach (var child in currentContainer.ChildContainers)
                    currentContainerQueue.Enqueue(child);
                
                foreach (var member in currentContainer.Memberships)
                {
                    if (!_rediDbContext.StakerMemberships.Contains(member))
                        _rediDbContext.StakerMemberships.Add(member);
                    else
                        _rediDbContext.Update(member);
                }
                
                _rediDbContext.SaveChanges();

            }            
        }
    }
}
