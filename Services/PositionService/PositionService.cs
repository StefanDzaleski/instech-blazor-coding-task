using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Implementation of IPositionService that is responsible for checking the position of vessels.
/// </summary>
public class PositionService : IPositionService
{
    /// <summary>
    /// Checks if the selected vessel overlaps with any other vessels
    /// </summary>
    /// <param name="selectedVessel">The vessel to check for overlaps</param>
    /// <param name="allVessels">A list of all vessels that need to be checked</param>
    /// <returns>True if the selected vessel overlaps with any other vesse, else false</returns>
    public bool IsOverlapping(Vessel selectedVessel, IEnumerable<Vessel> allVessels)
    {
        if (selectedVessel == null || allVessels == null) return false;

        foreach (var vessel in allVessels)
        {
            if (vessel == selectedVessel) continue;

            bool overlapX = selectedVessel.PositionX < vessel.PositionX + vessel.Width && selectedVessel.PositionX + selectedVessel.Width > vessel.PositionX;
            bool overlapY = selectedVessel.PositionY < vessel.PositionY + vessel.Height && selectedVessel.PositionY + selectedVessel.Height > vessel.PositionY;

            if (overlapX && overlapY)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if a vessel is completely in the anchorage.
    /// </summary>
    /// <param name="selectedVessel">The vessel to check</param>
    /// <param name="anchorage">The anchorage dimensions</param>
    /// <returns>True if the vessel is fully inside the anchorage, else false</returns>
    /// <remarks>
    /// The anchorage position is fixed on the page:
    /// - X offset: 32px (left padding)
    /// - Y offset: 82px (top padding + header height)
    /// </remarks>
    public bool IsInAnchorage(Vessel selectedVessel, Anchorage anchorage)
    {
        if (selectedVessel == null || anchorage == null) return false;

        var anchorageTopLeftPositionX = 32;
        var anchorageTopLeftPositionY = 82;

        bool leftEdgeInside = selectedVessel.PositionX >= anchorageTopLeftPositionX;
        bool rightEdgeInside = selectedVessel.PositionX + selectedVessel.Width <= anchorageTopLeftPositionX + anchorage.Width;
        bool topEdgeInside = selectedVessel.PositionY >= anchorageTopLeftPositionY;
        bool bottomEdgeInside = selectedVessel.PositionY + selectedVessel.Height <= anchorageTopLeftPositionY + anchorage.Height;

        return leftEdgeInside && rightEdgeInside && topEdgeInside && bottomEdgeInside;
    }

    /// <summary>
    /// Checks if all vessels in the list are completely in the anchorage.
    /// </summary>
    /// <param name="allVessels">A list of all vessels that need to be checked</param>
    /// <param name="anchorage">The anchorage dimensions</param>
    /// <returns>True if all vessels are fully inside the anchorage, else false</returns>
    public bool AllVesselsInAnchorage(IEnumerable<Vessel> allVessels, Anchorage anchorage)
    {
        if (allVessels == null || anchorage == null) return false;

        foreach (var vessel in allVessels)
        {
            if (!IsInAnchorage(vessel, anchorage))
            {
                return false;
            }
        }

        return true;
    }
}