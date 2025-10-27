using instech_blazor_coding_task.Models;
using instech_blazor_coding_task.Services;
using Xunit;

namespace instech_blazor_coding_task.Tests.Services;

public class RotationServiceTests
{
    private readonly RotationService _rotationService;

    public RotationServiceTests()
    {
        _rotationService = new RotationService();
    }

    [Fact]
    public void RotateVessel_ShouldRotateBy90Degrees()
    {
        var vessel = new Vessel { Width = 100, Height = 50, Rotation = 0 };

        _rotationService.RotateVessel(vessel);

        Assert.Equal(90, vessel.Rotation);
    }

    [Fact]
    public void RotateVessel_ShouldHandleMultipleRotations()
    {
        var vessel = new Vessel { Width = 100, Height = 50, Rotation = 0 };

        _rotationService.RotateVessel(vessel);
        _rotationService.RotateVessel(vessel);

        Assert.Equal(180, vessel.Rotation);
        Assert.Equal(100, vessel.Width);
        Assert.Equal(50, vessel.Height);
    }

    [Fact]
    public void RotateVessel_ShouldHandleNullVessel()
    {
        _rotationService.RotateVessel(null!);
    }
}

