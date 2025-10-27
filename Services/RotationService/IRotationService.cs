using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Service interface for handling vessel rotation operations.
/// </summary>
public interface IRotationService
{
    /// <summary>
    /// Rotates a vessel by 90 degrees clockwise.
    /// Updates the rotation angle. Width and height remain as original dimensions.
    /// </summary>
    /// <param name="vessel">The vessel to rotate</param>
    void RotateVessel(Vessel vessel);
}

