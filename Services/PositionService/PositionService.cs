using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Implementation of IPositionService that is responsible for checking the position of vessels.
/// </summary>
public class PositionService : IPositionService
{
    /// <summary>
    /// Small epsilon value for floating-point comparisons to handle precision issues
    /// </summary>
    private const double Epsilon = 1e-9;

    private const double LeftPadding = 32;
    private const double TopPadding = 32;
    private const double HeaderHeight = 106;
    private const double HeaderMargin = 24;
    /// <summary>
    /// Gets the effective width of a vessel based on its rotation
    /// </summary>
    private static double GetEffectiveWidth(Vessel vessel)
    {
        var rotationMod180 = vessel.Rotation % 180;
        return Math.Abs(rotationMod180 - 90) < Epsilon ? vessel.Height : vessel.Width;
    }

    /// <summary>
    /// Gets the effective height of a vessel based on its rotation
    /// </summary>
    private static double GetEffectiveHeight(Vessel vessel)
    {
        var rotationMod180 = vessel.Rotation % 180;
        return Math.Abs(rotationMod180 - 90) < Epsilon ? vessel.Width : vessel.Height;
    }

    /// <summary>
    /// Gets the actual bounding box position after accounting for rotation around center
    /// </summary>
    private static (double x, double y, double width, double height) GetBoundingBox(Vessel vessel)
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
        var (selectedX, selectedY, selectedWidth, selectedHeight) = GetBoundingBox(selectedVessel);

        foreach (var vessel in allVessels)
        {
            if (vessel == selectedVessel) continue;

            var (vesselX, vesselY, vesselWidth, vesselHeight) = GetBoundingBox(vessel);

            var overlapX = selectedX < vesselX + vesselWidth + Epsilon && selectedX + selectedWidth > vesselX - Epsilon;
            var overlapY = selectedY < vesselY + vesselHeight + Epsilon && selectedY + selectedHeight > vesselY - Epsilon;

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
    public bool IsInAnchorage(Vessel selectedVessel, Anchorage anchorage)
    {
        const double anchorageTopLeftPositionX = LeftPadding;
        const double anchorageTopLeftPositionY = TopPadding + HeaderHeight + HeaderMargin; // Amounts to 162px

        var (vesselX, vesselY, vesselWidth, vesselHeight) = GetBoundingBox(selectedVessel);

        var leftEdgeInside = vesselX >= anchorageTopLeftPositionX - Epsilon;
        var rightEdgeInside = vesselX + vesselWidth <= anchorageTopLeftPositionX + anchorage.Width + Epsilon;
        var topEdgeInside = vesselY >= anchorageTopLeftPositionY - Epsilon;
        var bottomEdgeInside = vesselY + vesselHeight <= anchorageTopLeftPositionY + anchorage.Height + Epsilon;

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