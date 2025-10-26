using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Service responsible for checking the position of vessels
/// </summary>
public interface IPositionService
{
    /// <summary>
    /// Checks if the selected vessel overlaps with any other vessels.
    /// </summary>
    /// <param name="selectedVessel">The vessel to check for overlaps</param>
    /// <param name="allVessels">A list of all vessels that need to be checked</param>
    /// <returns>True if the selected vessel overlaps with any other vessel, else false</returns>
    bool IsOverlapping(Vessel selectedVessel, IEnumerable<Vessel> allVessels);

    /// <summary>
    /// Checks if a vessel is inside the anchorage.
    /// </summary>
    /// <param name="selectedVessel">The vessel to check</param>
    /// <param name="anchorage">The anchorage dimensions</param>
    /// <returns>True if the vessel is completely in the anchorage, else false</returns>
    bool IsInAnchorage(Vessel selectedVessel, Anchorage anchorage);

    /// <summary>
    /// Checks if all vessels in the list are completely in the anchorage.
    /// </summary>
    /// <param name="allVessels">A list of all vessels that need to be checked</param>
    /// <param name="anchorage">The anchorage boundaries to check against</param>
    /// <returns>True if all vessels are completely in the anchorage, else false</returns>
    bool AllVesselsInAnchorage(IEnumerable<Vessel> allVessels, Anchorage anchorage);
}