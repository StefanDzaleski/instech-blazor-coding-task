using instech_blazor_coding_task.Models;
using instech_blazor_coding_task.Services;
using Xunit;

namespace instech_blazor_coding_task.Tests.Services;

public class VesselLayoutServiceTests
{
    private readonly VesselLayoutService _layoutService;

    public VesselLayoutServiceTests()
    {
        _layoutService = new VesselLayoutService();
    }

    [Fact]
    public void GenerateVesselLayout_ShouldScaleVesselDimensionsBy20()
    {
        var fleets = new List<Fleet>
        {
            new Fleet
            {
                shipCount = 1,
                singleShipDimensions = new SingleShipDimensions { width = 5, height = 10 }
            }
        };
        double anchorageWidth = 500;

        var result = _layoutService.GenerateVesselLayout(fleets, anchorageWidth);

        Assert.Single(result);
        Assert.Equal(100, result[0].Width);
        Assert.Equal(200, result[0].Height);
    }

    [Fact]
    public void GenerateVesselLayout_ShouldPositionVesselsToRightOfAnchorage()
    {
        var fleets = new List<Fleet>
        {
            new Fleet
            {
                shipCount = 1,
                singleShipDimensions = new SingleShipDimensions { width = 5, height = 5 }
            }
        };
        double anchorageWidth = 500;

        var result = _layoutService.GenerateVesselLayout(fleets, anchorageWidth);

        Assert.Single(result);
        Assert.Equal(550, result[0].PositionX);
    }

    [Fact]
    public void GenerateVesselLayout_ShouldDistributeVesselsAcrossColumns()
    {
        var fleets = new List<Fleet>
        {
            new Fleet
            {
                shipCount = 4,
                singleShipDimensions = new SingleShipDimensions { width = 5, height = 5 }
            }
        };
        double anchorageWidth = 500;

        var result = _layoutService.GenerateVesselLayout(fleets, anchorageWidth, 3);

        Assert.Equal(4, result.Count);
        Assert.Equal(550, result[0].PositionX);
        Assert.Equal(720, result[1].PositionX);
        Assert.Equal(890, result[2].PositionX);
        Assert.Equal(550, result[3].PositionX);
    }

    [Fact]
    public void GenerateVesselLayout_ShouldStackVesselsVerticallyInSameColumn()
    {
        var fleets = new List<Fleet>
        {
            new Fleet
            {
                shipCount = 4,
                singleShipDimensions = new SingleShipDimensions { width = 5, height = 5 }
            }
        };
        double anchorageWidth = 500;

        var result = _layoutService.GenerateVesselLayout(fleets, anchorageWidth, 3);

        Assert.Equal(0, result[0].PositionY);
        Assert.Equal(110, result[3].PositionY);
    }
}

