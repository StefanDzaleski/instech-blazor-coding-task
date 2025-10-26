using instech_blazor_coding_task.Models;
using instech_blazor_coding_task.Services;

public class OverlapService : IOverlapService
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
}