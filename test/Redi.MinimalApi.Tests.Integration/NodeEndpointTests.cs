using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Redi.MinimalApi.Tests.Integration;

public class NodeQueryEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public NodeQueryEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void GetNodeById_EntityExists_ReturnOkAndSpecifiedNodeDto()
    {
        // Arrange
        var endpoint = "/api/nodes/01db6f7d-e4ce-44d0-9c35-105bb95b50dc";
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(endpoint);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async void UpdateNode_EntityExists_ReturnNoContent()
    {
        // Arrange
        var endpoint = "/api/nodes/01db6f7d-e4ce-44d0-9c35-105bb95b50dc";
        var client = _factory.CreateClient();
        var request = new UpdateNodeRequest("new name");
        

        // Act
        var response = await client.PutAsJsonAsync(endpoint, request);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}