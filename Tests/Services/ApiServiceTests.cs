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
            anchorageSize = new Anchorage { Width = 25, Height = 25 },
            fleets = new List<Fleet>
            {
                new Fleet
                {
                    shipDesignation = "Carrier",
                    shipCount = 1,
                    singleShipDimensions = new SingleShipDimensions { width = 5, height = 5 }
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
        Assert.Equal(25, result.anchorageSize.Width);
        Assert.Equal(25, result.anchorageSize.Height);
        Assert.Single(result.fleets);
        Assert.Equal("Carrier", result.fleets[0].shipDesignation);
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
            anchorageSize = new Anchorage { Width = 30, Height = 30 },
            fleets = new List<Fleet>
            {
                new Fleet { shipDesignation = "Carrier", shipCount = 1, singleShipDimensions = new SingleShipDimensions { width = 5, height = 5 } },
                new Fleet { shipDesignation = "Battleship", shipCount = 2, singleShipDimensions = new SingleShipDimensions { width = 4, height = 4 } }
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
        Assert.Equal(2, result.fleets.Count);
        Assert.Equal("Carrier", result.fleets[0].shipDesignation);
        Assert.Equal("Battleship", result.fleets[1].shipDesignation);
    }
}

