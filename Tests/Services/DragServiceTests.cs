using instech_blazor_coding_task.Models;
using instech_blazor_coding_task.Services;
using Microsoft.AspNetCore.Components.Web;
using Moq;
using Xunit;

namespace instech_blazor_coding_task.Tests.Services;

public class DragServiceTests
{
    private readonly DragService _dragService;
    private readonly Mock<IPositionService> _mockPositionService;

    public DragServiceTests()
    {
        _dragService = new DragService();
        _mockPositionService = new Mock<IPositionService>();
    }

    [Fact]
    public void OnDragStart_ShouldSavePreviousPosition()
    {
        var vessel = new Vessel { PositionX = 100, PositionY = 150, Width = 50, Height = 50 };
        var mouseArgs = new MouseEventArgs { ClientX = 120, ClientY = 170 };

        _dragService.OnDragStart(vessel, mouseArgs);

        Assert.Equal(100, vessel.PreviousPositionX);
        Assert.Equal(150, vessel.PreviousPositionY);
    }

    [Fact]
    public void OnDragMove_ShouldUpdateVesselPosition()
    {
        var vessel = new Vessel { PositionX = 100, PositionY = 150, Width = 50, Height = 50 };
        var startMouseArgs = new MouseEventArgs { ClientX = 120, ClientY = 170 };
        var moveMouseArgs = new MouseEventArgs { ClientX = 200, ClientY = 250 };

        _dragService.OnDragStart(vessel, startMouseArgs);

        _dragService.OnDragMove(moveMouseArgs);

        Assert.Equal(180, vessel.PositionX);
        Assert.Equal(230, vessel.PositionY);
    }

    [Fact]
    public void OnDragEnd_ShouldKeepPosition_WhenNoOverlap()
    {
        var vessel = new Vessel { PositionX = 100, PositionY = 150, Width = 50, Height = 50 };
        var allVessels = new List<Vessel> { vessel };
        var startMouseArgs = new MouseEventArgs { ClientX = 120, ClientY = 170 };
        var moveMouseArgs = new MouseEventArgs { ClientX = 200, ClientY = 250 };

        _mockPositionService.Setup(x => x.IsOverlapping(vessel, allVessels)).Returns(false);

        _dragService.OnDragStart(vessel, startMouseArgs);
        _dragService.OnDragMove(moveMouseArgs);

        _dragService.OnDragEnd(allVessels, _mockPositionService.Object);

        Assert.Equal(180, vessel.PositionX);
        Assert.Equal(230, vessel.PositionY);
    }

    [Fact]
    public void OnDragEnd_ShouldSnapBack_WhenOverlapDetected()
    {
        var vessel = new Vessel { PositionX = 100, PositionY = 150, Width = 50, Height = 50 };
        var allVessels = new List<Vessel> { vessel };
        var startMouseArgs = new MouseEventArgs { ClientX = 120, ClientY = 170 };
        var moveMouseArgs = new MouseEventArgs { ClientX = 200, ClientY = 250 };

        _mockPositionService.Setup(x => x.IsOverlapping(vessel, allVessels)).Returns(true);

        _dragService.OnDragStart(vessel, startMouseArgs);
        _dragService.OnDragMove(moveMouseArgs);

        _dragService.OnDragEnd(allVessels, _mockPositionService.Object);

        Assert.Equal(100, vessel.PositionX);
        Assert.Equal(150, vessel.PositionY);
    }
}

