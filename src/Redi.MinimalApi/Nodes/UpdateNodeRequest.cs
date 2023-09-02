using MediatR;

namespace Redi.MinimalApi;

public record UpdateNodeRequest(string Name) : IRequest;
