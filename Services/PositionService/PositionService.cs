using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Implementation of IPositionService that is responsible for checking the position of vessels.
/// </summary>
public class PositionService : IPositionService
{
    /// <summary>
    /// Gets the effective width of a vessel based on its rotation
    /// </summary>
    private double GetEffectiveWidth(Vessel vessel)
    {
        return (vessel.Rotation == 90 || vessel.Rotation == 270) ? vessel.Height : vessel.Width;
    }

    /// <summary>
    /// Gets the effective height of a vessel based on its rotation
    /// </summary>
    private double GetEffectiveHeight(Vessel vessel)
    {
        return (vessel.Rotation == 90 || vessel.Rotation == 270) ? vessel.Width : vessel.Height;
    }

    /// <summary>
    /// Gets the actual bounding box position after accounting for rotation around center
    /// </summary>
    private (double x, double y, double width, double height) GetBoundingBox(Vessel vessel)
    {
        var effectiveWidth = GetEffectiveWidth(vessel);
        var effectiveHeight = GetEffectiveHeight(vessel);
        
        // Calculate the center of the original box
        var centerX = vessel.PositionX + vessel.Width / 2;
        var centerY = vessel.PositionY + vessel.Height / 2;
        
        // Calculate the new top-left position based on effective dimensions
        var newX = centerX - effectiveWidth / 2;
        var newY = centerY - effectiveHeight / 2;
        
        return (newX, newY, effectiveWidth, effectiveHeight);
    }

    /// <summary>
    /// Checks if the selected vessel overlaps with any other vessels
    /// </summary>
    /// <param name="selectedVessel">The vessel to check for overlaps</param>
    /// <param name="allVessels">A list of all vessels that need to be checked</param>
    /// <returns>True if the selected vessel overlaps with any other vesse, else false</returns>
    public bool IsOverlapping(Vessel selectedVessel, IEnumerable<Vessel> allVessels)
    {
        if (selectedVessel == null || allVessels == null) return false;

        var (selectedX, selectedY, selectedWidth, selectedHeight) = GetBoundingBox(selectedVessel);

        foreach (var vessel in allVessels)
        {
            if (vessel == selectedVessel) continue;

            var (vesselX, vesselY, vesselWidth, vesselHeight) = GetBoundingBox(vessel);

            bool overlapX = selectedX < vesselX + vesselWidth && selectedX + selectedWidth > vesselX;
            bool overlapY = selectedY < vesselY + vesselHeight && selectedY + selectedHeight > vesselY;

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
        var anchorageTopLeftPositionY = 162;

        var (vesselX, vesselY, vesselWidth, vesselHeight) = GetBoundingBox(selectedVessel);

        bool leftEdgeInside = vesselX >= anchorageTopLeftPositionX;
        bool rightEdgeInside = vesselX + vesselWidth <= anchorageTopLeftPositionX + anchorage.Width;
        bool topEdgeInside = vesselY >= anchorageTopLeftPositionY;
        bool bottomEdgeInside = vesselY + vesselHeight <= anchorageTopLeftPositionY + anchorage.Height;

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