using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Service for fetching data from the API.
/// </summary>
public interface IApiService
{
    /// <summary>
    /// Fetches a random configuration of an anchorage and fleets from the API.
    /// </summary>
    /// <returns>
    /// An ApiResponse containing anchorage size and an array of fleets
    /// </returns>
    Task<ApiResponse?> GetRandomFleetAsync();
}

