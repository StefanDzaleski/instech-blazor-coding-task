using instech_blazor_coding_task.Models;
using instech_blazor_coding_task.Services;

namespace instech_blazor_coding_task.Services;

public class PositionService : IPositionService
{
    public bool IsOverlapping(Vessel selectedVessel, IEnumerable<Vessel> allVessels)
    {
        if (selectedVessel == null || allVessels == null) return false;

        foreach (var vessel in allVessels)
        {
            if (vessel == selectedVessel) continue;

            bool overlapX = selectedVessel.PositionX < vessel.PositionX + vessel.Width && selectedVessel.PositionX + selectedVessel.Width > vessel.PositionX;
            bool overlapY = selectedVessel.PositionY < vessel.PositionY + vessel.Height && selectedVessel.PositionY + selectedVessel.Height > vessel.PositionY;

            return overlapX && overlapY;
        }

        return false;
    }

    public bool IsInAnchorage(Vessel selectedVessel, Anchorage anchorage)
    {
        if (selectedVessel == null || anchorage == null) return false;

        // Left and top paddings of the page are 32px, and there is the header above the anchorage too which makes it 82px
        // The anchorage always has fixed starting points
        var anchorageTopLeftPositionX = 32;
        var anchorageTopLeftPositionY = 82;

        // Check if all edges of the vessel are within the anchorage boundaries
        bool leftEdgeInside = selectedVessel.PositionX >= anchorageTopLeftPositionX;
        bool rightEdgeInside = selectedVessel.PositionX + selectedVessel.Width <= anchorageTopLeftPositionX + anchorage.Width;
        bool topEdgeInside = selectedVessel.PositionY >= anchorageTopLeftPositionY;
        bool bottomEdgeInside = selectedVessel.PositionY + selectedVessel.Height <= anchorageTopLeftPositionY + anchorage.Height;

        return leftEdgeInside && rightEdgeInside && topEdgeInside && bottomEdgeInside;
    }
}