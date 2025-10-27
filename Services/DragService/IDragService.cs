using instech_blazor_coding_task.Models;
using Microsoft.AspNetCore.Components.Web;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Service responsible for dragging and dropping vessels.
/// Manages the dragging state and position updates.
/// </summary>
public interface IDragService
{
    /// <summary>
    /// Starts the drag operation.
    /// Saves the initial position of the vessel so that if it can't be moved somewhere it snaps back into the previous position.
    /// </summary>
    /// <param name="vessel">The vessel to be dragged</param>
    /// <param name="e">Mouse event args containing cursor position</param>
    void OnDragStart(Vessel vessel, MouseEventArgs e);

    /// <summary>
    /// Updates the position of the dragged vessel based on mouse movement.
    /// </summary>
    /// <param name="e">Mouse event args containing current cursor position</param>
    void OnDragMove(MouseEventArgs e);

    /// <summary>
    /// End the drag operation.
    /// If the vessel overlaps with another, it snaps back to its previous position.
    /// </summary>
    /// <param name="allVessels">Collection of all vessels to check for overlaps</param>
    /// <param name="positionService">Position service used for checking the overlaps</param>
    void OnDragEnd(IEnumerable<Vessel> allVessels, IPositionService positionService);
}