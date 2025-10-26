using instech_blazor_coding_task.Models;
using Microsoft.AspNetCore.Components.Web;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Implementation of IDragService that is responsible for dragging and dropping vessels.
/// Manages the dragging state and position updates.
/// </summary>
public class DragService : IDragService
{
    /// <summary>
    /// Reference to the currently dragged vessel, necessary to know which vessel is being dragged for correct updates.
    /// </summary>
    private Vessel? _draggedVessel;
    
    /// <summary>
    /// X offset between the mouse cursor and the vessel's top-left corner when drag started
    /// Necessary in order to correctly update the position and not jump to the cursor
    /// </summary>
    private double _dragOffsetX;
    
    /// <summary>
    /// Y offset between the mouse cursor and the vessel's top-left corner when drag started
    /// Necessary in order to correctly update the position and not jump to the cursor
    /// </summary>
    private double _dragOffsetY;

    /// <summary>
    /// Starts the drag operation.
    /// Saves the initial position of the vessel so that if it can't be moved somewhere it snaps back into the previous position.
    /// </summary>
    /// <param name="vessel">The vessel to be dragged</param>
    /// <param name="e">Mouse event args containing cursor position at drag start</param>
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

    /// <summary>
    /// Updates the position of the currently dragged vessel based on mouse movement.
    /// Only updates if a drag operation is in progress.
    /// </summary>
    /// <param name="e">Mouse event args containing current cursor position</param>
    /// <remarks>
    /// Position calculation: vessel position = cursor position - drag offset
    /// This maintains the relative grab point throughout the drag operation.
    /// </remarks>
    public void OnDragMove(MouseEventArgs e)
    {
        if (_draggedVessel == null || e == null)
            return;

        _draggedVessel.PositionX = e.ClientX - _dragOffsetX;
        _draggedVessel.PositionY = e.ClientY - _dragOffsetY;
    }

    /// <summary>
    /// Updates the position of the dragged vessel based on mouse movement.

    /// </summary>
    /// <param name="allVessels">Collection of all vessels to check for overlaps</param>
    /// <param name="overlapService">Position service used to check the overlaps</param>
    public void OnDragEnd(IEnumerable<Vessel> allVessels, IPositionService overlapService)
    {
        if (_draggedVessel == null) return;

        if (overlapService.IsOverlapping(_draggedVessel, allVessels)) {
            _draggedVessel.PositionX = _draggedVessel.PreviousPositionX;
            _draggedVessel.PositionY = _draggedVessel.PreviousPositionY;
        }
        
        _draggedVessel = null;
    }
}