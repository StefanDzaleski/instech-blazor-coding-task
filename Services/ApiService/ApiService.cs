using instech_blazor_coding_task.Models;
using System.Net.Http.Json;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Implementation of IApiService that fetched data from the API.
/// </summary>
public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the ApiService class.
    /// </summary>
    /// <param name="httpClient">HTTP client with configured base address for the API</param>
    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Fetches a random configuration of an anchorage and fleets from the API.
    /// </summary>
    /// <returns>
    /// An ApiResponse containing anchorage size and an array of fleets
    /// </returns>
    public async Task<ApiResponse?> GetRandomFleetAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse>("api/fleets/random");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching fleet data: {ex.Message}");
            return null;
        }
    }
}

