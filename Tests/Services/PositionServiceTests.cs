using instech_blazor_coding_task.Models;
using instech_blazor_coding_task.Services;
using Xunit;

namespace instech_blazor_coding_task.Tests.Services;

public class PositionServiceTests
{
    private readonly PositionService _positionService;

    public PositionServiceTests()
    {
        _positionService = new PositionService();
    }

    [Fact]
    public void IsOverlapping_ShouldReturnTrue_WhenVesselsOverlap()
    {
        var vessel1 = new Vessel { PositionX = 0, PositionY = 0, Width = 100, Height = 100 };
        var vessel2 = new Vessel { PositionX = 50, PositionY = 50, Width = 100, Height = 100 };
        var allVessels = new List<Vessel> { vessel1, vessel2 };

        var result = _positionService.IsOverlapping(vessel1, allVessels);

        Assert.True(result);
    }

    [Fact]
    public void IsOverlapping_ShouldReturnFalse_WhenVesselsDoNotOverlap()
    {
        var vessel1 = new Vessel { PositionX = 0, PositionY = 0, Width = 100, Height = 100 };
        var vessel2 = new Vessel { PositionX = 200, PositionY = 200, Width = 100, Height = 100 };
        var allVessels = new List<Vessel> { vessel1, vessel2 };

        var result = _positionService.IsOverlapping(vessel1, allVessels);

        Assert.False(result);
    }

    [Fact]
    public void IsInAnchorage_ShouldReturnTrue_WhenVesselIsFullyInside()
    {
        var vessel = new Vessel { PositionX = 50, PositionY = 100, Width = 50, Height = 50 };
        var anchorage = new Anchorage { Width = 500, Height = 500 };

        var result = _positionService.IsInAnchorage(vessel, anchorage);

        Assert.True(result);
    }

    [Fact]
    public void IsInAnchorage_ShouldReturnFalse_WhenVesselIsOutsideBoundary()
    {
        var vessel = new Vessel { PositionX = 600, PositionY = 100, Width = 50, Height = 50 };
        var anchorage = new Anchorage { Width = 500, Height = 500 };

        var result = _positionService.IsInAnchorage(vessel, anchorage);

        Assert.False(result);
    }

    [Fact]
    public void AllVesselsInAnchorage_ShouldReturnTrue_WhenAllVesselsAreInside()
    {
        var vessels = new List<Vessel>
        {
            new Vessel { PositionX = 50, PositionY = 100, Width = 50, Height = 50 },
            new Vessel { PositionX = 150, PositionY = 150, Width = 50, Height = 50 }
        };
        var anchorage = new Anchorage { Width = 500, Height = 500 };

        var result = _positionService.AllVesselsInAnchorage(vessels, anchorage);

        Assert.True(result);
    }

    [Fact]
    public void AllVesselsInAnchorage_ShouldReturnFalse_WhenOneVesselIsOutside()
    {
        var vessels = new List<Vessel>
        {
            new Vessel { PositionX = 50, PositionY = 100, Width = 50, Height = 50 },
            new Vessel { PositionX = 600, PositionY = 150, Width = 50, Height = 50 }
        };
        var anchorage = new Anchorage { Width = 500, Height = 500 };

        var result = _positionService.AllVesselsInAnchorage(vessels, anchorage);

        Assert.False(result);
    }
}

