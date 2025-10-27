using instech_blazor_coding_task.Models;

namespace instech_blazor_coding_task.Services;

/// <summary>
/// Implementation of IRotationService responsible for rotating vessels.
/// </summary>
public class RotationService : IRotationService
{
    /// <summary>
    /// Rotates a vessel by 90 degrees clockwise.
    /// Updates the rotation angle only. Width and height represent the original dimensions.
    /// </summary>
    /// <param name="vessel">The vessel to rotate</param>
    public void RotateVessel(Vessel? vessel)
    {
        ArgumentNullException.ThrowIfNull(vessel);
        
        vessel.Rotation = (vessel.Rotation + 90) % 360;
    }
}

