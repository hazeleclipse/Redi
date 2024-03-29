﻿using Redi.Application.Persistence;
using Redi.Domain.Aggregates.NodeAggregate;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Repositories
{
    internal class NodeRepository : INodeRepository
    {
        private readonly RediDbContext _rediDbContext;

        public NodeRepository(RediDbContext rediDbContext)
            => _rediDbContext = rediDbContext;

        public void Add(Node container)
        {
            if (_rediDbContext.Nodes.Contains(container))
                return;

            _rediDbContext.Nodes.Add(container);

            _rediDbContext.SaveChanges();
        }

        public void DeleteById(NodeId id)
        {
            var node = _rediDbContext.Nodes.Find(id)
                ?? throw new Exception("Node not found.");

           _rediDbContext.Nodes.Remove(node);
        }

        public List<Node> GetAll()
        {
            return _rediDbContext.Nodes.ToList();
        }

        public Node? GetById(NodeId id)
        {
            var node = _rediDbContext.Nodes.Find(id);
            return node;
        }

        public void Update(Node node)
        {
            Queue<Node> currentNodeQueue = new();
            currentNodeQueue.Enqueue(node);
            Node currentNode;

            while (currentNodeQueue.Count > 0)
            {
                currentNode = currentNodeQueue.Dequeue();
                _rediDbContext.Update(currentNode);

                if (currentNode is CoreNode currentCoreNode)
                {
                    foreach (var child in currentCoreNode.Children)
                        currentNodeQueue.Enqueue(child);
                }

                _rediDbContext.SaveChanges();
            }    
        }
    }
}
