using instech_blazor_coding_task.Models;
using System.Net.Http.Json;

namespace instech_blazor_coding_task.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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

