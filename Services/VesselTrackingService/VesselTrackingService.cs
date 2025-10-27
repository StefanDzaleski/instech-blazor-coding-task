using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Implementation of IVesselTrackingService that is responsible for tracking vessel placement.
/// </summary>
public class VesselTrackingService : IVesselTrackingService
{
    private Dictionary<string, (int placed, int total)> _trackingInfo = new();

    /// <summary>
    /// Initializes tracking based on fleet data.
    /// </summary>
    /// <param name="fleets">The fleet data containing vessel counts and designations</param>
    public void Initialize(List<Fleet> fleets)
    {
        _trackingInfo.Clear();

        if (fleets == null) return;

        foreach (var fleet in fleets)
        {
            if (fleet?.shipDesignation != null)
            {
                _trackingInfo[fleet.shipDesignation] = (0, fleet.shipCount);
            }
        }
    }

    /// <summary>
    /// Updates tracking counts based on which vessels are in the anchorage.
    /// </summary>
    /// <param name="allVessels">Collection of all vessels</param>
    /// <param name="anchorage">The anchorage dimensions</param>
    /// <param name="positionService">Service to check if vessels are in anchorage</param>
    public void UpdateTracking(IEnumerable<Vessel> allVessels, Anchorage anchorage, IPositionService positionService)
    {
        if (allVessels == null || anchorage == null || positionService == null) return;

        // Reset placed counts
        var keys = _trackingInfo.Keys.ToList();
        foreach (var key in keys)
        {
            var (_, total) = _trackingInfo[key];
            _trackingInfo[key] = (0, total);
        }

        // Count vessels in anchorage by designation
        foreach (var vessel in allVessels)
        {
            if (vessel?.ShipDesignation != null && positionService.IsInAnchorage(vessel, anchorage))
            {
                if (_trackingInfo.ContainsKey(vessel.ShipDesignation))
                {
                    var (placed, total) = _trackingInfo[vessel.ShipDesignation];
                    _trackingInfo[vessel.ShipDesignation] = (placed + 1, total);
                }
            }
        }
    }

    /// <summary>
    /// Gets the current tracking information.
    /// </summary>
    /// <returns>Dictionary with ship designation as key and (placed count, total count) as value</returns>
    public Dictionary<string, (int placed, int total)> GetTrackingInfo()
    {
        return new Dictionary<string, (int placed, int total)>(_trackingInfo);
    }
}

