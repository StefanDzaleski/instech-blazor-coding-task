using instech_blazor_coding_task.Models;
using instech_blazor_coding_task.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Xunit;

namespace instech_blazor_coding_task.Tests.Services;

public class ApiServiceTests
{
    private Mock<HttpMessageHandler> CreateMockHttpMessageHandler(HttpStatusCode statusCode, string content)
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            });

        return mockHandler;
    }

    [Fact]
    public async Task GetRandomFleetAsync_ShouldReturnApiResponse_WhenRequestSucceeds()
    {
        var expectedResponse = new ApiResponse
        {
            AnchorageSize = new Anchorage { Width = 25, Height = 25 },
            Fleets = new List<Fleet>
            {
                new Fleet
                {
                    ShipDesignation = "Carrier",
                    ShipCount = 1,
                    SingleShipDimensions = new SingleShipDimensions { Width = 5, Height = 5 }
                }
            }
        };

        var jsonContent = JsonSerializer.Serialize(expectedResponse);
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.OK, jsonContent);
        var httpClient = new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://esa.instech.no/")
        };

        var apiService = new ApiService(httpClient);

        var result = await apiService.GetRandomFleetAsync();

        Assert.NotNull(result);
        Assert.Equal(25, result.AnchorageSize.Width);
        Assert.Equal(25, result.AnchorageSize.Height);
        Assert.Single(result.Fleets);
        Assert.Equal("Carrier", result.Fleets[0].ShipDesignation);
    }

    [Fact]
    public async Task GetRandomFleetAsync_ShouldReturnNull_WhenRequestFails()
    {
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.InternalServerError, "Error");
        var httpClient = new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://esa.instech.no/")
        };

        var apiService = new ApiService(httpClient);

        var result = await apiService.GetRandomFleetAsync();

        Assert.Null(result);
    }

    [Fact]
    public async Task GetRandomFleetAsync_ShouldHandleMultipleFleets()
    {
        var expectedResponse = new ApiResponse
        {
            AnchorageSize = new Anchorage { Width = 30, Height = 30 },
            Fleets = new List<Fleet>
            {
                new Fleet { ShipDesignation = "Carrier", ShipCount = 1, SingleShipDimensions = new SingleShipDimensions { Width = 5, Height = 5 } },
                new Fleet { ShipDesignation = "Battleship", ShipCount = 2, SingleShipDimensions = new SingleShipDimensions { Width = 4, Height = 4 } }
            }
        };

        var jsonContent = JsonSerializer.Serialize(expectedResponse);
        var mockHandler = CreateMockHttpMessageHandler(HttpStatusCode.OK, jsonContent);
        var httpClient = new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://esa.instech.no/")
        };

        var apiService = new ApiService(httpClient);

        var result = await apiService.GetRandomFleetAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Fleets.Count);
        Assert.Equal("Carrier", result.Fleets[0].ShipDesignation);
        Assert.Equal("Battleship", result.Fleets[1].ShipDesignation);
    }
}

