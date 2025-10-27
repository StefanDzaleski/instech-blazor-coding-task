using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Service responsible for tracking vessel placement in the anchorage.
/// </summary>
public interface IVesselTrackingService
{
    /// <summary>
    /// Updates the tracking counts based on which vessels are currently in the anchorage.
    /// </summary>
    /// <param name="allVessels">Collection of all vessels</param>
    /// <param name="anchorage">The anchorage dimensions</param>
    /// <param name="positionService">Service to check if vessels are in anchorage</param>
    void UpdateTracking(IEnumerable<Vessel> allVessels, Anchorage anchorage, IPositionService positionService);

    /// <summary>
    /// Gets the tracking information for all vessel types.
    /// </summary>
    /// <returns>Dictionary with ship designation as key and (placed count, total count) as value</returns>
    Dictionary<string, (int placed, int total)> GetTrackingInfo();

    /// <summary>
    /// Resets the tracking information.
    /// </summary>
    /// <param name="fleets">The fleets to initialize tracking from</param>
    void Initialize(List<Fleet> fleets);
}

