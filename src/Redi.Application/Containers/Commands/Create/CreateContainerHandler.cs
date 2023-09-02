﻿using MediatR;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.ContainerAggregate;

namespace Redi.Application.Containers.Commands.Create
{
    public class CreateContainerHandler : IRequestHandler<CreateContainer, ContainerDto>
    {
        private readonly IContainerRepository _containerRepository;

        public CreateContainerHandler(IContainerRepository containerRepository)
            => _containerRepository = containerRepository;        

        public async Task<ContainerDto> Handle(CreateContainer request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Create new container
            var newContainer = Container.Create(
                Guid.NewGuid(),
                request.Name);

            // Persist
            _containerRepository.Add(newContainer);

            return new(newContainer.Id, newContainer.Name);
        }
    }
}
