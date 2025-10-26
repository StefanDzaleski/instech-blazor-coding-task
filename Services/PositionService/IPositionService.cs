using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

public interface IPositionService
{
    bool IsOverlapping(Vessel selectedVessel, IEnumerable<Vessel> allVessels);

    bool IsInAnchorage(Vessel selectedVessel, Anchorage anchorage);
}