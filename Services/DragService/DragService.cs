using instech_blazor_coding_task.Models;
using instech_blazor_coding_task.Services;
using Microsoft.AspNetCore.Components.Web;

namespace instech_blazor_coding_task.Services;

public class DragService : IDragService
{
    private Vessel? _draggedVessel;
    private double _dragOffsetX;
    private double _dragOffsetY;

    public void OnDragStart(Vessel vessel, MouseEventArgs e)
    {
        if (vessel == null || e == null) return;

        _draggedVessel = vessel;

        // Save the position of the vessel, so that if it can't be moved somewhere it snaps back into the previous position
        _draggedVessel.PreviousPositionX = _draggedVessel.PositionX;
        _draggedVessel.PreviousPositionY = _draggedVessel.PositionY;

        _dragOffsetX = e.ClientX - vessel.PositionX;
        _dragOffsetY = e.ClientY - vessel.PositionY;
    }

    public void OnDragMove(MouseEventArgs e)
    {
        if (_draggedVessel == null || e == null)
            return;

        _draggedVessel.PositionX = e.ClientX - _dragOffsetX;
        _draggedVessel.PositionY = e.ClientY - _dragOffsetY;
    }

    public void OnDragEnd(IEnumerable<Vessel> allVessels, IOverlapService overlapService)
    {
        if (_draggedVessel == null) return;

        if (overlapService.IsOverlapping(_draggedVessel, allVessels)) {
            _draggedVessel.PositionX = _draggedVessel.PreviousPositionX;
            _draggedVessel.PositionY = _draggedVessel.PreviousPositionY;
        }
        
        _draggedVessel = null;
    }
}