using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

public interface IApiService
{
    Task<ApiResponse?> GetRandomFleetAsync();
}

